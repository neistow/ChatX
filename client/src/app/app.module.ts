import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxSignalrModule } from '@neistow/ngx-signalr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LogLevel } from '@microsoft/signalr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  ChatMessageComponent,
  ChatMessageListComponent,
  ChatOptionsFormComponent,
  ChatStatisticsComponent,
  ChatViewComponent,
  MessageInputComponent,
  SearchingChatViewComponent,
  StartChatViewComponent,
  TypingIndicatorComponent,
  UserInfoControlComponent
} from './components';
import { MaterialModule } from './modules/material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PickerModule } from '@ctrl/ngx-emoji-mart';


@NgModule({
  declarations: [
    AppComponent,
    StartChatViewComponent,
    UserInfoControlComponent,
    ChatOptionsFormComponent,
    SearchingChatViewComponent,
    ChatViewComponent,
    ChatMessageComponent,
    MessageInputComponent,
    TypingIndicatorComponent,
    ChatMessageListComponent,
    ChatStatisticsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,

    CommonModule,
    ReactiveFormsModule,

    MaterialModule,
    NgxSignalrModule.withConfig({
      baseUrl: 'https://localhost:5001',
      logLevel: LogLevel.Debug,
    }),
    PickerModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
