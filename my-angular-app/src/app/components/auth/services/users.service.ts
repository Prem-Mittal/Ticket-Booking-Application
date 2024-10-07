import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUser } from '../models/register_user.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginUserModel } from '../models/login_user.model';
import { LoginResponseModel } from '../models/login-response.model';
import { User } from '../models/user.model';
import { user_update_profile } from '../models/user-update.model';
import { BookingModel } from '../models/booking.model';
import { Event } from '../../pages/models/Event.model';
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
    localStorage.setItem('user',JSON.stringify(user));
    // localStorage.setItem('user-email',user.email);
    // localStorage.setItem('first-name',user.firstName);
    // localStorage.setItem('last-name',user.lastName);
    // localStorage.setItem('address',user.address);
    // localStorage.setItem('id',user.id);
    // localStorage.setItem('phoneNumber',user.phoneNumber);
  }

  //this method returns the user observable in this service class
  user():Observable<User|undefined>{
    return this.$user.asObservable();
  }
  
  //this method returns the user from local storage
  //it is created for extracting data whenever website is loaded and we have not logged in but user was already logged in
  getuser():User|undefined{
    const userdetails = localStorage.getItem('user');
    if(userdetails){
      const user= JSON.parse(userdetails);
      return user;
    }
    return undefined;
  }

  logout():void{
    localStorage.clear();
    this.cookieService.delete('Authorization','/');
    this.$user.next(undefined);
  }

  updateUser(id:string,model:user_update_profile):Observable<User>{
    return this.http.put<User>(`http://localhost:5077/api/User/${id}`,model,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

  getBookingbyUserId(id:string):Observable<BookingModel[]>{
    return this.http.get<BookingModel[]>(`http://localhost:5077/api/Booking/${id}`,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

  deleteBooking(id:string):Observable<BookingModel>{
    return this.http.delete<BookingModel>(`http://localhost:5077/api/Booking/${id}`,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

  getEventsbyUserId(id:string):Observable<Event[]>{
    return this.http.get<Event[]>(`http://localhost:5077/api/Event/${id}`,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

  deleteEvent(id:string):Observable<Event>{
    return this.http.delete<Event>(`http://localhost:5077/api/Event/${id}`,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }
}
