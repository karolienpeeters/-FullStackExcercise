import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Customer } from '../interfaces/customer';
import { CustomerFilterPagination } from '../interfaces/customerFilterPagination';

@Injectable({
  providedIn: 'root'
})
export class CustomerDataService {

  constructor(private http: HttpClient) { }

  getCustomersPage(route: string, customerFP:CustomerFilterPagination) {
    console.log(customerFP,"customerFP from getcustomerpage")
    return this.http.get(this.createFilterPageRoute(route, environment.urlAddress, customerFP));
  }

  updateCustomer(customer: Customer) {
    console.log(customer, "service update customer")
    return this.http.put(this.createRoute("api/customers/updatecustomer", environment.urlAddress), customer);

  }

  private createFilterPageRoute(route: string, envAddress: string, customerFP:CustomerFilterPagination) {
    let filterroute = `${envAddress}/${route}?skip=${customerFP.currentPage}&take=${customerFP.pageSize}&filterfirstname=${customerFP.filterFirstName}&filterlastname=${customerFP.filterLastName}&filteraccountnumber=${customerFP.filterAccountNumber}&filtersumtotalduehigher=${customerFP.filterSumTotalDueHigher}&filtersumtotalduelower=${customerFP.filterSumTotalDueLower}`;
    console.log(filterroute);
    return filterroute;
  }

  private createRoute(route: string, envAddress: string) {
    return `${envAddress}/${route}`;
  }

}
