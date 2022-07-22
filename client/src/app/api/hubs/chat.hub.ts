import { Observable } from 'rxjs';
import { Message, UserInfo } from '../models';
import { InjectionToken } from '@angular/core';
import { Hub } from '@neistow/ngx-signalr';

export type ChatHub = Hub<ChatHub.Commands, ChatHub.Events>

export namespace ChatHub {

  export const TOKEN = new InjectionToken<ChatHub>('Chat Hub')

  export interface Commands {
    startSearch(request: StartSearchRequest): Observable<void>;

    stopSearch(): Observable<void>;

    cancelActiveConversation(): Observable<void>;

    sendMessage(request: SendMessageRequest): Observable<void>;

    sendTyping(): Observable<void>;
  }

  export interface Events {
    conversationStarted: Observable<void>;
    conversationEnded: Observable<void>;
    messageReceived: Observable<Message>;
    strangerTyping: Observable<void>;
  }

  export interface StartSearchRequest {
    userInfo: UserInfo;
    searchPreferences: UserInfo;
  }

  export interface SendMessageRequest {
    text: string;
  }
}
