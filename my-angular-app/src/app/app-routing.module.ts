import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateEventComponent } from './components/pages/create-event/create-event.component';
import { ViewEventComponent } from './components/pages/view-event/view-event.component';
import { BookingComponent } from './components/pages/booking/booking.component';
import { RegisterUserComponent } from './components/auth/register-user/register-user.component';
import { LoginComponent } from './components/auth/login/login.component';
import { UserProfileComponent } from './components/auth/user-profile/user-profile.component';

const routes: Routes = [
  {
    path:'',
    component:ViewEventComponent
  },
  {
    path:'create-event',
    component: CreateEventComponent
  },
  {
    path:'event',
    component:ViewEventComponent
  },
  {
    path:'booking/:eventId/:price',
    component:BookingComponent
  },
  {
    path:'register',
    component:RegisterUserComponent
  },
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'profile',
    component:UserProfileComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
