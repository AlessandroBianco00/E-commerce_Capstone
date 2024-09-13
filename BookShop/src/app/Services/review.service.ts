import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iReview } from '../Models/review';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  reviewUrl:string = 'https://localhost:7059/api/Review'

  constructor(
    private http:HttpClient
  ) {}

  getReviewsByBookId(bookId:number){
    return this.http.get<iReview[]>(`${this.reviewUrl}/${bookId}`)
  }

  getReviewsByUserId(userId:number){
    return this.http.get<iReview[]>(`${this.reviewUrl}/${userId}`)
  }

  createNewReview(newReview:Partial<iReview>) {
    return this.http.post<iReview>(this.reviewUrl, newReview)
  }
}
