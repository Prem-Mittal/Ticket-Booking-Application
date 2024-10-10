import { Component, OnDestroy } from '@angular/core';
import { LoginUserModel } from '../models/login_user.model';
import { Subscription } from 'rxjs';
import { UsersService } from '../services/users.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnDestroy {
  model: LoginUserModel;
  private loginUserSubscription?: Subscription

  constructor(private userService: UsersService, private cookieService:CookieService, private router: Router) {
    this.model = {
      username: "",
      password: ""
    };
  }

  onFormSubmit() {
    this.loginUserSubscription = this.userService.loginUser(this.model)
    .subscribe({
        next: (response) => {
          if(response.message==="User Logged In"){
            console.log(response.message);
            this.cookieService.set('Authorization', `Bearer ${response.result.jwtToken}`, undefined, '/', undefined, true, 'Strict');
            this.userService.setUser({
                email: response.result.username,
                firstName: response.result.firstName,
                lastName: response.result.lastName,
                address: response.result.address,
                phoneNumber: response.result.phoneNumber,
                id: response.result.id
            });
            this.router.navigateByUrl('/');
          }
          else{
            console.log(response.message);
          } 
        },
        error: (err) => {
            console.error("Login failed", err);
        }
    });
}


  ngOnDestroy(): void {
    this.loginUserSubscription?.unsubscribe();
  }

}
