import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookService } from '../Services/book.service';
import { iBookSearchDto } from '../Dto/book-search-dto';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent {

  bookSearchedArray:iBookSearchDto[] = []

  // SearchParams
  category?: string
  author?: string
  title?: string
  editor?: string

  constructor(
    private route:ActivatedRoute,
    private BookSvc:BookService
  )
  {}

  ngOnInit() {

    this.category = this.route.snapshot.queryParams['category']
    this.author = this.route.snapshot.queryParams['author']
    this.title = this.route.snapshot.queryParams['title']
    this.editor = this.route.snapshot.queryParams['editor']

    this.searchBooks();
  }

  searchBooks(): void {
    // Set filters
    const filters = {
      category: this.category,
      author: this.author,
      title: this.title,
      editor: this.editor
    };

    console.log(filters)

    this.BookSvc.getBooksSearched(filters).subscribe({
      next: (results) => {
        console.log(results); // Process the search results here
        this.bookSearchedArray = results
      },
      error: (error) => {
        console.error('Error fetching books:', error);
      }
    });
  }

}
