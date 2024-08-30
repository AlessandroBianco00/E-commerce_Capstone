import { iBook } from "./book";

export interface iCategory {
    categoryId: number;
    categoryName: string;
    books: iBook[];
}
