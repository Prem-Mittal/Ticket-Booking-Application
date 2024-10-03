import { Component, OnDestroy } from '@angular/core';
import { RegisterUser } from '../models/register_user.model';
import { Subscription } from 'rxjs';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnDestroy {

  model: RegisterUser;
  private addUserSubscription?: Subscription

  constructor(private userService: UsersService) {
    this.model = {
      userName: "",
      password: "",
      firstName: "",
      lastName: "",
      address: "",
      phoneNumber: ""
    };
  }

  onFormSubmit(){
    this.addUserSubscription =this.userService.registerUser(this.model)
    .subscribe({
      next : (response)=>{
        console.log("This was successful");
      }
    });
  }

  ngOnDestroy(): void {
    this.addUserSubscription?.unsubscribe();
  }

}
