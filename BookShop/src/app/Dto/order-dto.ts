import { iOrderItemDto } from "./order-item-dto";

export interface iOrderDto {
    orderId: number;
    userId: number | null;
    shippingAddressId: number | null;
    orderDate: Date;
    status: number;
    books: iOrderItemDto[];
}
