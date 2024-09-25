import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateEventComponent } from './components/pages/create-event/create-event.component';
import { ViewEventComponent } from './components/pages/view-event/view-event.component';
import { BookingComponent } from './components/pages/booking/booking.component';

const routes: Routes = [
  {
    path:'create-event',
    component: CreateEventComponent
  },
  {
    path:'event',
    component:ViewEventComponent
  },
  {
    path:'booking',
    component:BookingComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
