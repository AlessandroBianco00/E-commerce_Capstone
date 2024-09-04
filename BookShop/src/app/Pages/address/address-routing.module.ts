import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddressComponent } from './address.component';
import { AddAddressComponent } from './add-address/add-address.component';

const routes: Routes = [
  { path: '', component: AddressComponent },
  { path: 'Add Address', component: AddAddressComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AddressRoutingModule { }
