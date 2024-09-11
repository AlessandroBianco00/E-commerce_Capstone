import { Component } from '@angular/core';
import { iCartDto } from '../../Dto/cart-dto';
import { CartService } from '../../Services/cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {

  myCart!:iCartDto

  constructor(
    private CartSvc:CartService,
    private router:Router
  ){}

  ngOnInit() {
    this.CartSvc.getMyCart().subscribe(cart => this.myCart = cart)
  }

  rimuoviDalCarrello(cartItemId:number) {
    this.CartSvc.removeFromCart(cartItemId).subscribe(() => {
      setTimeout(() => {
        this.router.navigate(['']);
    }, 1000);
    })
  }

  getProvisionalTotal():number{
    let provisionalTotal = 0
    this.myCart.books.forEach(cartItem => {
      provisionalTotal += cartItem.quantity*cartItem.book.price
    })
    return provisionalTotal
  }
}
