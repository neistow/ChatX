import { Constructor } from '../types';
import { BehaviorSubject } from 'rxjs';

export function LoadableMixin<T extends Constructor<{}>>(Base: T = class {} as any) {
  return class extends Base {

    private _loading = new BehaviorSubject<boolean>(false);
    public loading$ = this._loading.asObservable();

    public get loading(): boolean {
      return this._loading.value;
    }

    public set loading(value: boolean) {
      this._loading.next(value);
    }
  }
}
