import { Component } from '@angular/core';
import { AuthService } from '../../../Services/auth.service';
import { Router } from '@angular/router';
import { UserService } from '../../../Services/user.service';
import { iUserDto } from '../../../Dto/user-dto';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.scss'
})
export class UpdateProfileComponent {

  currentUser!:iUserDto

  constructor(
    private UserSvc:UserService,
    private AuthSvc:AuthService,
    private router:Router
  ){}

  ngOnInit() {
    const accessData = this.AuthSvc.getAccessData()
    if(!accessData) return
    this.currentUser = accessData.user
  }

  update(id: number) {
    this.UserSvc.updateProfile(this.currentUser, id)
      .subscribe({
        next: updatedUser => {
          const authResponse = JSON.parse(localStorage.getItem('currentUser') || '{}');
          authResponse.user = updatedUser;
          localStorage.setItem('currentUser', JSON.stringify(authResponse));

          setTimeout(() => {
            this.router.navigate(['/profile']);
          }, 1000);
        },
        error: err => {
          console.error("Update failed", err);
        }
      });
  }

}
