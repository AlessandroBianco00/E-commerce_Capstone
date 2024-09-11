import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderPreviewRoutingModule } from './order-preview-routing.module';
import { OrderPreviewComponent } from './order-preview.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    OrderPreviewComponent
  ],
  imports: [
    CommonModule,
    OrderPreviewRoutingModule,
    FormsModule
  ]
})
export class OrderPreviewModule { }
