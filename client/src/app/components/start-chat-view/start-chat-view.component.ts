import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ChatHub } from '../../api/hubs';
import { Router } from '@angular/router';
import { ChatOptionsStore } from '../../stores';

@Component({
  selector: 'app-start-chat-view',
  templateUrl: './start-chat-view.component.html',
  styleUrls: ['./start-chat-view.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class StartChatViewComponent {

  constructor(
    public chatOptionsStore: ChatOptionsStore,
    private router: Router,
  ) {
  }

  public startSearch(chatOptions: ChatHub.StartSearchRequest): void {
    this.chatOptionsStore.saveChatOptions(chatOptions);
    this.router.navigate(['searching']);
  }
}
