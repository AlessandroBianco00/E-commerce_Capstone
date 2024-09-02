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
    console.log("Registrazione") // Debug
    return this.http.post<iAuthResponse>(this.registerUrl, newUser)
  }

  login(credentials:iAuthData) {
    console.log("Login") // Debug
    return this.http.post<iAuthResponse>(this.loginUrl, credentials).pipe(tap(data => {
      this.authSubject.next(data.user)
      console.log(data)
      localStorage.setItem('currentUser', JSON.stringify(data))

      this.autoLogout()
    }))
  }

  logout():void {
    console.log("Logout") // Debug
    this.authSubject.next(null)
    localStorage.removeItem('currentUser')

    this.router.navigate(['/auth/login'])
  }

  getAccessData():iAuthResponse|null {
    const jsonData = localStorage.getItem('currentUser')

    if(!jsonData) return null

    console.log("User non nullo") // Debug
    const accessData:iAuthResponse = JSON.parse(jsonData)
    return accessData
  }

  autoLogout(): void {
    const accessData = this.getAccessData();

    if (!accessData) return;

    const expDate = this.jwtHelper.getTokenExpirationDate(accessData.token) as Date;
    const expMs = expDate.getTime() - new Date().getTime();

    if (expMs > 2147483647) {
      setTimeout(() => {
        this.autoLogout(); // Recheck after a safe interval
      }, 2147483647);
    } else {
      setTimeout(() => this.logout(), expMs); // Set the final logout
    }
  }

  restoreUser():void {
    console.log("Restore user") // Debug
    const accessData = this.getAccessData()

    if(!accessData) return
    if(this.jwtHelper.isTokenExpired(accessData.token)) return

    console.log("Token valido") // Debug
    this.authSubject.next(accessData.user)

    this.autoLogout()
  }
}
