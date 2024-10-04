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

  onFormSubmit(){
    this.loginUserSubscription =this.userService.loginUser(this.model)
    .subscribe({
      next : (response)=>{
        console.log("This was successful");
        console.log(response);
        this.cookieService.set('Authorization',`Bearer ${response.jwtToken}`,undefined,'/',undefined,true,'Strict');
        this.userService.setUser({
          email:response.username
        });
        this.router.navigateByUrl('/');
      }
    });
  }

  ngOnDestroy(): void {
    this.loginUserSubscription?.unsubscribe();
  }

}
