import { ChangeDetectionStrategy, Component, HostBinding, Inject, OnDestroy, OnInit } from '@angular/core';
import { ChatHub } from './api/hubs';
import { DOCUMENT } from '@angular/common';
import { provideHub } from '@neistow/ngx-signalr';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [
    provideHub(ChatHub.TOKEN, 'chat')
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements OnInit, OnDestroy {

  @HostBinding('style.height')
  public get height(): string {
    const innerHeight = this.doc.defaultView?.innerHeight;
    return innerHeight ? `${innerHeight}px` : '100vh';
  };

  constructor(
    @Inject(DOCUMENT) private doc: Document,
    @Inject(ChatHub.TOKEN) private chatHub: ChatHub
  ) {
  }

  public ngOnInit(): void {
    this.chatHub.connect();
  }

  public ngOnDestroy(): void {
    this.chatHub.disconnect();
  }

}
