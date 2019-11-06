import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

import * as jwt_decode from 'jwt-decode';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../interfaces/user';
import { map } from 'rxjs/operators';

export const TOKEN_NAME: string = 'jwt_token';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
      this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
      this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
      return this.currentUserSubject.value;
  }

  login(user) {
      return this.http.post<any>(`${environment.urlAddress}/api/account/login`, user)
          .pipe(map(user => {
              // login successful if there's a jwt token in the response
              if (user && user.token) {
                console.log(user,"test login");
                  // store user details and jwt token in local storage to keep user logged in between page refreshes
                 
                  
                  localStorage.setItem('currentUser', JSON.stringify(user));
                  this.currentUserSubject.next(user);
                  var decoded = jwt_decode(user.token);
                  this.currentUserSubject.value.rolesList = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
                  console.log(this.currentUserSubject.value.rolesList.length);
                  if(typeof this.currentUserSubject.value.rolesList === "string")
                  {
                    this.currentUserSubject.value.rolesList = [this.currentUserSubject.value.rolesList];
                  }
                 this.isAdmin();
                  //this.currentUserSubject.value.email = ;

              }

              return user;
          }));
  }

  logout() {
      // remove user from local storage to log user out
      localStorage.removeItem('currentUser');
      //this.currentUserSubject.unsubscribe();
      this.currentUserSubject.next(null);
      console.log(this.currentUser,'currentUser after log out');
      console.log(this.currentUserSubject.value, "currentusersubject after log out")
  }

  getRoles(){
 
    var decoded = jwt_decode(this.currentUserSubject.value.token);
    this.currentUserSubject.value.rolesList = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
console.log(this.currentUserSubject.value.rolesList ,"current user roles")
   
   
  }
  
  isAdmin()
  {
    console.log(this.currentUserValue)
    this.currentUserSubject.value.isAdmin = false;
    this.currentUserValue.rolesList.forEach(role => {
      if(role === "Admin")
      {
        this.currentUserValue.isAdmin = true;
      }
      
    });
    
  }


  
  getTokenExpirationDate(token: string): Date {
    const decoded = jwt_decode(token);

    if (decoded.exp === undefined) return null;

    const date = new Date(0); 
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  isTokenExpired(token?: string): boolean {
    if(!token) token = this.currentUserSubject.value.token;
    console.log(token,"isTokenExpired")
    if(!token) return true;

    const date = this.getTokenExpirationDate(token);
    if(date === undefined) return false;
    return !(date.valueOf() > new Date().valueOf());
  }

}
