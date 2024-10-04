import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUser } from '../models/register_user.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginUserModel } from '../models/login_user.model';
import { LoginResponseModel } from '../models/login-response.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  $user = new BehaviorSubject<User | undefined>(undefined);
  constructor(private http:HttpClient, private cookieService:CookieService) { }

  registerUser(model:RegisterUser):Observable<void>{
    return this.http.post<void>("http://localhost:5077/api/User",model);
  }

  loginUser(model:LoginUserModel):Observable<LoginResponseModel>{
    return this.http.post<LoginResponseModel>("http://localhost:5077/api/User/Login",model)
  }

  setUser(user: User):void{
    this.$user.next(user);
    localStorage.setItem('user-email',user.email);
  }

  user():Observable<User|undefined>{
    return this.$user.asObservable();
  }

  getuser():User|undefined{
    const email = localStorage.getItem('user-email');
    if(email){
      const user: User={
        email:email
      };
      return user;
    }
    return undefined;
  }
  logout():void{
    localStorage.clear();
    this.cookieService.delete('Authorization','/');
    this.$user.next(undefined);
  }
}
