import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

//import * as jwt_decode from 'jwt-decode';

export const TOKEN_NAME: string = 'jwt_token';


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

  
  private url: string = 'api/auth';
  private headers = new Headers({ 'Content-Type': 'application/json' });

 

  getToken(): string {
    return localStorage.getItem(TOKEN_NAME);
  }

  setToken(token: string): void {
    localStorage.setItem(TOKEN_NAME, token);
  }

  getTokenExpirationDate(token: string): Date {
    const decoded = jwt_decode(token);

    if (decoded.exp === undefined) return null;

    const date = new Date(0); 
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  isTokenExpired(token?: string): boolean {
    if(!token) token = this.getToken();
    if(!token) return true;

    const date = this.getTokenExpirationDate(token);
    if(date === undefined) return false;
    return !(date.valueOf() > new Date().valueOf());
  }

}
