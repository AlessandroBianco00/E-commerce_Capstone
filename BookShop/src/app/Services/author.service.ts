import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iAuthor } from '../Models/author';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  authorUrl:string = 'https://localhost:7059/api/Author'

  constructor(
    private http:HttpClient
  ) { }

  getAllAuthors(){
    return this.http.get<iAuthor[]>(this.authorUrl)
  }

  getAuthorById(id:number){
    return this.http.get<iAuthor>(`${this.authorUrl}/${id}`)
  }
}
