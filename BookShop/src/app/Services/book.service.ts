import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iBook } from '../Models/book';

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

  getBooksSearched(filters: { categoryName?: string, authorName?: string, title?: string, editor?: string }) {
    let params = new HttpParams();
    if (filters.categoryName) {
      params = params.set('categoryName', filters.categoryName);
    }
    if (filters.authorName) {
      params = params.set('authorName', filters.authorName);
    }
    if (filters.title) {
      params = params.set('title', filters.title);
    }
    if (filters.editor) {
      params = params.set('editor', filters.editor);
    }

    return this.http.get<iBook[]>(`${this.booksUrl}/search`, { params });
  }

}

