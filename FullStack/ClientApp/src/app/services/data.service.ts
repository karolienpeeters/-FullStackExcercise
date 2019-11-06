import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import{Headers,RequestOptions} from '@angular/http';
import { environment } from 'src/environments/environment';

import { AuthService, TOKEN_NAME } from './auth.service';

import { Customer } from '../interfaces/customer';
import { User } from '../interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class DataService {

 
  

  constructor(private http: HttpClient, private auth: AuthService) { }

  // getHeader(){
  //   return  {
  //     headers: new HttpHeaders({
  //       "Authorization": "bearer " + JSON.parse(localStorage.getItem(TOKEN_NAME)).token,
  //       "Content-Type": "application/json"
  //     })
  //   };
  // }


 

  

  getUsers(route: string) {
    return this.http.get(this.createRoute(route, environment.urlAddress));
  }

 

  registerUser(user) {
    console.log(user,"registeruser")
    return this.http.post(this.createRoute("api/users/register", environment.urlAddress), user);
  }

  deleteUser(user: User) {
    return this.http.delete(this.createRouteDelete("api/users/delete", environment.urlAddress, user.userId));
  }

  updateUser(user: User) {
    console.log(user, "service update user")
    return this.http.put(this.createRoute("api/users/updateuser/" + user.userId, environment.urlAddress), user);

  }





  

  private createRoute(route: string, envAddress: string) {
    return `${envAddress}/${route}`;
  }

  private createRouteDelete(route: string, envAddress: string, userId: string) {
    return `${envAddress}/${route}?userid=${userId}`;

  }


}
