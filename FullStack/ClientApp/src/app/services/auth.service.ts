import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

import * as jwt_decode from 'jwt-decode';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../interfaces/user';
import { map, distinctUntilChanged } from 'rxjs/operators';

export const TOKEN_NAME: string = 'jwt_token';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    const token = JSON.parse(localStorage.getItem('currentUser'));
    this.currentUserSubject = new BehaviorSubject<User>(token ? this.getCurrentUserFromToken(token.token) : null);
    this.currentUser = this.currentUserSubject.asObservable().pipe(distinctUntilChanged());
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(user) {
    return this.http.post<any>(`${environment.urlAddress}/api/account/login`, user)
      .pipe(map(userToken => {
        // login successful if there's a jwt token in the response
        if (userToken && userToken.token) {
          console.log(userToken, "test login");
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(userToken));
          this.currentUserSubject.next(this.getCurrentUserFromToken(userToken.token));
        }
        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    console.log("logout", this.currentUserValue);
  }


  private getCurrentUserFromToken(token: string): User {
    var decoded = jwt_decode(token);

    let user = {
      rolesList: decoded ['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
      userId: "",
      email: decoded ['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
      showForm: false,
      token: token,
      isAdmin: false
    };
    user.isAdmin = user.rolesList.includes("Admin");
    return user;
  }

  getTokenExpirationDate(token: string): Date {
    const decoded = jwt_decode(token);
    if (decoded.exp === undefined) return null;

    const date = new Date(0);
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  isTokenExpired(token?: string): boolean {
    if (!token) token = this.currentUserSubject.value.token;
    console.log(token, "isTokenExpired")
    if (!token) return true;

    const date = this.getTokenExpirationDate(token);
    if (date === undefined) return false;
    return !(date.valueOf() > new Date().valueOf());
  }

}
