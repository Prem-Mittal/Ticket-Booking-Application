import { Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { CreateEvent } from '../models/create_event.model';
import { Subscription } from 'rxjs';
import { EventService } from '../services/event.service';
import { UsersService } from '../../auth/services/users.service';
import { User } from '../../auth/models/user.model';

@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent implements OnDestroy, OnInit {
  model: CreateEvent;
  user?: User;
  private addEventSubscription?: Subscription
  constructor(private eventService: EventService, private router: Router, private userService: UsersService) {
    this.model = {
      EventName: "",
      UsersId: "",
      Description: "",
      EventDate: "",
      EventTime: "",
      Location: "",
      TicketPrice: 0,
      TicketQuantity: 0,
    }
  }
  ngOnInit(): void {
    this.userService.user().subscribe(
      {
        next: (response) => {
          this.user = response;
        }
      }
    )
    if (this.user) {
      this.model.UsersId = this.user.id;
    }
  }

  onFormSubmit() {
    this.addEventSubscription = this.eventService.addEvent(this.model)
      .subscribe({
        next: (response) => {
          console.log("This was successful");
          this.router.navigateByUrl('/');
        }
      });
  }

  ngOnDestroy(): void {
    this.addEventSubscription?.unsubscribe();
  }

}
