import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../Services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard {

  constructor(
    private AuthSvc:AuthService,
    private router: Router
  ){}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): MaybeAsync<GuardResult> {

    if(this.AuthSvc.syncIsAdmin) return true;

    this.router.navigate(['/page403']);
    return false;
  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    return this.canActivate(childRoute, state)
  }

}
