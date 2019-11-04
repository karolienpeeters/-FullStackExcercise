import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public authResponse:any;
  isLoggedIn = false;
  redirectUrl: string;

  httpOptions = {
    headers: new HttpHeaders({
      
      "Content-Type": "application/json"
    })
  };


  constructor(private http: HttpClient) { }

  Login(user) {
       let result = this.http.post(this.createRoute("api/account/login",environment.urlAddress), user,this.httpOptions);
        console.log(result, "auth service")
       
       return result;
  }

  

  private createRoute(route:string, envAddress:string){
    return `${envAddress}/${route}`;
  }

}
