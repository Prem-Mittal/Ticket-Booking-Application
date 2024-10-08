import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { UpdateEventModel } from '../models/update-event.model';

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  $eventdetails = new BehaviorSubject<UpdateEventModel | undefined>(undefined);
 
  event():Observable<UpdateEventModel|undefined>{
    return this.$eventdetails.asObservable();
  }

  setEvent(event: UpdateEventModel):void {
    this.$eventdetails.next(event);
    localStorage.setItem('Event',JSON.stringify(event));
  }

  getEvent():UpdateEventModel|undefined{
    const eventDetails = localStorage.getItem('Event');
    if(eventDetails){
      const event=JSON.parse(eventDetails);
      return event
    } 
    return undefined;
  }
  
}
