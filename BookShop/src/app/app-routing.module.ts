import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./Pages/home/home.module').then(m => m.HomeModule),
    title: "BookShop"
  },
  {
    path: 'auth',
    loadChildren: () => import('./Pages/auth/auth.module').then(m => m.AuthModule),
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
    title: "Profile"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
