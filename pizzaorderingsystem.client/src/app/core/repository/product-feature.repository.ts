import { Injectable } from '@angular/core';
import { HttpService, ProductFeatureInterface } from '../core-reference';
import { Observable } from 'rxjs';
import { environment } from '../../../environment/environment';
import { OrderModel, ProductFeatureModel } from '../../reference';

@Injectable({
  providedIn: 'root',
})
export class ProductFeatureRepository
  implements ProductFeatureInterface<ProductFeatureModel>
{
  baseURL: string =
    environment.host +
    environment.api +
    environment.composite.productFeature.mainUrl;

  constructor(private httpService: HttpService) {}

  getAll(): Observable<ProductFeatureModel> {
    return this.httpService.get({
      url: `${this.baseURL}${environment.composite.productFeature.getAll}`,
    });
  }
}
