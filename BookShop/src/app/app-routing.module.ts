import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Guards/auth.guard';
import { GuestGuard } from './Guards/guest.guard';
import { AdminGuard } from './Guards/admin.guard';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./Pages/home/home.module').then(m => m.HomeModule),
    title: "BookShop"
  },
  {
    path: 'auth',
    loadChildren: () => import('./Pages/auth/auth.module').then(m => m.AuthModule),
    canActivate:[GuestGuard],
    canActivateChild: [GuestGuard],
    title: "Auth"
  },
  {
    path: 'author/:id',
    loadChildren: () => import('./Pages/author/author.module').then(m => m.AuthorModule),
    title: "Author"
  },
  {
    path: 'profile',
    loadChildren: () => import('./Pages/profile/profile.module').then(m => m.ProfileModule),
    canActivate:[AuthGuard],
    canActivateChild: [AuthGuard],
    title: "Profile"
  },
  {
    path: 'payment',
    loadChildren: () => import('./Pages/payment/payment.module').then(m => m.PaymentModule),
    canActivate:[AuthGuard],
    canActivateChild: [AuthGuard],
    title: "MyPayments"
  },
  {
    path: 'address',
    loadChildren: () => import('./Pages/address/address.module').then(m => m.AddressModule),
    canActivate:[AuthGuard],
    canActivateChild: [AuthGuard],
    title: "MyAddresses"
  },
  {
    path: 'search',
    loadChildren: () => import('./Pages/search/search.module').then(m => m.SearchModule),
    title: "Search"
  },
  {
    path: 'book-detail/:id',
    loadChildren: () => import('./Pages/book-detail/book-detail.module').then(m => m.BookDetailModule),
    title: "BookDetail"
  },
  {
    path: 'cart',
    loadChildren: () => import('./Pages/cart/cart.module').then(m => m.CartModule),
    canActivate:[AuthGuard],
    canActivateChild: [AuthGuard],
    title: "MyCart"
  },
  {
    path: 'order-preview',
    loadChildren: () => import('./Pages/order-preview/order-preview.module').then(m => m.OrderPreviewModule),
    canActivate:[AuthGuard],
    canActivateChild: [AuthGuard],
    title: "OrderPreview"
  },
  {
    path: 'order',
    loadChildren: () => import('./Pages/order/order.module').then(m => m.OrderModule) ,
    canActivate:[AuthGuard],
    canActivateChild: [AuthGuard],
    title: "Order"
  },
  {
    path: 'review/:id',
    loadChildren: () => import('./Pages/review/review.module').then(m => m.ReviewModule),
    canActivate:[AuthGuard],
    canActivateChild: [AuthGuard],
    title:"Review"
  },
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
    canActivate:[AdminGuard],
    canActivateChild: [AdminGuard],
    title:"Admin"
  },
  {
    path: 'page403',
    loadChildren: () => import('./Pages/page403/page403.module').then(m => m.Page403Module),
    title: "403 - Forbidden"
  },
  {
    path: '**',
    loadChildren: () => import('./Pages/page404/page404.module').then(m => m.Page404Module),
    title: "404 - Not Found"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
