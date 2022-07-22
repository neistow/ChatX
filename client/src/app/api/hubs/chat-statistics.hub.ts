import { Hub } from '@neistow/ngx-signalr';
import { InjectionToken } from '@angular/core';
import { Observable } from 'rxjs';
import { ChatStatistics } from '../models';

export type ChatStatisticsHub = Hub<{}, ChatStatisticsHub.Events>

export namespace ChatStatisticsHub {

  export const TOKEN = new InjectionToken<ChatStatisticsHub>('Chat Statistics Hub')

  export interface Events {
    chatStatsUpdated: Observable<ChatStatistics>;
  }
}
