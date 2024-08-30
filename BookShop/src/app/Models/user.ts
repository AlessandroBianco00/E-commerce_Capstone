import { iShippingAddress } from "./shipping-address";
import { iRole } from "./role";
import { iReview } from "./review";
import { iOrder } from "./order";
import { iPayment } from "./payment";

export interface iUser {
    userId: number;
    name: string;
    surname: string;
    email: string;
    phoneNumber: string;
    password: string;
    deletedAt: string | null;
    shippingAddresses: iShippingAddress[];
    roles: iRole[];
    reviews: iReview[];
    orders: iOrder[];
    payments: iPayment[];
}
