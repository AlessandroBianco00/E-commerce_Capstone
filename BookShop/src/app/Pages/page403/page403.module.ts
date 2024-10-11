import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Page403RoutingModule } from './page403-routing.module';
import { Page403Component } from './page403.component';


@NgModule({
  declarations: [
    Page403Component
  ],
  imports: [
    CommonModule,
    Page403RoutingModule
  ]
})
export class Page403Module { }
