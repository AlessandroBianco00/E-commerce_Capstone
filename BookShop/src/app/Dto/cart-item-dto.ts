import { iBookSearchDto } from "./book-search-dto";

export interface iCartItemDto {
    cartItemId: number;
    quantity: number;
    cartId: number;
    bookId: number;
    book: iBookSearchDto;
}
