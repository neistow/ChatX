import { ChangeDetectionStrategy, Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { provideHub } from '@neistow/ngx-signalr';
import { ChatStatisticsHub } from '../../api/hubs';
import { Observable } from 'rxjs';
import { ChatStatistics } from '../../api/models';

@Component({
  selector: 'app-chat-statistics',
  templateUrl: './chat-statistics.component.html',
  styleUrls: ['./chat-statistics.component.scss'],
  providers: [
    provideHub(ChatStatisticsHub.TOKEN, 'stats')
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChatStatisticsComponent implements OnInit, OnDestroy {

  public chatStats$: Observable<ChatStatistics>

  constructor(
    @Inject(ChatStatisticsHub.TOKEN) private chatStatsHub: ChatStatisticsHub
  ) {
    this.chatStats$ = this.chatStatsHub.listen.chatStatsUpdated;
  }

  public ngOnInit(): void {
    this.chatStatsHub.connect();
  }

  public ngOnDestroy(): void {
    this.chatStatsHub.disconnect();
  }

}
