import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReviewRoutingModule } from './review-routing.module';
import { ReviewComponent } from './review.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    ReviewComponent
  ],
  imports: [
    CommonModule,
    ReviewRoutingModule,
    FormsModule
  ]
})
export class ReviewModule { }
