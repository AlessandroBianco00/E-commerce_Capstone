import { HttpClient } from '@angular/common/http';
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

  //Metodi per film
  getAllBooks(){
    return this.http.get<iBook[]>(this.booksUrl)
  }

  getBookById(id:number){
    return this.http.get<iBook>(`${this.booksUrl}/${id}`)
  }

}

