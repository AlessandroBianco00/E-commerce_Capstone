import { iUser } from "./user";
import { iBook } from "./book";

export interface iWishlist {
    wishlistId: number;
    userId: number;
    user: iUser;
    books: iBook[];
}
