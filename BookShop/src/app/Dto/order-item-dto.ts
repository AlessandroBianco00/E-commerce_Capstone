import { iBookSearchDto } from "./book-search-dto";

export interface iOrderItemDto {
    orderItemId: number;
    quantity: number;
    price: number;
    orderId: number;
    bookId: number;
    book: iBookSearchDto;
}
