import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iCartDto } from '../Dto/cart-dto';

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
}
