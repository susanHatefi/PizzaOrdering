import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ModifyOrderComponent } from './order-reference';
import { orderResolver } from '../../core/core-reference';
const routes: Routes = [
  {
    path: '',
    component: ModifyOrderComponent,
    resolve:{
      formData:orderResolver
    }
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrderRoutingModule {}
