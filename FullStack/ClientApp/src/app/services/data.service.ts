import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Pagination } from '../interfaces/pagination';
import { AuthService } from './auth.service';
import { CustomerFilterPagination } from '../interfaces/customerFilterPagination';
import { Customer } from '../interfaces/customer';
import { User } from '../interfaces/user';


@Injectable({
  providedIn: 'root'
})
export class DataService {

  httpOptions = {
    headers: new HttpHeaders({
      "Authorization": "bearer " + this.auth.authResponse.token,
      "Content-Type": "application/json"
    })
  };

  constructor(private http: HttpClient, private auth: AuthService) { }


  getCustomersPage(route: string, page: number, items: number, filterFirstName: string, filterAccountNumber: string, filterLastName: string,
    filterSumTotalDueHigher: number, filterSumTotalDueLower: number) {

    return this.http.get(this.createFilterPageRoute(route, environment.urlAddress, page, items, filterFirstName, filterAccountNumber,
      filterLastName, filterSumTotalDueHigher, filterSumTotalDueLower), this.httpOptions);
  }

  // FilterCustomers(route: string, page: number, items: number, filterFirstName: string, filterAccountNumber: string, filterLastName: string,
  //   filterSumTotalDueHigher: number, filterSumTotalDueLower: number) {

  //   return this.http.get(this.createFilterPageRoute(route, environment.urlAddress, page, items, filterFirstName, filterAccountNumber,
  //     filterLastName, filterSumTotalDueHigher, filterSumTotalDueLower), this.httpOptions);
  // }

  getUsers(route: string) {
    return this.http.get(this.createRoute(route, environment.urlAddress), this.httpOptions);
  }

 updateCustomer(customer: Customer) {
    console.log(customer, "service update customer")
    return this.http.put(this.createRoute("api/customers/updatecustomer", environment.urlAddress), customer, this.httpOptions);

  }

 createUser(user:User){
    return this.http.post(this.createRoute("api/users/create", environment.urlAddress), user, this.httpOptions);
  }

  deleteUser(user:User){
    return this.http.delete(this.createRouteDelete("api/users/delete", environment.urlAddress,user.userId), this.httpOptions);
  }

  updateUser(user: User) {
   

    console.log(user, "service update user")
    return this.http.put(this.createRoute("api/users/updateuser/" + user.userId, environment.urlAddress), user, this.httpOptions);

  }




  private createFilterPageRoute(route: string, envAddress: string, page: number, items: number, filterFirstName: string, filterAccountNumber: string,
    filterLastName: string, filterSumTotalDueHigher: number, filterSumTotalDueLower: number) {
    let filterroute = `${envAddress}/${route}?skip=${page}&take=${items}&filterfirstname=${filterFirstName}&filterlastname=${filterLastName}&filteraccountnumber=${filterAccountNumber}&filtersumtotalduehigher=${filterSumTotalDueHigher}&filtersumtotalduelower=${filterSumTotalDueLower}`;
    console.log(filterroute);
    return filterroute;
  }

  private createRoute(route: string, envAddress: string) {
    return `${envAddress}/${route}`;
  }

  private createRouteDelete(route: string, envAddress: string,userId:string){
    return `${envAddress}/${route}?userid=${userId}`;

  }

  private createRouteUpdate(route: string, envAddress: string,userId:string,roles:string[]){
    return `${envAddress}/${route}?userid=${userId}&roles=${roles}`;

  }


}
