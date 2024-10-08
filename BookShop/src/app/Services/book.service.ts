import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iBook } from '../Models/book';
import { iBookDetailDto } from '../Dto/book-detail-dto';
import { iSearchDto } from '../Dto/search-dto';
import { iBookSearchDto } from '../Dto/book-search-dto';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  booksUrl:string = 'https://localhost:7059/api/Book'

  constructor(
    private http:HttpClient
  ) { }

  getAllBooks(){
    return this.http.get<iBook[]>(this.booksUrl)
  }

  getBookById(id:number){
    return this.http.get<iBook>(`${this.booksUrl}/${id}`)
  }

  //Metodi per la ricerca

  getBooksSearched(filters: { category?: string, author?: string, title?: string, editor?: string, page?:number}) {
    let params = new HttpParams();
    if (filters.category) {
      params = params.set('category', filters.category);
    }
    if (filters.author) {
      params = params.set('author', filters.author);
    }
    if (filters.title) {
      params = params.set('title', filters.title);
    }
    if (filters.editor) {
      params = params.set('editor', filters.editor);
    }
    if (filters.page) {
      params = params.set('page', filters.page);
    }

    return this.http.get<iSearchDto>(`${this.booksUrl}/search`, { params });
  }

  getBookDetail(id:number) {
    return this.http.get<iBookDetailDto>(`${this.booksUrl}/detail/${id}`)
  }

  getRecommendedBooks(){
    return this.http.get<iBookSearchDto[]>(`${this.booksUrl}/recommended`)
  }

}

