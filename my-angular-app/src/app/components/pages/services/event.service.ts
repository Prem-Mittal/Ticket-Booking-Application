import { CookieService } from 'ngx-cookie-service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateEvent } from '../models/create_event.model';
import { Observable } from 'rxjs';
import { Event } from '../models/Event.model';
import { CreateBooking } from '../models/create_booking.model';
import { AfterEvent } from '../models/after_event.model';
@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http:HttpClient, private cookieService:CookieService) { }
  
  addEvent(model:CreateEvent):Observable<AfterEvent>{
    return this.http.post<AfterEvent>("http://localhost:5077/api/Event",model,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

  getEvent():Observable<Event[]>{
    return this.http.get<Event[]>("http://localhost:5077/api/Event");
  }

  createBooking(model:CreateBooking):Observable<void>{
    return this.http.post<void>("http://localhost:5077/api/Booking",model);
  }

}
