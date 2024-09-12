import {
  AfterContentChecked,
  AfterContentInit,
  AfterViewChecked,
  AfterViewInit,
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import {
  OrderColumnResult,
  OrderModel,
  PizzaSizeEnum,
  PizzaSizeModel,
  PromotionModel,
  ToppingEnum,
  ToppingModel,
} from '../../../../reference';

@Component({
  selector: 'app-presentational-modify-order',
  templateUrl: './presentational-modify-order.component.html',
  styleUrl: './presentational-modify-order.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PresentationalModifyOrderComponent implements AfterViewInit {
  @Input() pizzaSizes: PizzaSizeModel[] = [];
  @Input() vegToppings: ToppingModel[] = [];
  @Input() nonVegToppings: ToppingModel[] = [];
  @Input() promotions: PromotionModel[] = [];
  @Input() form: FormGroup | undefined;
  @Input() formData: any | undefined;
  @Output() bindform: EventEmitter<any> = new EventEmitter<any>();
  @Output() saveFormDataInState: EventEmitter<any> = new EventEmitter<any>();

  orderItemsPrice: Map<string, OrderColumnResult> = new Map();

  get toppingsWithPrice(): { [key: string]: number } {
    return (
      [...(this.vegToppings ?? []), ...(this.nonVegToppings ?? [])].reduce(
        (current, topping) => ({ ...current, [topping.name]: topping.price }),
        {}
      ) ?? {}
    );
  }

  get sizePrices(): { [key: string]: number } {
    return (
      this.pizzaSizes?.reduce(
        (current, size) => ({ ...current, [size.name]: size.price }),
        {}
      ) ?? {}
    );
  }

  pizzaOrders(sizeName: string): FormArray {
    return <FormArray>this.form?.get(sizeName);
  }
  constructor() {}
  ngAfterViewInit(): void {
    setTimeout(() => {
      this.bindform.emit({
        pizzaSizes: this.pizzaSizes,
        vegToppings: this.vegToppings,
        nonVegToppings: this.nonVegToppings,
        formData: this.formData,
      });
    }, 0);
  }

  onToppingChange(
    toppingType: string,
    sizeName: string,
    orderIndex: number,
    toppingInOrderIndex: number
  ) {
    const controlName =
      toppingType === ToppingEnum.Veg ? 'vegToppings' : 'nonVegToppings';
    const order = <FormGroup>(
      this.pizzaOrders(sizeName).get(
        `${orderIndex}.${controlName}.${toppingInOrderIndex}`
      )
    );
    const isChecked = order.get('isChecked')?.value;
    const toppingName = order.get('name')?.value;
    const toppings = this.pizzaOrders(sizeName).get(
      `${orderIndex}.toppings`
    ) as FormControl;
    if (isChecked) {
      const toppingSet: Set<string> = new Set([
        ...(toppings.value ?? []),
        toppingName,
      ]);
      toppings.patchValue([...toppingSet]);
    } else {
      const valueWithRemovedItem = toppings.value.filter(
        (topping: any) => topping !== toppingName
      );
      toppings.patchValue([...valueWithRemovedItem]);
    }
    this.onCaculatePrice(sizeName);
    this.saveFormDataInState.emit(this.form?.value);
  }

  getGroupingItemOrders(orderItems: OrderModel[]): OrderModel[] {
    const map = new Map();
    if (orderItems) {
      for (let item of orderItems) {
        const stringTopping = JSON.stringify(item.toppings);
        if (map.has(item.size)) {
          const oldeValue = map.get(item.size);
          map.set(item.size, [...oldeValue, { ...item, stringTopping }]);
        } else {
          map.set(item.size, [{ ...item, stringTopping }]);
        }
      }
    }
    const newOrderItems = this.getMergedItemOrders(map);
    return newOrderItems;
  }

  getMergedItemOrders(groupItems: Map<string, any[]>): OrderModel[] {
    const orderItems: OrderModel[] = [];
    for (let [key, value] of groupItems) {
      const newSet = new Set();
      const newMap = new Map();
      for (let i = 0; i < value.length; i++) {
        const item = value[i];
        const { stringTopping, ...newItem } = item;
        if (newSet.has(stringTopping)) {
          const oldItem = newMap.get(stringTopping);
          newMap.set(item.stringTopping, {
            ...oldItem,
            quantity: oldItem.quantity + 1,
          });
        } else {
          newSet.add(stringTopping);
          newMap.set(stringTopping, newItem);
        }
      }
      orderItems.push(...newMap.values());
    }

    return orderItems;
  }

  onCaculatePrice(sizeName: string) {
    if (this.orderItemsPrice.has(sizeName)) {
      this.orderItemsPrice.set(sizeName, {
        offers: [],
        totalPrice: 0,
        originalPrice: 0,
        hasDiscount: false,
      });
    }
    const orderItems = this.pizzaOrders(sizeName).value?.map((val: any) => {
      const freezedToppings = Object.freeze(val?.toppings);
      return {
        size: val.size,
        toppings: [...freezedToppings].sort(),
        quantity: val.quantity,
      };
    });
    const mergedOrderItems = this.getGroupingItemOrders(orderItems);
    mergedOrderItems?.map((item: OrderModel) => {
      if (item.toppings) {
        const itemQuantity = item?.quantity ?? 1;
        let hasDiscount = false;
        let offers = '';
        const itemPricesSum = item.toppings.reduce(
          (current, topping) => current + this.toppingsWithPrice[topping],
          this.sizePrices[item.size]
        );
        let totalItemPrice = itemPricesSum;
        let machedPromotion: PromotionModel | undefined;
        if (this.promotions) {
          for (let promotion of this.promotions) {
            if (promotion.size !== item.size) continue;
            if (promotion.totalToppings) {
              let totalToppingNo = item.toppings.length;
              if (promotion.totalToppingsUnit) {
                totalToppingNo = promotion.totalToppingsUnit

                  .filter((t) => item.toppings.includes(t.toppingName))
                  .reduce((c, v) => {
                    c = c - 1 + v.unit;
                    return c;
                  }, item.toppings.length);
              }
              if (promotion.totalToppings === totalToppingNo) {
                if (promotion.quantity) {
                  if (itemQuantity >= promotion.quantity) {
                    machedPromotion = promotion;
                    break;
                  }
                } else {
                  machedPromotion = promotion;
                  break;
                }
              }
            }
          }
        }

        if (machedPromotion) {
          hasDiscount = hasDiscount || !!machedPromotion.discount;
          offers = machedPromotion.name;
          if (machedPromotion.quantity) {
            const numberOfOffersMatched = Math.floor(
              itemQuantity / machedPromotion?.quantity
            );
            totalItemPrice =
              (machedPromotion.price
                ? machedPromotion.price
                : ((100 - machedPromotion.discount) * itemPricesSum) / 100) *
              numberOfOffersMatched;
            const numberOfItemswithoutOffers =
              itemQuantity - numberOfOffersMatched * machedPromotion.quantity;
            totalItemPrice =
              totalItemPrice + numberOfItemswithoutOffers * itemPricesSum;
          } else {
            totalItemPrice =
              (machedPromotion.price
                ? machedPromotion.price
                : ((100 - machedPromotion.discount) * itemPricesSum) / 100) *
              itemQuantity;
          }
        } else {
          hasDiscount = false;
          offers = '';
          totalItemPrice = itemPricesSum * itemQuantity;
        }

        if (this.orderItemsPrice.has(item.size)) {
          const oldeValue = this.orderItemsPrice.get(
            item.size
          ) as OrderColumnResult;
          this.orderItemsPrice.set(item.size, {
            ...oldeValue,
            offers: offers
              ? [...(oldeValue?.offers ?? []), offers]
              : [...(oldeValue?.offers ?? [])],
            totalPrice: totalItemPrice + oldeValue.totalPrice,
            originalPrice:
              itemPricesSum * itemQuantity + oldeValue.originalPrice,
            hasDiscount,
          });
        } else {
          this.orderItemsPrice.set(item.size, {
            totalPrice: totalItemPrice,
            originalPrice: itemPricesSum * itemQuantity,
            offers: offers ? [offers] : [],
            hasDiscount,
          });
        }
      }
    });
  }
}
