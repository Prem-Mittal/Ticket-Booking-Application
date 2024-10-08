import { Component, OnInit } from '@angular/core';
import { UsersService } from './components/auth/services/users.service';
import { User } from './components/auth/models/user.model';
import { UpdateEventModel } from './components/auth/models/update-event.model';
import { EventsService } from './components/auth/services/events.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  user?:User;
  event?:UpdateEventModel;
  constructor(private userService:UsersService, private eventService:EventsService){}
  ngOnInit(): void {
    this.user=this.userService.getuser();
    if(this.user){
      this.userService.setUser(this.user);
    }
    this.event=this.eventService.getEvent();
    if(this.event){
      this.eventService.setEvent(this.event);
    }
  }
}
