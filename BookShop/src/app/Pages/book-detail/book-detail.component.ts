import { Component } from '@angular/core';
import { BookService } from '../../Services/book.service';
import { ActivatedRoute } from '@angular/router';
import { iBookDetailDto } from '../../Dto/book-detail-dto';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrl: './book-detail.component.scss'
})
export class BookDetailComponent {

  bookDetail!:iBookDetailDto

  constructor(
    private route:ActivatedRoute,
    private BookSvc:BookService
  )
  {}

  ngOnInit() {
    this.route.params.subscribe((params:any) => {

      this.BookSvc.getBookDetail(params.id).subscribe(book => {
        if (book) {
          console.log(book);

          this.bookDetail = book;
          console.log(this.bookDetail);
        }
      });
    })
  }
}
