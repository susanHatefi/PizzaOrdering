import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpParamModel } from '../../reference';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  header = new HttpHeaders();
  constructor(private httpClient: HttpClient) {}

  get(param: HttpParamModel): any {
    return this.httpClient.get(param.url, { headers: this.header });
  }

  post(param: HttpParamModel): any {
    return this.httpClient.post(param.url, param.data, {
      headers: this.header,
    });
  }

  put(param: HttpParamModel): any {
    return this.httpClient.put(param.url, param.data ?? {}, {
      headers: this.header,
    });
  }

  delete(param: HttpParamModel): any {
    const options = {
      headers: this.header,
      body: param.data,
    };
    return this.httpClient.delete(param.url, options);
  }
}
