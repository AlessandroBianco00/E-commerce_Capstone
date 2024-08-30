import { iBook } from "./book";
import { iUser } from "./user";

export interface iReview {
    reviewId: number;
    score: number;
    description: string;
    bookId: number;
    userId: number;
    book: iBook;
    user: iUser;
}
