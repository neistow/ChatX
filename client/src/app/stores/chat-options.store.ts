import { Injectable } from '@angular/core';
import { ChatOptions } from '../components';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatOptionsStore {

  private _chatOptions = new BehaviorSubject<ChatOptions | null>(null);
  public chatOptions$ = this._chatOptions.asObservable();

  public get chatOptions(): ChatOptions | null {
    return this._chatOptions.value;
  }

  public saveChatOptions(options: ChatOptions | null) {
    this._chatOptions.next(options);
  }
}
