import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Event } from '../models/Event.model';
import { EventService } from '../services/event.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-view-event',
  templateUrl: './view-event.component.html',
  styleUrls: ['./view-event.component.css']
})
export class ViewEventComponent implements OnInit {
  events$?: Observable<Event[]>;

  constructor(private eventservice: EventService, private router :Router) { }

  ngOnInit(): void {
    this.events$ = this.eventservice.getEvent();
  }
  onButtonClick(eventId:string,ticketPrice:number ){
    this.router.navigate(['/booking',eventId,ticketPrice]);
  }

}
