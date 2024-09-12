import { createAction, props } from '@ngrx/store';
import { OrderModel, ProductFeatureModel } from '../../../reference';

export const selectingToping = createAction(
  '[Page] Order Items Select Topping',
  props<{ formData: any }>()
);
