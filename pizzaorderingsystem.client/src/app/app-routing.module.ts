import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GuideComponent, WelcomeComponent } from './reference';

const routes: Routes = [
  {
    path: 'order',
    loadChildren: () =>
      import('./component/order/order.module').then((module) => module.OrderModule),
  },
  {
    path: 'home',
    component: WelcomeComponent,
  },
  {
    path: 'guide',
    component: GuideComponent,
  },
  {
    path: '',
    redirectTo: 'order',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: '/',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
