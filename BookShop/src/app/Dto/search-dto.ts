import { iBookSearchDto } from "./book-search-dto";

export interface iSearchDto {
    pages: number;
    books: iBookSearchDto[];
}
