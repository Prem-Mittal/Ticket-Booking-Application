import { Component } from '@angular/core';
import { CreateBooking } from '../models/create_booking.model';
import { Subscription } from 'rxjs';
import { EventService } from '../services/event.service';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent {
  model:CreateBooking;
  private addBookingSubscription?: Subscription
  constructor(private bookingService: EventService) {
    this.model = {
      name: "",
      phoneNumber: "",
      eventId: "",
      noOfTickets: 0,
      bookingTime: "2024-09-25T14:30:00Z",
      amount: 0 
    }
  }

  onFormSubmit(){
    console.log(this.model);
    this.addBookingSubscription =this.bookingService.createBooking(this.model)
    .subscribe({
      next : (response)=>{
        console.log("This was successful");
      }
    });
  }

  ngOnDestroy(): void {
    this.addBookingSubscription?.unsubscribe();
  }
}
