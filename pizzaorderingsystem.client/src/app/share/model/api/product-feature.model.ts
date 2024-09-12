export interface ProductFeatureModel {
  pizzaSize: PizzaSizeModel[];
  vegToppings: ToppingModel[];
  nonVegToppings: ToppingModel[];
  promotion: PromotionModel[];
}

export interface PizzaSizeModel {
  name: string;
  price: number;
}

export interface ToppingModel {
  name: string;
  price: number;
}

export interface PromotionModel {
  name: string;
  size: string;
  price: number;
  discount: number;
  totalToppings: number;
  totalToppingsUnit?: [{ toppingName: string; unit: number }];
  quantity?: number;
}
