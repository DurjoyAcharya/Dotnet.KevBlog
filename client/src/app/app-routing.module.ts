import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BusinessesComponent } from './businesses/businesses.component';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { InterviewQuestionsComponent } from './programming/interview-questions/interview-questions.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'members',  component: MemberListComponent},
  { path: 'members/:id',  component: MemberDetailComponent},
  { path: 'lists', component: ListsComponent },
  { path: 'messages', component: MessagesComponent },
  { path: 'businesses', component: BusinessesComponent },
  { path: 'interview-Questions', component: InterviewQuestionsComponent },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
