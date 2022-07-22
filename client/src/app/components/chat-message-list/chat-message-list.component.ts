import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { ChatMessage } from '../chat-view/chat-view.component';

@Component({
  selector: 'app-chat-message-list',
  templateUrl: './chat-message-list.component.html',
  styleUrls: ['./chat-message-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChatMessageListComponent {

  @Input()
  public messages!: Observable<ChatMessage[]>;

}
