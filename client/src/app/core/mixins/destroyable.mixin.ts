import { OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { Constructor } from '../types';

export function DestroyableMixin<T extends Constructor<{}>>(Base: T = class {} as any) {
  return class extends Base implements OnDestroy {

    private _destroyed = new Subject<void>();
    protected destroyed$ = this._destroyed.asObservable();

    public ngOnDestroy(): void {
      this._destroyed.next();
      this._destroyed.complete();
    }
  }
}
