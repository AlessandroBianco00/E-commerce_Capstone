import { Component } from '@angular/core';
import { AuthService } from '../../Services/auth.service';
import { iUserDto } from '../../Dto/user-dto';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {

  currentUser!:iUserDto

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
