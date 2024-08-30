import { iCart } from "./cart";
import { iBook } from "./book";

export interface iCartItem {
    cartItemId: number;
    quantity: number;
    cartId: number;
    bookId: number;
    cart: iCart;
    book: iBook;
}
