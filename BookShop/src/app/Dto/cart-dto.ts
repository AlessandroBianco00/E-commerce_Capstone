import { iCartItemDto } from "./cart-item-dto";

export interface iCartDto {
    cartId: number;
    userId: number;
    books: iCartItemDto[];
}
