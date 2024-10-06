import { Component, OnInit } from '@angular/core';
import { UsersService } from './components/auth/services/users.service';
import { User } from './components/auth/models/user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  user?:User;
  constructor(private userService:UsersService){}
  ngOnInit(): void {
    this.user=this.userService.getuser();
    if(this.user){
      this.userService.setUser(this.user);
    }
  }
}
