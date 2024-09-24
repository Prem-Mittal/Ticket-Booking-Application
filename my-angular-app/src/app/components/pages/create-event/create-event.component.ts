import { Component, OnDestroy } from '@angular/core';
import { CreateEvent } from '../models/create_event.model';
import { Subscription } from 'rxjs';
import { EventService } from '../services/event.service';

@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent implements OnDestroy {
  model: CreateEvent;
  private addEventSubscription?: Subscription
  constructor(private eventService: EventService) {
    this.model = {
      EventName: "",
      Description: "",
      EventDate: "",
      EventTime: "",
      Location: "",
      TicketPrice: 0,
      TicketQuantity: 0,
    }
  }

  onFormSubmit(){
    this.addEventSubscription =this.eventService.addEvent(this.model)
    .subscribe({
      next : (response)=>{
        console.log("This was successful");
      }
    });
  }

  ngOnDestroy(): void {
    this.addEventSubscription?.unsubscribe();
  }

}
