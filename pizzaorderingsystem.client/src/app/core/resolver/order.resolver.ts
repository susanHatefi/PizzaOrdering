import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { Store } from '@ngrx/store';
import { Selectors, State } from '../../component/order/order-reference';

export const orderResolver: ResolveFn<any> = (route, state) => {
  const store = inject(Store<State>);
  return store.select(Selectors.getOrderItems);
};
