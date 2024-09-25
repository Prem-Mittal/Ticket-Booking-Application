import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateEvent } from '../models/create_event.model';
import { Observable } from 'rxjs';
import { Event } from '../models/Event.model';
import { CreateBooking } from '../models/create_booking.model';
@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http:HttpClient) { 

  }
  addEvent(model:CreateEvent):Observable<void>{
    return this.http.post<void>("http://localhost:5077/api/Event",model);
  }

  getEvent():Observable<Event[]>{
    return this.http.get<Event[]>("http://localhost:5077/api/Event");
  }

  createBooking(model:CreateBooking):Observable<void>{
    return this.http.post<void>("http://localhost:5077/api/Booking",model);
  }

}
