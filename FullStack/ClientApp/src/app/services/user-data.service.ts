import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class UserDataService {

  constructor(private http: HttpClient) { }

  getUsers(route: string) {
    return this.http.get(this.createRoute(route, environment.urlAddress));
  }

  registerUser(user) {
    return this.http.post(this.createRoute("api/users/register", environment.urlAddress), user);
  }

  deleteUser(user) {
    return this.http.delete(this.createRouteDelete("api/users/delete", environment.urlAddress, user.userId));
  }

  updateUser(user) {
    return this.http.put(this.createRoute("api/users/updateuser/" + user.userId, environment.urlAddress), user);
  }

  private createRoute(route: string, envAddress: string) {
    return `${envAddress}/${route}`;
  }
  
  private createRouteDelete(route: string, envAddress: string, userId: string) {
    return `${envAddress}/${route}?userid=${userId}`;

  }
}
