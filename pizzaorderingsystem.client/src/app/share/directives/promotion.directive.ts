import {
  Directive,
  Input,
  OnInit,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { PizzaSizeEnum } from '../share-reference';

@Directive({
  selector: '[orderPromotion]',
})
export class PromotionDirective implements OnInit {
  @Input({ required: true }) formGroup!: {
    size: string;
    toppings: string[];
  };
  constructor(
    private templateRef: TemplateRef<any>,
    private vcRef: ViewContainerRef
  ) {}
  ngOnInit(): void {
    console.log(this.formGroup);
    const data = this.matchOffers();
    console.log(data);

    // this.vcRef.createEmbeddedView(this.templateRef, {
    //   $implicit: data,
    // });
  }

  matchOffers(): string {
    const size = this.formGroup.size;
    if (size === PizzaSizeEnum.Medium || size === PizzaSizeEnum.Large) {
      const toppings = this.formGroup.toppings;
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
        const offer2 = size === PizzaSizeEnum.Medium && toppings.length === 4;

        return offer1 ? 'offer1' : offer3 ? 'offer3' : '';
      }
      return '';
    }
    return '';
  }
}
