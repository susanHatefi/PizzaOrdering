import { createAction, props } from '@ngrx/store';
import { ProductFeatureModel } from '../../../reference';

export const loadingProductInfoData = createAction(
  '[API] Loading Product Features'
);
export const SuccessfullyloadedProductInfoData = createAction(
  '[API] Successfully Loaded Product Features',
  props<{ productInfoData: ProductFeatureModel }>()
);
export const FailedToloadProductInfoData = createAction(
  '[API] Failed To Load Product Features',
  props<{ error: string }>()
);
