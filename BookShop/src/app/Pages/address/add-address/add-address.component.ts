import { Component } from '@angular/core';
import { iShippingAddress } from '../../../Models/shipping-address';
import { ShippingAddressService } from '../../../Services/shipping-address.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';
import { iUserDto } from '../../../Dto/user-dto';

@Component({
  selector: 'app-add-address',
  templateUrl: './add-address.component.html',
  styleUrl: './add-address.component.scss'
})
export class AddAddressComponent {

  currentUser!:iUserDto
  newAddress:Partial<iShippingAddress> = {}

  constructor(
    private AddressSvc:ShippingAddressService,
    private AuthSvc:AuthService,
    private router:Router
  ){}

  ngOnInit() {
    const accessData = this.AuthSvc.getAccessData()
    if(!accessData) return
    this.currentUser = accessData.user
  }

  create(){

    this.newAddress.userId = this.currentUser.userId
    this.newAddress.user = null
    this.AddressSvc.createNewAddress(this.newAddress)
    .subscribe(()=>{
      setTimeout(() => {this.router.navigate(['/address'])}, 1000)
    })
  }

}
