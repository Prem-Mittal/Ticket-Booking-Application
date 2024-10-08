import { Component, OnInit } from '@angular/core';
import { CreateBooking } from '../models/create_booking.model';
import { Subscription } from 'rxjs';
import { EventService } from '../services/event.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../auth/models/user.model';
import { UsersService } from '../../auth/services/users.service';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit{
  user?:User;
  model:CreateBooking;
  private addBookingSubscription ?: Subscription
  ticketPrice: number = 0; 
  constructor(private bookingService: EventService, private router:Router,private route:ActivatedRoute,private userService:UsersService) {
    this.model = {
      name: "",
      phoneNumber:"",
      eventId: "",
      email:"",
      noOfTickets: 1,
      bookingTime:"2024-09-25T14:30:00Z",
      amount: 0 ,
      usersId:""
    }
  }
  ngOnInit(): void {
    this.userService.user().subscribe({
      next:(response)=>{
        this.user=response;
      }
    })
    if(this.user){
      this.model.phoneNumber=this.user.phoneNumber;
      this.model.email=this.user.email;
      this.model.name= this.user.firstName + this.user.lastName;
      this.model.usersId=this.user.id;
    }
    //fetching result from another component
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
        this.router.navigateByUrl('/');
      }
    });
  }

  ngOnDestroy(): void {
    this.addBookingSubscription?.unsubscribe();
  }
}
