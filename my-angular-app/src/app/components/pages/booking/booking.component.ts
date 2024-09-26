import { Component, OnInit } from '@angular/core';
import { CreateBooking } from '../models/create_booking.model';
import { Subscription } from 'rxjs';
import { EventService } from '../services/event.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit{
  model:CreateBooking;
  private addBookingSubscription ?: Subscription
  ticketPrice: number = 0; 
  constructor(private bookingService: EventService, private route:ActivatedRoute) {
    this.model = {
      name: "",
      phoneNumber: "",
      eventId: "",
      email:"",
      noOfTickets: 1,
      bookingTime:"2024-09-25T14:30:00Z",
      amount: 0 
    }
  }
  ngOnInit(): void {
    const eventId=this.route.snapshot.paramMap.get('eventId');
    const ticketPrice= Number(this.route.snapshot.paramMap.get('price'));
    this.ticketPrice=ticketPrice;  
    if (eventId) {
      this.model.eventId = eventId;
    }
    if (ticketPrice) {
      this.model.amount = ticketPrice;
    }
  }

  onticketChange(){
    this.model.amount = this.ticketPrice * this.model.noOfTickets;
  }

  onFormSubmit(){
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
