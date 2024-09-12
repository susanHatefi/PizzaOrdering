import { Injectable } from '@angular/core';
import { HttpService, OrderInterface } from '../core-reference';
import { Observable } from 'rxjs';
import { environment } from '../../../environment/environment';
import { OrderModel } from '../../reference';

@Injectable({
  providedIn: 'root',
})
export class OrderRepository implements OrderInterface<OrderModel> {
  baseURL: string =
    environment.host + environment.api + environment.composite.order.mainUrl;

  constructor(private httpService: HttpService) {}

  insert(data: OrderModel | any): Observable<OrderModel | any> {
    return this.httpService.post({
      url: `${this.baseURL}${environment.composite.order.insert}`,
      data,
    });
  }
}
