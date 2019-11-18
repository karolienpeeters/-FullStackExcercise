import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Pagination } from '../_interfaces/pagination';


@Injectable({
  providedIn: 'root'
})
export class UserDataService {

  constructor(private http: HttpClient) { }

  getUsers(route: string, skip: number, take: number) {
    return this.http.get<Pagination>(this.createRoutePage(route, environment.urlAddress, skip, take));
  }

  registerUser(user) {
    return this.http.post(this.createRoute("api/users/register", environment.urlAddress), user);
  }

  deleteUser(user) {
    return this.http.delete(this.createRoute("api/users/delete?userid=" + user.userId, environment.urlAddress));
  }

  updateUser(user) {
    //console.log('userservice',user)
    return this.http.put(this.createRoute("api/users/updateuser", environment.urlAddress), user);
  }

  private createRoute(route: string, envAddress: string) {
    return `${envAddress}/${route}`;
  }

  private createRoutePage(route: string, envAddress: string, skip: number, take: number) {
    return `${envAddress}/${route}?skip=${skip}&take=${take}`;
  }
}
