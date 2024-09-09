import { iDiscount } from "../Models/discount";
import { iAuthorSearchDto } from "./author-search-dto";
import { iCategoryDto } from "./category-dto";
import { iReviewDto } from "./review-dto";
import { iTranslatorSearchDto } from "./translator-search-dto";

export interface iBookDetailDto {
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
  author: iAuthorSearchDto;
  translator: iTranslatorSearchDto;
  discount: iDiscount;
  categories: iCategoryDto[];
  reviews: iReviewDto[];
}
