import { Subject, takeUntil } from 'rxjs';
import { AbstractControl, ControlValueAccessor } from '@angular/forms';
import { Injectable, OnDestroy } from '@angular/core';

@Injectable()
export abstract class ControlValueAccessorAdapter implements ControlValueAccessor, OnDestroy {

  protected unsubscribe = new Subject<void>();

  public abstract control: AbstractControl;

  public onTouched: () => void = () => {
  };

  public ngOnDestroy(): void {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }

  public writeValue(val: any): void {
    if (val === null) {
      this.control.reset();
    } else {
      this.control.setValue(val, { emitEvent: false });
    }
  }

  public registerOnChange(fn: any): void {
    this.control.valueChanges.pipe(takeUntil(this.unsubscribe)).subscribe(fn);
  }

  public registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.control.disable() : this.control.enable();
  }
}
