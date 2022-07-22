import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { AbstractControl, FormBuilder } from '@angular/forms';
import { UserInfo } from '../../api/models';
import { NullableControlsOf } from '../../core/types';

interface UserInfoValidatorErrors {
  ageRequired?: true;
  genderRequired?: true
}

function userInfoValidator(
  control: AbstractControl<UserInfo | null>
): UserInfoValidatorErrors | null {
  const value = control.value

  const errors: UserInfoValidatorErrors = {};
  if (value?.age == null || value?.age == 0) {
    errors['ageRequired'] = true;
  }
  if (value?.gender == null || value?.gender == 0) {
    errors['genderRequired'] = true;
  }
  return Object.keys(errors).length === 0 ? null : errors;
}

export type ChatOptions = {
  userInfo: UserInfo;
  searchPreferences: UserInfo;
};
type ChatOptionsForm = NullableControlsOf<ChatOptions>;

@Component({
  selector: 'app-chat-options-form',
  templateUrl: './chat-options-form.component.html',
  styleUrls: ['./chat-options-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChatOptionsFormComponent {

  @Input()
  public set chatOptions(options: ChatOptions | null) {
    if (options != null) {
      this.form.setValue(options);
    }
  }

  public form = this.fb.group<ChatOptionsForm>({
    userInfo: this.fb.control(null, { validators: userInfoValidator }),
    searchPreferences: this.fb.control(null, { validators: userInfoValidator })
  });

  public get invalid(): boolean {
    return this.form.invalid;
  }

  public get validatedValue(): ChatOptions {
    if (this.form.invalid) {
      throw new Error('Form is invalid');
    }
    return this.form.value as ChatOptions;
  }

  constructor(
    private fb: FormBuilder
  ) {
  }

}
