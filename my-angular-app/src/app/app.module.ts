import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BookingComponent } from './components/pages/booking/booking.component';
import { RegisterUserComponent } from './components/auth/register-user/register-user.component';
import { CreateEventComponent } from './components/pages/create-event/create-event.component';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from './components/core/header/navbar/navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    BookingComponent,
    RegisterUserComponent,
    CreateEventComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
