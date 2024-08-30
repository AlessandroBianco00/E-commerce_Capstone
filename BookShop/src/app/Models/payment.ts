import { iUser } from "./user";

export interface iPayment {
    paymentId: number;
    userId: number;
    user: iUser;
}
