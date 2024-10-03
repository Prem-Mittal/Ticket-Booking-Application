import { Component, OnDestroy } from '@angular/core';
import { LoginUserModel } from '../models/login_user.model';
import { Subscription } from 'rxjs';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnDestroy {
  model: LoginUserModel;
  private loginUserSubscription?: Subscription

  constructor(private userService: UsersService) {
    this.model = {
      username: "",
      password: ""
    };
  }

  onFormSubmit(){
    this.loginUserSubscription =this.userService.loginUser(this.model)
    .subscribe({
      next : (response)=>{
        console.log("This was successful");
        console.log(response);
      }
    });
  }

  ngOnDestroy(): void {
    this.loginUserSubscription?.unsubscribe();
  }

}
