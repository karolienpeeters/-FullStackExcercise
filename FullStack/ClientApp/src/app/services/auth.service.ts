import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public authResponse:any;
  isLoggedIn = false;
  redirectUrl: string;

  constructor(private http: HttpClient) { }

  Login(user) {
       let result = this.http.post(this.createRoute("api/account/login",environment.urlAddress), user);
        console.log(result, "login")
       
       return result;
  }

  

  private createRoute(route:string, envAddress:string){
    return `${envAddress}/${route}`;
  }

}
