import { ShippingAddressService } from './../../../Services/shipping-address.service';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { iShippingAddress } from '../../../Models/shipping-address';

@Component({
  selector: 'app-update-address',
  templateUrl: './update-address.component.html',
  styleUrl: './update-address.component.scss'
})
export class UpdateAddressComponent {

  shippingAddress!:iShippingAddress

  constructor(
    private route:ActivatedRoute,
    private AddressSvc:ShippingAddressService,
    private router:Router
  ){}

  ngOnInit() {
    this.route.params.subscribe((params:any) => {

      this.AddressSvc.getAddressById(params.id).subscribe(addressFound => {
        if (addressFound) {
          this.shippingAddress = addressFound;
        }
      });
    })
  }

  update(id:number){
    this.AddressSvc.updateAddress(this.shippingAddress, id)
    .subscribe(()=>{
      setTimeout(() => {this.router.navigate(['/address'])}, 1000)
    })
  }
}
