import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderPreviewRoutingModule } from './order-preview-routing.module';
import { OrderPreviewComponent } from './order-preview.component';


@NgModule({
  declarations: [
    OrderPreviewComponent
  ],
  imports: [
    CommonModule,
    OrderPreviewRoutingModule
  ]
})
export class OrderPreviewModule { }
