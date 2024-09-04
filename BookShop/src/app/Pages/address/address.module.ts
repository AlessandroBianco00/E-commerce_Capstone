import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddressRoutingModule } from './address-routing.module';
import { AddressComponent } from './address.component';
import { FormsModule } from '@angular/forms';
import { AddAddressComponent } from './add-address/add-address.component';
import { UpdateAddressComponent } from './update-address/update-address.component';


@NgModule({
  declarations: [
    AddressComponent,
    AddAddressComponent,
    UpdateAddressComponent
  ],
  imports: [
    CommonModule,
    AddressRoutingModule,
    FormsModule
  ]
})
export class AddressModule { }
