import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { State, ApiActions } from '../order-reference';
import { catchError, exhaustMap, map, of } from 'rxjs';
import {
  OrderRepository,
  ProductFeatureRepository,
} from '../../../core/core-reference';

@Injectable({
  providedIn: 'root',
})
export class OrderEffects {
  constructor(
    private action$: Actions,
    private productFeatureRepository: ProductFeatureRepository,
    private store: Store<State>
  ) {}

  loadOrderTable$ = createEffect(() =>
    this.action$?.pipe(
      ofType(ApiActions.loadingProductInfoData),
      exhaustMap((action) =>
        this.productFeatureRepository.getAll().pipe(
          map(
            (data) =>
              ApiActions.SuccessfullyloadedProductInfoData({
                productInfoData: data,
              }),
            catchError((error) =>
              of(ApiActions.FailedToloadProductInfoData(error))
            )
          )
        )
      )
    )
  );
}
