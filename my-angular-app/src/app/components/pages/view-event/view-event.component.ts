import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Event } from '../models/Event.model';
import { EventService } from '../services/event.service';
@Component({
  selector: 'app-view-event',
  templateUrl: './view-event.component.html',
  styleUrls: ['./view-event.component.css']
})
export class ViewEventComponent implements OnInit {
  events$?: Observable<Event[]>;
  constructor(private eventservice: EventService) { }
  ngOnInit(): void {
    this.events$ = this.eventservice.getEvent();
    // console.log(this.events$.value);
    this.events$.subscribe({
      next: (events: Event[]) => console.log(events), // Logs the emitted values (events array)
    });
  }

}
