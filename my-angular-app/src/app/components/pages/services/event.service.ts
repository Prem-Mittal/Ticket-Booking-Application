import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateEvent } from '../models/create_event.model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http:HttpClient) { 

  }
  addEvent(model:CreateEvent):Observable<void>{
    return this.http.post<void>("http://localhost:5077/api/Event",model);
  }
}
