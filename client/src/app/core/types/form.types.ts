import { FormControl, FormGroup } from '@angular/forms';

export type NullableControlsOf<T extends Record<string, any>> = {
  [K in keyof T]: FormControl<T[K] | null>
};

export type ControlsOf<T extends Record<string, any>> = {
  [K in keyof T]: FormControl<T[K] | null>
};

export type NestedControlsOf<T extends Record<string, any>> = {
  [K in keyof T]: T[K] extends Record<any, any>
    ? FormGroup<NestedControlsOf<T[K]>>
    : FormControl<T[K]>;
};
