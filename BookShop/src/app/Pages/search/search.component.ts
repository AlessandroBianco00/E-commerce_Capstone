import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../Services/book.service';
import { iBookSearchDto } from '../../Dto/book-search-dto';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent {

  bookSearchedArray:iBookSearchDto[] = []
  arrayOfPages:number[] = []

  // SearchParams
  category?: string
  author?: string
  title?: string
  editor?: string
  page?:number

  constructor(
    private route:ActivatedRoute,
    private router: Router,
    private BookSvc:BookService
  )
  {}

  ngOnInit() {

    this.route.queryParams.subscribe(params => {
      this.category = params['category'];
      this.author = params['author'];
      this.title = params['title'];
      this.editor = params['editor'];
      this.page = params['page'] ?? 1;

      this.searchBooks(); // Fetch books whenever query params change
    });
  }

  searchBooks(): void {
    // Set filters
    const filters = {
      category: this.category,
      author: this.author,
      title: this.title,
      editor: this.editor,
      page: this.page
    };

    console.log(filters)

    this.BookSvc.getBooksSearched(filters).subscribe({
      next: (results) => {
        console.log(results); // Process the search results here
        this.bookSearchedArray = results.books

        this.arrayOfPages = [];

        for(let i = 1; i <= results.pages; i++){
          this.arrayOfPages.push(i)
        }
        console.log("number array", this.arrayOfPages)
      },
      error: (error) => {
        console.error('Error fetching books:', error);
      }
    });
  }

  navigateToPage(page: number) {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { page: page },
      queryParamsHandling: 'merge' // Merge with existing query params if any
    });
  }

}
