import { Component } from '@angular/core';
import { iOrderDto } from '../../Dto/order-dto';
import { OrderService } from '../../Services/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss'
})
export class OrderComponent {

  orderArray!:iOrderDto[]

  constructor(private OrderSvc:OrderService)
  {}

  ngOnInit() {
    this.OrderSvc.getMyOrders().subscribe(orders => this.orderArray = orders)
  }

  getOrderTotal(order:iOrderDto):number{
    let orderTotal = 0
    order.books.forEach(orderItem => {
      orderTotal += orderItem.quantity*orderItem.price
    })
    return orderTotal
  }

  statusOrdine(statusId:number) {
    switch(statusId){
      case 1:
        return "Preso in carico";
      case 2:
        return "Spedito";
      case 3:
        return "In consegna";
      case 4:
        return "Consegnato";
      default:
          return "Imprevisto"
    }
  }
}
