import { iUser } from "./user";
import { iShippingAddress } from "./shipping-address";
import { iOrderItem } from "./order-item";

export interface iOrder {
    orderId: number;
    userId: number | null;
    shippingAddressId: number | null;
    orderDate: Date;
    status: number;
    user: iUser | null;
    shippingAddress: iShippingAddress | null;
    books: iOrderItem[];
}
