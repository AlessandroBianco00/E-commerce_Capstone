import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iOrderDto } from '../Dto/order-dto';
import { iOrder } from '../Models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  orderUrl:string = 'https://localhost:7059/api/Order'

  constructor(
    private http:HttpClient
  ) { }

  getMyOrders(){
    return this.http.get<iOrderDto[]>(`${this.orderUrl}/MyOrders`)
  }

  getMyOrderById(id:number){
    return this.http.get<iOrderDto>(`${this.orderUrl}/MyOrder/${id}`)
  }

  makeAnOrder(order:Partial<iOrder>){
    return this.http.post<Partial<iOrder>>(`${this.orderUrl}`, order)
  }
}
