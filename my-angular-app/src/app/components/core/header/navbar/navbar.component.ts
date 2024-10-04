import { User } from 'src/app/components/auth/models/user.model';
import { UsersService } from './../../../auth/services/users.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  user?:User;
  constructor(private userService:UsersService , private router: Router){}
  ngOnInit(): void {
    this.userService.user().subscribe({
      next:(response)=>{
        this.user=response;
      }
    })
    this.user=this.userService.getuser();
  }

  onLogout():void{
    this.userService.logout();
    this.router.navigateByUrl('/');
  }

}
