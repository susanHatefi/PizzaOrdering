import {
  MainState,
  OrderColumnResult,
  OrderModel,
  ProductFeatureModel,
  PromotionModel,
} from '../../../reference';

export interface OrderState {
  productInfoData?: ProductFeatureModel | null;
  orderItems?: any;
}

export interface State extends MainState {
  order: OrderState;
}
