import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Event } from '../models/Event.model';
import { EventService } from '../services/event.service';
import { Router } from '@angular/router';
import { User } from '../../auth/models/user.model';
import { UsersService } from '../../auth/services/users.service';
@Component({
  selector: 'app-view-event',
  templateUrl: './view-event.component.html',
  styleUrls: ['./view-event.component.css']
})
export class ViewEventComponent implements OnInit {
  events$?: Observable<Event[]>;
  user?:User;
  constructor(private eventservice: EventService, private router :Router,private userService:UsersService) { }

  ngOnInit(): void {
    this.events$ = this.eventservice.getEvent();
    this.userService.user().subscribe({
      next:(response)=>{
        this.user=response;
      }
    })
  }
  onButtonClick(eventId:string,ticketPrice:number ){
    if(this.user===undefined){
      this.router.navigateByUrl('login');
    }else{
      this.router.navigate(['/booking',eventId,ticketPrice]);
    }
  }

  createEventClick(){
    if(this.user===undefined){
      this.router.navigateByUrl('login');
    }else{
      this.router.navigate(['/create-event']);
    }
  }

}
