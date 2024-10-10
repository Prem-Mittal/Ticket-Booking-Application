import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Event } from '../models/Event.model';
import { EventService } from '../services/event.service';
import { Router } from '@angular/router';
import { User } from '../../auth/models/user.model';
import { UsersService } from '../../auth/services/users.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-view-event',
  templateUrl: './view-event.component.html',
  styleUrls: ['./view-event.component.css']
})
export class ViewEventComponent implements OnInit {
  events$?: Observable<Event[]>; // Keep this as an observable
  user?: User;

  constructor(
    private eventservice: EventService, 
    private router: Router, 
    private userService: UsersService
  ) { }

  ngOnInit(): void {
    // Map the response to extract the event array, wrapping it as an Observable
    this.events$ = this.eventservice.getEvent().pipe(
      map((response) => {
        console.log(response.message);
        return response.event}) // Extract only the event array
    );

    // Subscribe to get user details
    this.userService.user().subscribe({
      next: (response) => {
        this.user = response; // Assign user details to `user`
      }
    });
  }

  // Function for booking a ticket
  onButtonClick(eventId: string, ticketPrice: number) {
    if (this.user === undefined) {
      this.router.navigateByUrl('login'); // Redirect to login if user is undefined
    } else {
      this.router.navigate(['/booking', eventId, ticketPrice]); // Redirect to booking page with eventId and ticketPrice
    }
  }

  // Function for adding event
  createEventClick() {
    if (this.user === undefined) {
      this.router.navigateByUrl('login'); // Redirect to login if user is undefined
    } else {
      this.router.navigate(['/create-event']); // Redirect to create event page
    }
  }
}
