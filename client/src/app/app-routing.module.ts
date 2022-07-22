import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatViewComponent, SearchingChatViewComponent, StartChatViewComponent } from './components';


const routes: Routes = [
  { component: StartChatViewComponent, path: '' },
  { component: SearchingChatViewComponent, path: 'searching' },
  { component: ChatViewComponent, path: 'chat' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
