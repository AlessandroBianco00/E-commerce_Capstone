import { Component } from '@angular/core';
import { AuthService } from '../../../Services/auth.service';
import { Router } from '@angular/router';
import { iUser } from '../../../Models/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {

  newUser:Partial<iUser> = {}

  constructor(
    private authSvc:AuthService,
    private router:Router
  ){}

  register(){
    this.authSvc.register(this.newUser)
    .subscribe(()=>{
      setTimeout(() => {this.router.navigate(['/auth/login'])}, 1000)
    })
  }
}
