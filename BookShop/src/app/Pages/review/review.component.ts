import { Component } from '@angular/core';
import { iReview } from '../../Models/review';
import { iUserDto } from '../../Dto/user-dto';
import { AuthService } from '../../Services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ReviewService } from '../../Services/review.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrl: './review.component.scss'
})
export class ReviewComponent {

  currentUser!:iUserDto
  newReview:Partial<iReview> = {}
  bookId!:number

  constructor(
    private AuthSvc:AuthService,
    private ReviewSvc:ReviewService,
    private route:ActivatedRoute,
    private router:Router
  ){}

  ngOnInit() {
    const accessData = this.AuthSvc.getAccessData()
    if(!accessData) return
    this.currentUser = accessData.user

    this.route.params.subscribe((params: any) => {
      this.bookId = params['id'];})
  }

  create(){
    this.newReview.userId = this.currentUser.userId
    this.newReview.bookId = this.bookId
    this.newReview.user = null
    this.newReview.book = null
    console.log(this.newReview)
    this.ReviewSvc.createNewReview(this.newReview)
    .subscribe(()=>{
      setTimeout(() => {this.router.navigate(['/order'])}, 1000)
    })
  }
}
