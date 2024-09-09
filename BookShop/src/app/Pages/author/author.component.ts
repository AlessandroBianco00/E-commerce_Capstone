import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthorService } from '../../Services/author.service';
import { iAuthorDetailDto } from '../../Dto/author-detail-dto';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrl: './author.component.scss'
})
export class AuthorComponent {

  currentAuthor!:iAuthorDetailDto

  constructor(
    private route:ActivatedRoute,
    private AuthorSvc:AuthorService
  ){}

  ngOnInit() {
    this.route.params.subscribe((params: any) => {
      const authorId = params['id'];

      this.AuthorSvc.getAuthorById(authorId).subscribe(author => this.currentAuthor = author);
    });
  }
}
