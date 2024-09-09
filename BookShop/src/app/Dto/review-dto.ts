import { iUserDto } from "./user-dto";

export interface iReviewDto {
  reviewId: number;
  score: number;
  description: string;
  bookId: number;
  userId: number;
  user: iUserDto
}
