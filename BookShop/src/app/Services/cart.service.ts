import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iCartDto } from '../Dto/cart-dto';
import { iCartItem } from '../Models/cart-item';
import { iCartItemDto } from '../Dto/cart-item-dto';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cartUrl:string = 'https://localhost:7059/api/Cart'

  constructor(
    private http:HttpClient
  ) { }

  getMyCart(){
    return this.http.get<iCartDto>(`${this.cartUrl}/MyCart`)
  }

  addToCart(cartItem:Partial<iCartItem>){
    return this.http.post<iCartItemDto>(`${this.cartUrl}/addToCart`, cartItem)
  }

  removeFromCart(cartItemId:number){
    return this.http.delete<iCartItemDto>(`${this.cartUrl}/removeFromCart/${cartItemId}`)
  }
}
