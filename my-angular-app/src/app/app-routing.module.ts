import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateEventComponent } from './components/pages/create-event/create-event.component';
import { ViewEventComponent } from './components/pages/view-event/view-event.component';
import { BookingComponent } from './components/pages/booking/booking.component';
import { RegisterUserComponent } from './components/auth/register-user/register-user.component';
import { LoginComponent } from './components/auth/login/login.component';
import { UserProfileComponent } from './components/auth/user-profile/user-profile.component';
import { UpdateEventComponent } from './components/auth/update-event/update-event.component';
import { PasswordUpdateComponent } from './components/auth/password-update/password-update.component';
import { authGuardGuard } from './components/guards/auth-guard.guard';

const routes: Routes = [
  {
    path:'',
    component:ViewEventComponent
  },
  {
    path:'update-password/:id',
    component:PasswordUpdateComponent,
    canActivate:[authGuardGuard]
  },
  {
    path:'create-event',
    component: CreateEventComponent,
    canActivate:[authGuardGuard]
  },
  {
    path:'event',
    component:ViewEventComponent
  },
  {
    path:'booking/:eventId/:price',
    component:BookingComponent,
    canActivate:[authGuardGuard]
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
    component:UserProfileComponent,
    canActivate:[authGuardGuard]
  },
  {
    path:'update-event/:id',
    component:UpdateEventComponent,
    canActivate:[authGuardGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
