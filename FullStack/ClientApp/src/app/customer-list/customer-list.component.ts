import { Component, OnInit, ViewChild } from '@angular/core';
import { CustomerFilterPagination } from '../interfaces/customerFilterPagination';
import { Customer } from '../interfaces/customer';
import { AuthService } from '../services/auth.service';
import { User } from '../interfaces/user';
import { CustomerDataService } from '../services/customer-data.service';
import { PaginationComponent } from '../pagination/pagination.component';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  public customerFilterPagination: CustomerFilterPagination;
  currentUser: User;
  @ViewChild(PaginationComponent) pagination: PaginationComponent;

  constructor(private customerService: CustomerDataService, authService: AuthService) {
    this.currentUser = authService.currentUserValue;
  }

  ngOnInit() {
    this.customerFilterPagination =
      {
        filterFirstName: "",
        filterLastName: "",
        filterAccountNumber: "",
        filterSumTotalDueHigher: 0,
        filterSumTotalDueLower: 0,
        pagination:{
          pageSize:15,
          currentPage: 0,
          totalItems: 0,
          customerList:[] = [],
          userList:[]=[]

        },
      
      };
    this.getListCustomer();
  }

 async getListCustomer() {
  
    this.customerService.getCustomersPage("api/customers", this.customerFilterPagination)
      .subscribe((result => {
        console.log("get list customer result", result)
        this.customerFilterPagination.pagination.customerList = result.customerList;
        this.customerFilterPagination.pagination.totalItems = result.totalItems;
      
      }));
      
  }

  saveCustomer(customer: Customer) {
    this.customerService.updateCustomer(customer).subscribe((() => {
      customer.showForm = false;
    }));
  }

  async clickedSearch() {
    console.log("clicked search activated")
    await this.getListCustomer();
    console.log("clicked search ", this.customerFilterPagination)
    this.pagination.setPage(this.customerFilterPagination.pagination.currentPage,this.customerFilterPagination.pagination.totalItems);

  }

   clickedPage() {
    this.getListCustomer();
  }

}