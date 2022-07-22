import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { ChatHub } from '../../api/hubs';
import { ChatOptionsStore } from '../../stores';
import { DestroyableMixin } from '../../core/mixins';
import { takeUntil, tap } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-searching-chat-view',
  templateUrl: './searching-chat-view.component.html',
  styleUrls: ['./searching-chat-view.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SearchingChatViewComponent extends DestroyableMixin() implements OnInit {

  constructor(
    @Inject(ChatHub.TOKEN) private chatHub: ChatHub,
    private chatOptionsStore: ChatOptionsStore,
    private router: Router
  ) {
    super();
  }

  public ngOnInit(): void {
    this.chatHub.listen.conversationStarted
      .pipe(
        tap(() => this.router.navigate(['chat'])),
        takeUntil(this.destroyed$)
      ).subscribe()

    const chatOptions = this.chatOptionsStore.chatOptions;
    if (chatOptions == null) {
      this.router.navigate(['']);
      return;
    }

    this.chatHub.invoke.startSearch(chatOptions)
      .pipe(
        takeUntil(this.destroyed$)
      ).subscribe({ error: () => this.router.navigate(['']) });
  }

  public stopSearch(): void {
    this.chatHub.send.stopSearch().pipe(
      takeUntil(this.destroyed$)
    ).subscribe();
    this.router.navigate(['']);
  }
}
