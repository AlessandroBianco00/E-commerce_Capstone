import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Page403Component } from './page403.component';

const routes: Routes = [{ path: '', component: Page403Component }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class Page403RoutingModule { }
