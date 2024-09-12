import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import {
  OrderColumnResult,
  OrderModel,
  PizzaSizeModel,
  ToppingEnum,
  ToppingModel,
} from '../../../reference';
import { Store } from '@ngrx/store';
import { State, ApiActions, Selectors, PageActions } from '../order-reference';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-modify-order',
  templateUrl: './modify-order.component.html',
  styles: [
    `
      :host {
        width: 100%;
        height: 100%;
      }
    `,
  ],
})
export class ModifyOrderComponent implements OnInit {
  form: FormGroup | undefined;
  $data = this.store.select(Selectors.getProductInfo);
  formData: any = null;
  constructor(
    private fb: FormBuilder,
    private store: Store<State>,
    private route: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.store.dispatch(ApiActions.loadingProductInfoData());
    this.formData = this.route.snapshot.data['formData'];
  }

  buildToppingArray(toppings: ToppingModel[]): any[] {
    return (
      toppings?.map((veg) =>
        this.fb.group({
          name: this.fb.control(veg.name),
          isChecked: this.fb.control(false),
          price: this.fb.control({
            value: veg.price,
            disabled: true,
          }),
        })
      ) ?? []
    );
  }

  buildFormArray(
    size: string,
    vegToppings: ToppingModel[],
    nonVegToppings: ToppingModel[]
  ): FormGroup {
    return this.fb.group({
      size: this.fb.control(size),
      toppings: this.fb.control([]),
      quantity: this.fb.control(1),
      vegToppings: this.fb.array(this.buildToppingArray(vegToppings)), // .disable(),
      nonVegToppings: this.fb.array(this.buildToppingArray(nonVegToppings)), //.disable(),
    });
  }

  buildForm(value: {
    pizzaSizes: PizzaSizeModel[];
    vegToppings: ToppingModel[];
    nonVegToppings: ToppingModel[];
    formData: any;
  }) {
    this.form = this.fb.group(
      value.pizzaSizes.reduce(
        (current, item) => ({
          ...current,
          [item.name]: this.fb.array([
            this.buildFormArray(
              item.name,
              value.vegToppings,
              value.nonVegToppings
            ),
          ]),
        }),
        {}
      )
    );
    if (value.formData) {
      this.form.patchValue(value.formData);
    }
  }

  keepOrderItemsInState(value: any) {
    this.store.dispatch(PageActions.selectingToping({ formData: value }));
  }
}
