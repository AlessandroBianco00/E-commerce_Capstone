import { iOrderItemDto } from "./order-item-dto";
import { iShippingAddressDto } from "./shipping-address-dto";

export interface iOrderDto {
    orderId: number;
    userId: number | null;
    shippingAddressId: number | null;
    orderDate: Date;
    status: number;
    shippingAddress: iShippingAddressDto | null;
    books: iOrderItemDto[];
}
