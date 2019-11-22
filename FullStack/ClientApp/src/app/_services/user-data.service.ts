import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Pagination } from '../_interfaces/pagination';


@Injectable({
  providedIn: 'root'
})
export class UserDataService {

  constructor(private http: HttpClient) { }

  getUsers(skip: number, take: number) {
    return this.http.get<Pagination>(this.createRoute("api/users" + `?skip=${skip}&take=${take}`, environment.urlAddress));
  }

  registerUser(user) {
    return this.http.post(this.createRoute("api/users/register", environment.urlAddress), user);
  }

  deleteUser(user) {
    return this.http.delete(this.createRoute("api/users/delete?id=" + user.id, environment.urlAddress));
  }

  updateUser(user) {
    
    return this.http.put(this.createRoute("api/users/updateuser", environment.urlAddress), user);
  }

  private createRoute(route: string, envAddress: string) {
    return `${envAddress}/${route}`;
  }


}
