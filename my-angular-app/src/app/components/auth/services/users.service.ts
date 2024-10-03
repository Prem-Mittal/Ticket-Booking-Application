import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUser } from '../models/register_user.model';
import { Observable } from 'rxjs';
import { LoginUserModel } from '../models/login_user.model';
import { LoginResponseModel } from '../models/login-response.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http:HttpClient) { }

  registerUser(model:RegisterUser):Observable<void>{
    return this.http.post<void>("http://localhost:5077/api/User",model);
  }

  loginUser(model:LoginUserModel):Observable<LoginResponseModel>{
    return this.http.post<LoginResponseModel>("http://localhost:5077/api/User/Login",model)
  }
  
}
