import { iBookSearchDto } from "./book-search-dto";

export interface iAuthorDetailDto {
    authorId: number;
    authorName: string;
    image: string;
    description: string;
    books: iBookSearchDto[];
}
