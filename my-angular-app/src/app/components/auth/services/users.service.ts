import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUser } from '../models/register_user.model';
import { BehaviorSubject, catchError, map, Observable, of } from 'rxjs';
import { LoginUserModel } from '../models/login_user.model';
import { LoginResponseModel } from '../models/login-response.model';
import { User } from '../models/user.model';
import { user_update_profile } from '../models/user-update.model';
import { BookingModel } from '../models/booking.model';
import { Event } from '../../pages/models/Event.model';
import { UpdateEventModel } from '../models/update-event.model';
import { Password } from '../models/password.model';
@Injectable({
  providedIn: 'root'
})
export class UsersService {
  $user = new BehaviorSubject<User | undefined>(undefined);
  constructor(private http:HttpClient, private cookieService:CookieService) { }

  registerUser(model: RegisterUser): Observable<string> {
    return this.http.post<void>(`http://localhost:5077/api/User/Register`, model).pipe(
      map(() => "User Created Successfully"), 
      catchError((error) => {
        let errorMessage = "An error occurred during registration.";
        if (error.error && Array.isArray(error.error)) {
          errorMessage = error.error.map((err: any) => err.description || err).join(", ");
        }
        return of(errorMessage); 
      })
    );
  }

  loginUser(model:LoginUserModel):Observable<LoginResponseModel>{
    return this.http.post<LoginResponseModel>("http://localhost:5077/api/User/Login",model);
  }

  //function for setting info in localstoarge and user behaviour subject
  setUser(user: User):void{
    this.$user.next(user);
    localStorage.setItem('user',JSON.stringify(user));
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
    this.$user.next(undefined); //By calling next(undefined), you're notifying any subscribers that the user is no longer logged in.
  }

  updateUser(id:string,model:user_update_profile):Observable<User>{
    return this.http.put<User>(`http://localhost:5077/api/User/Update/${id}`,model,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

  updateEvent(id:string,model:UpdateEventModel):Observable<string>{
    return this.http.put<void>(`http://localhost:5077/api/Event/UpdateEvent/${id}`,model,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    })
    .pipe(
      map(() => "Event Updated Successfully"), 
      catchError((error) => {
        let errorMessage = "An error occurred during updation.";
        if (error.error && Array.isArray(error.error)) {
          errorMessage = error.error.map((err: any) => err.description || err).join(", ");
        }
        return of(errorMessage); 
      })
    );;
  }

  getBookingbyUserId(id:string):Observable<BookingModel[]>{
    return this.http.get<BookingModel[]>(`http://localhost:5077/api/Booking/ShowBookingbyUserId/${id}`,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

  deleteBooking(id:string):Observable<BookingModel>{
    return this.http.delete<BookingModel>(`http://localhost:5077/api/Booking/DeleteBooking/${id}`,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

  getEventsbyUserId(id:string):Observable<Event[]>{
    return this.http.get<Event[]>(`http://localhost:5077/api/Event/GetEventByUserId/${id}`,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

  deleteEvent(id:string):Observable<Event>{
    return this.http.delete<Event>(`http://localhost:5077/api/Event/DeleteEvent/${id}`,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    });
  }

  updatePassword(id:string, model:Password):Observable<string>{
    return this.http.post<void>(`http://localhost:5077/api/User/ChangePassword/${id}`,model,{
      headers:{
        'Authorization':this.cookieService.get('Authorization')
      }
    })
    .pipe(
      map(() => "Event Updated Successfully"), 
      catchError((error) => {
        let errorMessage = "An error occurred during updation.";
        if (error.error && Array.isArray(error.error)) {
          errorMessage = error.error.map((err: any) => err.description || err).join(", ");
        }
        return of(errorMessage); 
      })
    );;
  }
}
