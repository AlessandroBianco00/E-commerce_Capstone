import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { iCategoryDto } from '../Dto/category-dto';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  categoriesSubject = new BehaviorSubject<iCategoryDto[]>([])
  categories$ = this.categoriesSubject.asObservable()

  constructor(
    private http:HttpClient
  ) {
    this.getAllCategories().subscribe((categories) => this.categoriesSubject.next(categories))
  }

  categoryUrl:string = 'https://localhost:7059/api/Category'
  categoryDtoUrl:string = `${this.categoryUrl}/dto`

  getAllCategories(){
    return this.http.get<iCategoryDto[]>(this.categoryDtoUrl)
  }

  getCategoryById(id:number){
    return this.http.get<iCategoryDto>(`${this.categoryDtoUrl}/${id}`)
  }
}
