import { CookieService } from 'ngx-cookie-service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateEvent } from '../models/create_event.model';
import { Observable } from 'rxjs';
import { Event } from '../models/Event.model';
import { CreateBooking } from '../models/create_booking.model';
import { BookingModel } from '../../auth/models/booking.model';
@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http:HttpClient, private cookieService:CookieService) { }
  
  //method for adding the event
  addEvent(model:CreateEvent):Observable<{message:string,event:Event}>{
    return this.http.post<{message:string,event:Event}>("http://localhost:5077/api/Event/Create",model,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

  //method for getting all bookings
  getEvent():Observable<{message:string,event:Event[]}>{
    return this.http.get<{message:string,event:Event[]}>("http://localhost:5077/api/Event/GetAllEvents");
  }

  //method for creating  booking
  createBooking(model:CreateBooking):Observable<{message: string; booking: BookingModel}>{
    return this.http.post<{message: string; booking: BookingModel}>("http://localhost:5077/api/Booking/AddBooking",model,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

}
