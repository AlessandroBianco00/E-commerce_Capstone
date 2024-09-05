import { Component } from '@angular/core';
import { BookService } from '../../Services/book.service';
import { iBook } from '../../Models/book';
import { iCategoryDto } from '../../Dto/category-dto';
import { CategoryService } from '../../Services/category.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  bookArray:iBook[] = []
  categoryArray:iCategoryDto[] = []

  constructor(
    private BookSvc:BookService,
    private CategorySvc: CategoryService
  ) {}

  ngOnInit() {
    this.CategorySvc.categories$.subscribe((categories) => {
      this.categoryArray = categories
    })

    this.BookSvc.getAllBooks().subscribe(books => {
      console.log(books)
      this.bookArray = books
    })
  }
}
