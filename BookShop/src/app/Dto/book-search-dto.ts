import { iCategory } from "../Models/category";
import { iDiscount } from "../Models/discount";
import { iAuthorSearchDto } from "./author-search-dto";
import { iTranslatorSearchDto } from "./translator-search-dto";

export interface iBookSearchDto {
  bookId: number;
  title: string;
  description: string;
  image: string;
  price: number;
  editor: string;
  language: string;
  quantityAvailable: number;
  authorId: number;
  translatorId: number;
  discountId: number;
  author: iAuthorSearchDto;
  translator: iTranslatorSearchDto;
  discount: iDiscount;
  categories: iCategory[];
}
