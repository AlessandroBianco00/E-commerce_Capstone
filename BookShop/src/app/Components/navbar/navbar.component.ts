import { Component } from '@angular/core';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  show:boolean = false
  isCollapsed:boolean = true

  constructor(private AuthSvc:AuthService) {}

  isLoggedIn = this.AuthSvc.syncIsLoggedIn

  ngOnInit() {
    this.AuthSvc.isLoggedIn$
    .subscribe(isLoggedIn => this.isLoggedIn = isLoggedIn )
  }

  logout() {
    this.AuthSvc.logout()
  }

}
