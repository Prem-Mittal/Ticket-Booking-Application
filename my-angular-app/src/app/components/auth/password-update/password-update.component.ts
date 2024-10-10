import { Component, OnInit } from '@angular/core';
import { Password } from '../models/password.model';
import { Subscription } from 'rxjs';
import { UsersService } from '../services/users.service';
import { ActivatedRoute, Route, Router } from '@angular/router';

@Component({
  selector: 'app-password-update',
  templateUrl: './password-update.component.html',
  styleUrls: ['./password-update.component.css']
})
export class PasswordUpdateComponent implements OnInit{
  model:Password;
  userId?:string;
  private updatePasswordSubscription?:Subscription;
  constructor(private userService:UsersService, private route:ActivatedRoute,private router:Router) { 
    this.model = {
      oldPassword: '',
      newPassword: '',
      confirmPassword: ''
    };
  }
  ngOnInit(): void {
    const userId=this.route.snapshot.paramMap.get('id');
    if(userId){
      this.userId=userId;
    }
  }

  onSubmit() {
    if(this.userId){
      if (this.model.newPassword !== this.model.confirmPassword) {
        alert('New password and confirm password do not match!');
        return;
      }
      this.updatePasswordSubscription=this.userService.updatePassword(this.userId,this.model)
      .subscribe({
        next:(response)=>{
          console.log(response.message);
          this.router.navigateByUrl('/profile')
        }
      })
    }
  }

  ngOnDestroy(): void {
    if (this.updatePasswordSubscription) {
      this.updatePasswordSubscription.unsubscribe();
    }
  }
}
