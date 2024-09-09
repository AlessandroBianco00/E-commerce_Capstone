import { Component } from '@angular/core';
import { BookService } from '../../Services/book.service';
import { ActivatedRoute, Router } from '@angular/router';
import { iBookDetailDto } from '../../Dto/book-detail-dto';
import { iCartItem } from '../../Models/cart-item';
import { iUserDto } from '../../Dto/user-dto';
import { AuthService } from '../../Services/auth.service';
import { CartService } from '../../Services/cart.service';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrl: './book-detail.component.scss'
})
export class BookDetailComponent {

  bookDetail!:iBookDetailDto
  myCartId!:number
  newCartItem:Partial<iCartItem> = {}
  currentUser!:iUserDto

  constructor(
    private route:ActivatedRoute,
    private AuthSvc:AuthService,
    private BookSvc:BookService,
    private CartSvc:CartService,
    private router:Router
  )
  {}

  ngOnInit() {
    const accessData = this.AuthSvc.getAccessData()
    if(!accessData) return
    this.currentUser = accessData.user

    this.route.params.subscribe((params:any) => {

      this.BookSvc.getBookDetail(params.id).subscribe(book => {
        if (book) {
          console.log(book);

          this.bookDetail = book;
        }
      });
    })
  }

  addToCart(bookId:number) {
    this.CartSvc.getMyCart().subscribe(cart => {
        this.myCartId = cart.cartId;
        this.newCartItem.cartId = this.myCartId;
        this.newCartItem.bookId = bookId
        this.newCartItem.cart = null;
        this.newCartItem.book = null

        console.log(this.newCartItem);  // Ensure bookId is set before this point

        this.CartSvc.addToCart(this.newCartItem).subscribe(() => {
            setTimeout(() => {
                this.router.navigate(['/cart']);
            }, 1000);
        });
    });
}
}
