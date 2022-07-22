import { ChangeDetectionStrategy, Component, Input, Optional, Self } from '@angular/core';
import { AgeRange, Gender, UserInfo } from '../../api/models';
import { FormBuilder, FormControl, NgControl } from '@angular/forms';
import { map, takeUntil } from 'rxjs';
import { BooleanInput, coerceBooleanProperty } from '@angular/cdk/coercion';
import { combineEnumFlags, EnumMembersDescription, splitEnumFlags } from '../../core/utils';
import { ControlValueAccessorAdapter } from '../../core/adapters';


interface UserInfoForm {
  age: FormControl<AgeRange[] | AgeRange | null>;
  gender: FormControl<Gender[] | Gender | null>;
}

@Component({
  selector: 'app-user-info-control',
  templateUrl: './user-info-control.component.html',
  styleUrls: ['./user-info-control.component.scss'],
  host: {
    '(focusout)': 'onTouched()'
  },
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserInfoControlComponent extends ControlValueAccessorAdapter {

  @Input()
  get allowMultipleSelections() {
    return this._allowMultipleSelections;
  }

  set allowMultipleSelections(value: BooleanInput) {
    this._allowMultipleSelections = coerceBooleanProperty(value);
  }

  private _allowMultipleSelections = false;

  public ageRanges = AgeRange;
  public genders = Gender;

  public enumMembersMap = new Map<typeof AgeRange | typeof Gender, EnumMembersDescription>([
    [this.ageRanges, AgeRange.Description],
    [this.genders, Gender.Description]
  ]);

  public control = this.fb.group<UserInfoForm>({
    age: this.fb.control(null),
    gender: this.fb.control(null)
  });

  constructor(
    @Self() @Optional() public ngControl: NgControl | null,
    private fb: FormBuilder
  ) {
    super();

    if (this.ngControl != null) {
      this.ngControl.valueAccessor = this;
    }
  }

  public override writeValue(val: UserInfo): void {
    if (val == null || !this.allowMultipleSelections) {
      super.writeValue(val);
      return;
    }

    super.writeValue({
      age: val.age ? splitEnumFlags(val.age, AgeRange.Flags) : val.age,
      gender: val.gender ? splitEnumFlags(val.gender, Gender.Flags) : val.gender
    });
  }

  public override registerOnChange(fn: any): void {
    this.control.valueChanges
      .pipe(
        map(val => {
          if (!this.allowMultipleSelections) {
            return val;
          }

          return {
            gender: Array.isArray(val.gender) ? combineEnumFlags(val.gender) : val.gender,
            age: Array.isArray(val.age) ? combineEnumFlags(val.age) : val.age
          };
        }),
        takeUntil(this.unsubscribe)
      ).subscribe(fn);
  }
}
