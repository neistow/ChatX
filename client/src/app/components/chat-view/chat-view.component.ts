import {
  ChangeDetectionStrategy,
  Component,
  Inject,
  OnInit
} from '@angular/core';
import { ChatHub } from '../../api/hubs';
import { Router } from '@angular/router';
import { DestroyableMixin } from '../../core/mixins';
import {
  catchError,
  concat,
  finalize,
  map,
  mapTo,
  mergeWith,
  Observable,
  of,
  scan,
  share,
  Subject,
  switchMap,
  takeUntil,
  tap,
  timeout
} from 'rxjs';
import { Message } from '../../api/models';
import { ChatOptionsStore } from '../../stores';

export type ChatMessage = Message & { type: 'sent' | 'received' };

@Component({
  selector: 'app-chat-view',
  templateUrl: './chat-view.component.html',
  styleUrls: ['./chat-view.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChatViewComponent extends DestroyableMixin() implements OnInit {

  private _sentMessages = new Subject<ChatMessage>();
  public chatMessages$: Observable<ChatMessage[]>;

  public strangerTyping$: Observable<boolean>;

  constructor(
    @Inject(ChatHub.TOKEN) private chatHub: ChatHub,
    private chatOptionsStore: ChatOptionsStore,
    private router: Router,
  ) {
    super();

    const messageReceived$: Observable<ChatMessage> = this.chatHub.listen.messageReceived.pipe(
      map(m => ({ ...m, type: 'received' } as ChatMessage)),
      share()
    );

    this.chatMessages$ = this._sentMessages.pipe(
      mergeWith(messageReceived$),
      scan((acc, value) => [value, ...acc], [] as ChatMessage[])
    );

    this.strangerTyping$ = this.chatHub.listen.strangerTyping.pipe(
      switchMap(() => concat(
          of(true),
          messageReceived$.pipe(
            timeout({ first: 2000, with: () => of(false) }),
            mapTo(false),
          )
        )
      ),
    );
  }

  public ngOnInit(): void {
    if (this.chatOptionsStore.chatOptions == null) {
      this.router.navigate(['']);
      return;
    }

    this.chatHub.listen.conversationEnded.pipe(
      takeUntil(this.destroyed$)
    ).subscribe(() => this.router.navigate(['']));
  }

  public cancelChat(): void {
    this.chatHub.invoke.cancelActiveConversation().pipe(
      finalize(() => this.router.navigate([''])),
      takeUntil(this.destroyed$)
    ).subscribe();
  }

  public onMessageSent(text: string): void {
    const message: ChatMessage = {
      text,
      dateTimeSent: new Date().toUTCString(),
      type: 'sent'
    };

    this.chatHub.invoke
      .sendMessage({ text })
      .pipe(
        tap(() => this._sentMessages.next(message)),
        takeUntil(this.destroyed$)
      ).subscribe();
  }

  public onTextTyped(): void {
    this.chatHub.invoke
      .sendTyping()
      .pipe(takeUntil(this.destroyed$))
      .subscribe();
  }
}
