import { iAuthor } from "./author";
import { iTranslator } from "./translator";
import { iDiscount } from "./discount";
import { iCategory } from "./category";
import { iReview } from "./review";
import { iWishlist } from "./wishlist";
import { iCartItem } from "./cart-item";
import { iOrderItem } from "./order-item";

export interface iBook {
    bookId: number;
    title: string;
    description: string;
    image: string;
    price: number;
    editor: string;
    pages: number;
    isbn: string;
    language: string;
    publicationDate: Date;
    quantityAvailable: number;
    authorId: number;
    translatorId: number;
    discountId: number;
    author: iAuthor;
    translator: iTranslator;
    discount: iDiscount;
    categories: iCategory[];
    reviews: iReview[];
    wishlists: iWishlist[];
    carts: iCartItem[];
    orders: iOrderItem[];
}
