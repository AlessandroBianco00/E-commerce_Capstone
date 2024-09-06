import { Component } from '@angular/core';
import { iBook } from '../../Models/book';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrl: './book-detail.component.scss'
})
export class BookDetailComponent {

  bookDetail!:iBook
}
