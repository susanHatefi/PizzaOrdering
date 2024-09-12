import { createAction, createReducer, on } from '@ngrx/store';
import { OrderState } from './state';
import { ApiActions, PageActions } from '../order-reference';

const initialOrder: OrderState = {
  orderItems: undefined,
  productInfoData: null,
};

export const orderReducer = createReducer(
  initialOrder,
  on(
    ApiActions.SuccessfullyloadedProductInfoData,
    (state, { productInfoData }): OrderState => {
      return {
        ...state,
        productInfoData,
      };
    }
  ),
  on(PageActions.selectingToping, (state, { formData }): OrderState => {
    return {
      ...state,
      orderItems: formData
        ? {...formData }
        : undefined,
    };
  })
);
