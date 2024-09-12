import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ShareModule } from '../../share/share.module';
import {
  ModifyOrderComponent,
  OrderComponent,
  OrderEffects,
  orderReducer,
  PresentationalModifyOrderComponent,
} from './order-reference';
import { OrderRoutingModule } from './order-routing.module';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

@NgModule({
  declarations: [
    OrderComponent,
    ModifyOrderComponent,
    PresentationalModifyOrderComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ShareModule,
    StoreModule.forFeature('order', orderReducer),
    EffectsModule.forFeature(OrderEffects),
    OrderRoutingModule,
  ],
})
export class OrderModule {}
