import { Component } from '@angular/core';
import { BookService } from '../../Services/book.service';
import { iBook } from '../../Models/book';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  bookArray:iBook[] = []

  constructor(
    private BookSvc:BookService,
  ) {}

  ngOnInit() {
    this.BookSvc.getAllBooks().subscribe(books => {
      console.log(books)
      this.bookArray = books
    })
  }
}
