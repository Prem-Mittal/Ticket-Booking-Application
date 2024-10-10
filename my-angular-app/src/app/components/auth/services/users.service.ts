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
  constructor(private http: HttpClient, private cookieService: CookieService) { }

  //method for registering the user
  registerUser(model: RegisterUser): Observable<string> {
    return this.http.post<{ Message: string }>(`http://localhost:5077/api/User/Register`, model).pipe(
      map(response => response.Message || "User Created Successfully"), // Get message from response
      catchError((error) => {
        let errorMessage = "An error occurred during registration.";
        // Check if the error response contains a structured error message
        if (error.error && error.error.Message) {
          errorMessage = error.error.Message; // Use the structured error message from the server
        }
        return of(errorMessage); // Return the error message as an observable
      })
    );
  }

  //method for logging in the user
  loginUser(model: LoginUserModel): Observable<LoginResponseModel> {
    return this.http.post<LoginResponseModel>("http://localhost:5077/api/User/Login", model).pipe(
      map((response: LoginResponseModel) => {
        return response;
      }),
      catchError((error) => {
        let errorMessage = "An error occurred during login.";
        if (error.error && error.error.message) {
          errorMessage = error.error.message;
        }
        return of({
          message: errorMessage,
          result: {
            jwtToken: '',
            username: '',
            firstName: '',
            lastName: '',
            phoneNumber: '',
            id: '',
            address: ''
          }
        } as LoginResponseModel);
      })
    );
  }

  //function for setting info in localstoarge and user behaviour subject
  setUser(user: User): void {
    this.$user.next(user);
    localStorage.setItem('user', JSON.stringify(user));
  }

  //this method returns the user observable in this service class
  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }

  //this method returns the user from local storage
  //it is created for extracting data whenever website is loaded and we have not logged in but user was already logged in
  getuser(): User | undefined {
    const userdetails = localStorage.getItem('user');
    if (userdetails) {
      const user = JSON.parse(userdetails);
      return user;
    }
    return undefined;
  }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authorization', '/');
    this.$user.next(undefined); //By calling next(undefined), you're notifying any subscribers that the user is no longer logged in.
  }

  //method for updating the user
  updateUser(id: string, model: user_update_profile): Observable<{ message: string; result: User }> {
    return this.http.put<{ message: string; result: User }>(`http://localhost:5077/api/User/Update/${id}`, model, {
      headers: {
        'Authorization': this.cookieService.get('Authorization')
      }
    });
  }

  updateEvent(id: string, model: UpdateEventModel, userId: string): Observable<{ Message: string, Event: Event }> {
    return this.http.put<{ Message: string, Event: Event }>(`http://localhost:5077/api/Event/UpdateEvent/${userId}/${id}`, model, {
      headers: {
        'Authorization': this.cookieService.get('Authorization')
      }
    }).pipe(
      map((response) => ({ Message: response.Message, Event: response.Event })),  
      catchError((error) => {
        let errorMessage = "An error occurred during update.";
        if (error.error && Array.isArray(error.error)) {
          errorMessage = error.error.map((err: any) => err.description || err).join(", ");
        }
        return of({ Message: errorMessage, Event: null as any });  // Handle error case
      })
    );
  }
  

  //Showing booking based on userID
  getBookingbyUserId(id: string): Observable<{ message: string, bookings: BookingModel[] }> {
    return this.http.get<{ message: string, bookings: BookingModel[] }>(`http://localhost:5077/api/Booking/ShowBookingbyUserId/${id}`, {
      headers: {
        'Authorization': this.cookieService.get('Authorization')
      }
    });
  }

  //deleting a booking created by user
  deleteBooking(id: string): Observable<{ message: string, booking: BookingModel }> {
    return this.http.delete<{ message: string, booking: BookingModel }>(`http://localhost:5077/api/Booking/DeleteBooking/${id}`, {
      headers: {
        'Authorization': this.cookieService.get('Authorization')
      }
    });
  }

//get events by userId
  getEventsbyUserId(id: string): Observable<{message:string,events:Event[]}> {
    return this.http.get<{message:string,events:Event[]}>(`http://localhost:5077/api/Event/GetEventByUserId/${id}`, {
      headers: {
        'Authorization': this.cookieService.get('Authorization')
      }
    });
  }

  deleteEvent(id: string): Observable<{ message: string, Event: Event }> {
    return this.http.delete<{ message: string, Event: Event }>(`http://localhost:5077/api/Event/DeleteEvent/${id}`, {
      headers: {
        'Authorization': this.cookieService.get('Authorization')
      }
    })
    .pipe(
      map((response) => ({ message: response.message, Event: response.Event })),  
      catchError((error) => {
        let errorMessage = "An error occurred during update.";
        if (error.error && Array.isArray(error.error)) {
          errorMessage = error.error.map((err: any) => err.description || err).join(", ");
        }
        return of({ message: errorMessage, Event: null as any });  // Handle error case
      })
    );
  }

  //method for updating password
  updatePassword(id: string, model: Password): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`http://localhost:5077/api/User/ChangePassword/${id}`, model, {
      headers: {
        'Authorization': this.cookieService.get('Authorization')
      }
    }).pipe(
      map((response) => {
        // The response contains the success message
        return { message: response.message };
      }),
      catchError((error) => {
        let errorMessage = "An error occurred during password change.";
        if (error.error && error.error.Message) {
          errorMessage = error.error.Message; // Get the error message sent from the server
        }
        // Return the error message as part of the Observable
        return of({ message: errorMessage });
      })
    );
  }
}
