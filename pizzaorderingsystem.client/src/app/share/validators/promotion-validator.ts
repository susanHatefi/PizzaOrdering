import { AbstractControl } from '@angular/forms';
import { PizzaSizeEnum } from '../share-reference';

export function promotionValidator(
  c: AbstractControl
): { [key: string]: boolean } | null {
  const size = c.get('size')?.value;
  if (size === PizzaSizeEnum.Medium || size === PizzaSizeEnum.Large) {
    const toppings = c.get('toppings')?.value;
    if (toppings && toppings.length) {
      const offer1 = size === PizzaSizeEnum.Medium && toppings.length === 2;
      const offer3 =
        size === PizzaSizeEnum.Large &&
        (((toppings.includes('Pepperoni') ||
          toppings.includes('Barbecue chicken')) &&
          toppings.length === 2) ||
          (!toppings.includes('Pepperoni') &&
            !toppings.includes('Barbecue chicken') &&
            toppings.length === 4));
      return offer1 ? { offer1: offer1 } : offer3 ? { offer3: offer3 } : null;
      const offer2 = size === PizzaSizeEnum.Medium && toppings.length === 4;
    }
    return null;
  }

  return null;
}
