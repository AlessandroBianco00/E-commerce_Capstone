import { Component } from '@angular/core';
import { iCartDto } from '../../Dto/cart-dto';
import { CartService } from '../../Services/cart.service';
import { Router } from '@angular/router';
import { ShippingAddressService } from '../../Services/shipping-address.service';
import { iShippingAddress } from '../../Models/shipping-address';
import { iOrder } from '../../Models/order';
import { iUserDto } from '../../Dto/user-dto';
import { AuthService } from '../../Services/auth.service';
import { OrderService } from '../../Services/order.service';

@Component({
  selector: 'app-order-preview',
  templateUrl: './order-preview.component.html',
  styleUrl: './order-preview.component.scss'
})
export class OrderPreviewComponent {

  myCart!:iCartDto
  addressArray:iShippingAddress[] = []
  newOrder:Partial<iOrder> = { }
  currentUser!:iUserDto


  constructor(
    private AuthSvc:AuthService,
    private CartSvc:CartService,
    private AddressSvc:ShippingAddressService,
    private OrderSvc:OrderService,
    private router:Router
  ){}

  ngOnInit() {
    const accessData = this.AuthSvc.getAccessData()
    if(!accessData) return
    this.currentUser = accessData.user

    this.CartSvc.getMyCart().subscribe(cart => this.myCart = cart)

    this.AddressSvc.myAddresses$.subscribe((addresses) => {
      this.addressArray = addresses
    })
  }

  completeOrder(){
    let addressChosen = this.addressArray.find(a => a.shippingAddressId == this.newOrder.shippingAddressId)
    this.newOrder.userId = addressChosen?.userId
    this.newOrder.status = 1 // Status 1:Preso in carico 2:Spedito 3:In consegna 4:Consegnato 5:Imprevisto

    this.OrderSvc.makeAnOrder(this.newOrder).subscribe(() => {
      setTimeout(() => {
        this.router.navigate(['cart']);
    }, 1000);
    })
  }
}
