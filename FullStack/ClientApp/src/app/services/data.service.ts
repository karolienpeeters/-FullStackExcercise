import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Pagination } from '../interfaces/pagination';
import { AuthService } from './auth.service';
import { CustomerFilterPagination } from '../interfaces/customerFilterPagination';


@Injectable({
  providedIn: 'root'
})
export class DataService {

 

  constructor(private http:HttpClient,private auth: AuthService) { }

  // GetAllCustomers(route:string){
  //   return this.http.get<Pagination>(this.createCompleteRoute(route,environment.urlAddress));
  // }

  GetCustomersPage(route:string,page:number, items:number,filterFirstName:string,filterAccountNumber:string,filterLastName:string,filterSumTotalDueHigher:number,filterSumTotalDueLower:number){
    
    return this.http.get(this.createFilterPageRoute(route,environment.urlAddress,page,items,filterFirstName,filterAccountNumber,filterLastName,filterSumTotalDueHigher,filterSumTotalDueLower),{
      headers: new HttpHeaders({
        "Authorization": "bearer " + this.auth.authResponse.token,
        "Content-Type": "application/json"
      })
    })
  }

 FilterCustomers(route:string,page:number, items:number,filterFirstName:string,filterAccountNumber:string,filterLastName:string,filterSumTotalDueHigher:number,filterSumTotalDueLower:number){
    
    return this.http.get(this.createFilterPageRoute(route,environment.urlAddress,page,items,filterFirstName,filterAccountNumber,filterLastName,filterSumTotalDueHigher,filterSumTotalDueLower),{
      headers: new HttpHeaders({
        "Authorization": "bearer " + this.auth.authResponse.token,
        "Content-Type": "application/json"
      })
    })
  }

  GetUsers(route:string){
 
    return this.http.get(this.createRoute(route,environment.urlAddress),{
      headers: new HttpHeaders({
        "Authorization": "bearer " + this.auth.authResponse.token,
        "Content-Type": "application/json"
      })
    })
  
 
}

  
  private createPageRoute(route:string, envAddress:string,page:number, items:number){
    return `${envAddress}/${route}?skip=${page}&take=${items}`;
  }

  private createFilterPageRoute(route:string, envAddress:string,page:number, items:number,filterFirstName:string,filterAccountNumber:string,filterLastName:string,filterSumTotalDueHigher:number,filterSumTotalDueLower:number){
    let filterroute= `${envAddress}/${route}?skip=${page}&take=${items}&filterfirstname=${filterFirstName}&filterlastname=${filterLastName}&filteraccountnumber=${filterAccountNumber}&filtersumtotalduehigher=${filterSumTotalDueHigher}&filtersumtotalduelower=${filterSumTotalDueLower}`;
    console.log(filterroute);
    return filterroute;
  }

  private createRoute(route:string, envAddress:string){
    return `${envAddress}/${route}`;
  }


}
