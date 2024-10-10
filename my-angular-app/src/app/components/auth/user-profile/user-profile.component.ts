import { Component, OnDestroy, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { UsersService } from '../services/users.service';
import { user_update_profile } from '../models/user-update.model';
import { map, Observable, Subscription } from 'rxjs';
import { BookingModel } from '../models/booking.model';
import { Event } from '../../pages/models/Event.model';
import { Router } from '@angular/router';
import { EventsService } from '../services/events.service';
@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit, OnDestroy {
  user?: User;
  model: user_update_profile;
  booking$?: Observable<BookingModel[]>;
  event$?: Observable<Event[]>
  private updateUserSubscription?: Subscription

  constructor(private userService: UsersService, private router: Router, private eventService: EventsService) {
    this.model = {
      firstName: "",
      lastName: "",
      address: "",
      phoneNumber: ""
    }
  }

  ngOnInit(): void {
    this.userService.user().subscribe({
      next: (response) => {
        this.user = response;
      }
    })
    if (this.user) {
      this.model.firstName = this.user.firstName;
      this.model.lastName = this.user.lastName;
      this.model.address = this.user.address;
      this.model.phoneNumber = this.user.phoneNumber;

      this.booking$ = this.userService.getBookingbyUserId(this.user.id).pipe(
        map(response => {
          console.log(response.message);  // Log  the message 
          return response.bookings;       // Return only the bookings array for further use
        })
      );

      this.event$ = this.userService.getEventsbyUserId(this.user.id).pipe(
        map((response)=>{
          console.log(response.message);
          console.log(response);
          return response.events;
        })
      );
    }
  }

  //method for updating the user details
  updateUserDetails() {
    if (this.user) {
      this.updateUserSubscription = this.userService.updateUser(this.user.id, this.model)
        .subscribe({
          next: (response) => {
            console.log(response.message);
          }
        }
      );
      localStorage.clear();
      this.userService.setUser({
        email: this.user.email,
        firstName: this.model.firstName,
        lastName: this.model.lastName,
        address: this.model.address,
        phoneNumber: this.model.phoneNumber,
        id: this.user.id
      });
    }
  }

  //Deleting the existing Booking
  deleteBooking(bookingId: string) {
    this.userService.deleteBooking(bookingId).subscribe({
      next: (response) => {
        console.log(response.message); // Log the success message
        console.log("Deleted Booking Details:", response.booking); // Log the deleted booking details
        this.reloadBookings(); // Reload the bookings to reflect the deletion
      },
      error: (err) => {
        console.error("Error deleting booking", err); // Log the error message
      }
    });
  }


  deleteEvent(eventId: string) {
    this.userService.deleteEvent(eventId).subscribe({
      next: (response) => {
        console.log(response.message);
        this.reloadEvents();
        this.reloadBookings();
      },
      error: (err) => {
        console.error("Error deleting booking", err);
      }
    });
  }

  modifyEvent(event: Event, id: string) {
    if(this.user){
      this.eventService.setEvent({
        eventName: event.eventName,
        description: event.description,
        eventDate: event.eventDate,
        eventTime: event.eventTime,
        location: event.location,
        ticketPrice: event.ticketPrice,
        ticketQuantity: event.ticketQuantity
      });
      this.router.navigate(['/update-event', id, this.user.id]);
    }
    
  }

  //Reloading Bookings
  reloadBookings() {
    if (this.user) {
      this.booking$ = this.userService.getBookingbyUserId(this.user.id).pipe(
        map(response => {
          return response.bookings;
        })
      );

    }
  }

  reloadEvents() {
    if (this.user) {
      this.event$ = this.userService.getEventsbyUserId(this.user.id).pipe(
        map((response)=>{
          console.log(response.message);
          return response.events;
        })
      );
    }
  }

  navigateToPasswordUpdate() {
    this.router.navigate(['/update-password', this.user?.id]);
  }

  ngOnDestroy(): void {
    this.updateUserSubscription?.unsubscribe();
  }
}
