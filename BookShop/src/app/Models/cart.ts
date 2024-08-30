import { iUser } from "./user";
import { iCartItem } from "./cart-item";

export interface iCart {
    cartId: number;
    userId: number;
    user: iUser;
    books: iCartItem[];
}
