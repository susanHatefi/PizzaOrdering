import { Observable } from 'rxjs';

export abstract class RepoInterface<T> {
  getAll?: (() => Observable<T | T[]>) | undefined;
  insert?: ((data: T | any) => Observable<T | any>) | undefined;
}
