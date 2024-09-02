import { Component } from '@angular/core';
import { iAuthor } from '../../Models/author';
import { ActivatedRoute } from '@angular/router';
import { AuthorService } from '../../Services/author.service';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrl: './author.component.scss'
})
export class AuthorComponent {

  currentAuthor:iAuthor | null = null

  constructor(
    private route:ActivatedRoute,
    private AuthorSvc:AuthorService
  ){}

  ngOnInit() {
    this.route.params.subscribe((params: any) => {
      const authorId = params['id'];
      this.AuthorSvc.getAuthorById(authorId).subscribe(author => this.currentAuthor = author);
    });

    this.AuthorSvc.getAllAuthors().subscribe(authors => {console.log(authors)})
  }
}
