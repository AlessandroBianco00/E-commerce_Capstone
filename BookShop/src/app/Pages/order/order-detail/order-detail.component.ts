import { Component } from '@angular/core';
import { OrderService } from '../../../Services/order.service';
import { ActivatedRoute } from '@angular/router';
import { iOrderDto } from '../../../Dto/order-dto';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrl: './order-detail.component.scss'
})
export class OrderDetailComponent {

  currentOrder!:iOrderDto

  constructor(
    private OrderSvc:OrderService,
    private route:ActivatedRoute,
  ) { }

  ngOnInit() {
    this.route.params.subscribe((params: any) => {
      const orderId = params['id'];

      this.OrderSvc.getMyOrderById(orderId).subscribe(order => this.currentOrder = order);
    });
  }

}
