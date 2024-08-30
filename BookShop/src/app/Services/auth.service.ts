import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { iUser } from '../Models/user';
import { BehaviorSubject, map, tap } from 'rxjs';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { iAuthResponse } from '../Models/auth-response';
import { iAuthData } from '../Models/auth-data';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  jwtHelper:JwtHelperService = new JwtHelperService()

  authSubject = new BehaviorSubject<null|iUser>(null)
  user$ = this.authSubject.asObservable()

  syncIsLoggedIn:boolean = false;

  isLoggedIn$ = this.user$.pipe(
    map(user => !!user),
    tap(user => this.syncIsLoggedIn = user)
  )

  constructor(
    private router:Router,
    private http:HttpClient
  ) {
    this.restoreUser()
  }

  loginUrl:string = 'https://localhost:7059/api/authentication/login'
  registerUrl:string = 'https://localhost:7059/api/authentication/register'

  register(newUser:Partial<iUser>) {
    return this.http.post<iAuthResponse>(this.registerUrl, newUser)
  }

  login(credentials:iAuthData) {
    return this.http.post<iAuthResponse>(this.loginUrl, credentials).pipe(tap(data => {
      this.authSubject.next(data.user)
      console.log(data)
      localStorage.setItem('currentUser', JSON.stringify(data))

      this.autoLogout()
    }))
  }

  logout():void {
    this.authSubject.next(null)
    localStorage.removeItem('currentUser')

    this.router.navigate(['/auth/login'])
  }

  getAccessData():iAuthResponse|null {
    const jsonData = localStorage.getItem('currentUser')

    if(!jsonData) return null

    const accessData:iAuthResponse = JSON.parse(jsonData)
    return accessData
  }

  autoLogout():void {
    const accessData = this.getAccessData()

    if(!accessData) return

    const expDate = this.jwtHelper.getTokenExpirationDate(accessData.token) as Date

    console.log("date", expDate)

    const expMs = expDate.getTime() - new Date().getTime()

    console.log("ms",expMs)

    setTimeout(() => this.logout(), expMs);
  }

  restoreUser():void {
    const accessData = this.getAccessData()

    if(!accessData) return
    if(this.jwtHelper.isTokenExpired(accessData.token)) return

    this.authSubject.next(accessData.user)

    this.autoLogout()
  }
}
