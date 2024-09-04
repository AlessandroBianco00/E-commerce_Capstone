import { Component } from '@angular/core';
import { iUser } from '../../Models/user';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {

  currentUser!:iUser

  constructor(
    private AuthSvc: AuthService
  ) {}

  ngOnInit() {
    const accessData = this.AuthSvc.getAccessData()
    if(!accessData) return
    this.currentUser = accessData.user

  }

  redirect(pageName:string) {

  }
}
