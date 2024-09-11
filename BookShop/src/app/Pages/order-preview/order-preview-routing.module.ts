import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrderPreviewComponent } from './order-preview.component';

const routes: Routes = [{ path: '', component: OrderPreviewComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderPreviewRoutingModule { }
