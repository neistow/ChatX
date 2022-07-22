import { ChangeDetectionStrategy, Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { DestroyableMixin } from '../../core/mixins';
import { switchMap, takeUntil, throttleTime } from 'rxjs';


interface Emoji {
  id: string;
  name: string;
  native: string;
}

interface EmojiSelectEvent {
  emoji: Emoji;
  $event: Event;
}

@Component({
  selector: 'app-message-input',
  templateUrl: './message-input.component.html',
  styleUrls: ['./message-input.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MessageInputComponent extends DestroyableMixin() implements OnInit {

  public messageControl = new FormControl<string>(
    '',
    {
      nonNullable: true,
      validators: [Validators.required, Validators.maxLength(256)],
    }
  );

  @Output()
  public messageSent = new EventEmitter<string>();

  @Output()
  public textTyped = new EventEmitter<void>();

  public emojiPickerOpen = false;

  constructor() {
    super();
  }

  public ngOnInit(): void {
    this.messageControl.valueChanges.pipe(
      throttleTime(1500),
      takeUntil(this.destroyed$)
    ).subscribe(() => this.textTyped.emit());
  }

  public sendMessage(): void {
    if (this.messageControl.invalid) {
      return;
    }

    this.messageSent.emit(this.messageControl.value);
    this.messageControl.reset('', { emitEvent: false });
  }

  public onEmojiSelect(event: EmojiSelectEvent): void {
    const currentValue = this.messageControl.value;
    this.messageControl.setValue(currentValue + event.emoji.native);
  }

}
