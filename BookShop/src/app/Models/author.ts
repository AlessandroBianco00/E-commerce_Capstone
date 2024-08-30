import { iBook } from "./book";

export interface iAuthor {
    authorId: number;
    authorName: string;
    image: string;
    description: string;
    books: iBook[];
}
