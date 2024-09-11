import { Component } from '@angular/core';
import { iCartDto } from '../../Dto/cart-dto';
import { CartService } from '../../Services/cart.service';
import { Router } from '@angular/router';
import { ShippingAddressService } from '../../Services/shipping-address.service';
import { iShippingAddress } from '../../Models/shipping-address';

@Component({
  selector: 'app-order-preview',
  templateUrl: './order-preview.component.html',
  styleUrl: './order-preview.component.scss'
})
export class OrderPreviewComponent {

  myCart!:iCartDto
  addressArray:iShippingAddress[] = []

  constructor(
    private CartSvc:CartService,
    private AddressSvc:ShippingAddressService,
    private router:Router
  ){}

  ngOnInit() {
    this.CartSvc.getMyCart().subscribe(cart => this.myCart = cart)

    this.AddressSvc.myAddresses$.subscribe((addresses) => {
      this.addressArray = addresses
    })
  }
}
