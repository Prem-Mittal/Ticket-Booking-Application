import { Component, OnInit } from '@angular/core';
import { UpdateEventModel } from '../models/update-event.model';
import { UsersService } from '../services/users.service';
import { EventsService } from '../services/events.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-update-event',
  templateUrl: './update-event.component.html',
  styleUrls: ['./update-event.component.css']
})
export class UpdateEventComponent implements OnInit {
   event?:UpdateEventModel
   model:UpdateEventModel;
   eventId?: string ;
   private updateEventSubscription?:Subscription;
   constructor(private eventService:EventsService, private userService:UsersService,private route:ActivatedRoute, private router:Router){
    this.model={
      eventName: "",
      description: "",
      eventDate: "",
      eventTime: "",
      location: "",
      ticketPrice: 1,
      ticketQuantity: 1
    }
   }
  ngOnInit(): void {
    this.eventService.event().subscribe({
      next:(response)=>{
        this.event=response;
      }
    })
    if(this.event){
      this.model.eventName=this.event.eventName;
      this.model.description=this.event.description;
      this.model.eventDate=this.event.eventDate;
      this.model.eventTime=this.event.eventTime;
      this.model.location=this.event.location;
      this.model.ticketPrice=this.event.ticketPrice;
      this.model.ticketQuantity=this.event.ticketQuantity;
    }
    const eventId=this.route.snapshot.paramMap.get('id');
    if(eventId){
      this.eventId=eventId;
    }
  }
   
  updateEvent(){
    if(this.eventId){
      this.updateEventSubscription = this.userService.updateEvent(this.eventId,this.model)
      .subscribe({
        next : (response)=>{
          console.log("This was successful");
          localStorage.removeItem('Event');
          this.router.navigateByUrl('profile')
        }
      });
      this.eventService.setEvent(this.model); 
      
    } 
  }

  ngOnDestroy(): void {
    if (this.updateEventSubscription) {
      this.updateEventSubscription.unsubscribe();
    }
  }
}
