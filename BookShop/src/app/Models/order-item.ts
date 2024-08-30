import { iOrder } from "./order";
import { iBook } from "./book";

export interface iOrderItem {
    orderItemId: number;
    quantity: number;
    price: number;
    orderId: number;
    bookId: number;
    order: iOrder;
    book: iBook;
}
