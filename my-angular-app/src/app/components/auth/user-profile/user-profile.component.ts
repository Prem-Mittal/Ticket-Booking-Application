import { Component, OnDestroy, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { UsersService } from '../services/users.service';
import { user_update_profile } from '../models/user-update.model';
import { Observable, Subscription } from 'rxjs';
import { BookingModel } from '../models/booking.model';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit, OnDestroy {
  user?:User;
  model:user_update_profile;
  booking$?: Observable<BookingModel[]>;
  private updateUserSubscription?: Subscription
  
  constructor(private userService:UsersService){
    this.model={
      firstName:"",
      lastName:"",
      address:"",
      phoneNumber:""
    }
  }
  
  ngOnInit(): void {
    this.userService.user().subscribe({
      next:(response)=>{
        this.user=response;
      }
    })
    if(this.user){
      this.model.firstName=this.user.firstName;
      this.model.lastName=this.user.lastName;
      this.model.address=this.user.address;
      this.model.phoneNumber=this.user.phoneNumber;
      this.booking$=this.userService.getBookingbyUserId(this.user?.id);
    }
  }

  updateUserDetails(){
    if(this.user){
      this.updateUserSubscription =this.userService.updateUser(this.user.id,this.model)
      .subscribe({
        next : (response)=>{
          console.log("This was successful");
        }
      });
      localStorage.clear();
      this.userService.setUser({
        email: this.user.email,
        firstName:this.model.firstName,
        lastName:this.model.lastName,
        address:this.model.address,
        phoneNumber:this.model.phoneNumber,
        id:this.user.id
      }); 
    } 
  }

  deleteBooking(bookingId: string) {
    this.userService.deleteBooking(bookingId).subscribe({
      next: (response) => {
        console.log("Booking deleted successfully", response);
        this.reloadBookings();  
      },
      error: (err) => {
        console.error("Error deleting booking", err);
      }
    });
  }

  reloadBookings() {
    if(this.user){
      this.booking$ = this.userService.getBookingbyUserId(this.user?.id);
    }
  }

  ngOnDestroy(): void {
    this.updateUserSubscription?.unsubscribe();
  }
}
