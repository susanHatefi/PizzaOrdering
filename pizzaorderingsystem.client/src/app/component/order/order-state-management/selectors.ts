import { createFeatureSelector, createSelector } from '@ngrx/store';
import { OrderState } from './state';
import { OrderColumnResult, OrderModel, PromotionModel } from '../../../reference';

const orderSelector = createFeatureSelector<OrderState>('order');

export const getProductInfo = createSelector(
  orderSelector,
  (state) => state.productInfoData
);
export const getPizzaSizes = createSelector(
  orderSelector,
  (state) => state.productInfoData?.pizzaSize
);

export const getPromotions = createSelector(
  orderSelector,
  (state) => state.productInfoData?.promotion
);

export const getVegToppings = createSelector(
  orderSelector,
  (state) => state.productInfoData?.vegToppings
);
export const getNonVegToppings = createSelector(
  orderSelector,
  (state) => state.productInfoData?.nonVegToppings
);
export const getToppings = createSelector(
  orderSelector,
  getVegToppings,
  getNonVegToppings,
  (state, vegToppings, nonVegToppings) => [
    ...(vegToppings ?? []),
    ...(nonVegToppings ?? []),
  ]
);

export const getOrderItems = createSelector(
  orderSelector,
  (state) => state.orderItems
);
