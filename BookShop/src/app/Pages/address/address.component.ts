import { iShippingAddress } from './../../Models/shipping-address';
import { Component } from '@angular/core';
import { AuthService } from '../../Services/auth.service';
import { ShippingAddressService } from '../../Services/shipping-address.service';
import { Router } from '@angular/router';
import { iUserDto } from '../../Dto/user-dto';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrl: './address.component.scss'
})
export class AddressComponent {

  myAddressesArray:iShippingAddress[] = []
  currentUser!:iUserDto

  constructor(
    private AddressSvc: ShippingAddressService,
    private AuthSvc: AuthService,
    private router:Router
  ) {}

  ngOnInit() {
    const accessData = this.AuthSvc.getAccessData()
    if(!accessData) return
    this.currentUser = accessData.user

    this.AddressSvc.getMyAddresses().subscribe(addresses => {
      this.myAddressesArray = addresses
    })
  }

  delete(id:number) {
    this.AddressSvc.deleteAddress(id).subscribe(() => {
      setTimeout(() => {this.router.navigate(['/profile'])}, 500) //Da modificare, deve refreshare la pagina
    })
  }
}
