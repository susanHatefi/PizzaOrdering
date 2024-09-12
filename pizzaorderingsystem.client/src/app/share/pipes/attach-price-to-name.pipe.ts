import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'attachPriceToName',
  pure: true,
})
export class AttachPriceToNamePipe implements PipeTransform {
  transform(
    value: string,
    price: number,
    precision: number = 0,
    currency: string = '$',
    withoutValue: boolean = false
  ): string {
    if ((withoutValue || value) && price) {
      if (precision) {
        const val = Number.parseFloat(price.toString()).toFixed(precision);
        return `${value ?? ''} (${currency}${val})`;
      }
      return `${value ?? ''} (${currency}${price})`;
    }

    return '';
  }
}
