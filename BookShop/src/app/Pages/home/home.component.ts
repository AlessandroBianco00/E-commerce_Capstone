import { Component } from '@angular/core';
import { BookService } from '../../Services/book.service';
import { iBook } from '../../Models/book';
import { iCategoryDto } from '../../Dto/category-dto';
import { CategoryService } from '../../Services/category.service';
import { iBookSearchDto } from '../../Dto/book-search-dto';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  bookArray:iBookSearchDto[] = []
  categoryArray:iCategoryDto[] = []

  constructor(
    private BookSvc:BookService,
    private CategorySvc: CategoryService
  ) {}

  ngOnInit() {
    this.CategorySvc.categories$.subscribe((categories) => {
      this.categoryArray = categories
    })

    // I libri consigliati sono visibili solo se si è loggati
    this.BookSvc.getRecommendedBooks().subscribe(books => {
      this.bookArray = books
    })
  }
}
