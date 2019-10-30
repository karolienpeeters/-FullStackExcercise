import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public authResponse:any;
  constructor(private http: HttpClient) { }

  Login(user) {
    console.log(user);
    return this.http.post(this.createRoute("api/account/login",environment.urlAddress), user);
  }

  private createRoute(route:string, envAddress:string){
    return `${envAddress}/${route}`;
  }

}
