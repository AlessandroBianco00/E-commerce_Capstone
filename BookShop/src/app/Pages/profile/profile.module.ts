import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileComponent } from './profile.component';
import { UpdateProfileComponent } from './update-profile/update-profile.component';


@NgModule({
  declarations: [
    ProfileComponent,
    UpdateProfileComponent
  ],
  imports: [
    CommonModule,
    ProfileRoutingModule
  ]
})
export class ProfileModule { }
