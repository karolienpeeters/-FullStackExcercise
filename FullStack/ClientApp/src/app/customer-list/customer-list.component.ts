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
        pageSize: 15,
        currentPage: 0,
        totalItems: 0,
        customerItemList: [],
      };
    this.getListCustomer();
  }

  async getListCustomer() {
    this.customerService.getCustomersPage("api/customers", this.customerFilterPagination)
      .subscribe((result => {
        this.customerFilterPagination.customerItemList = result.customerItemList;
        this.customerFilterPagination.totalItems = result.totalItems;
      }));
  }

  saveCustomer(customer: Customer) {
    this.customerService.updateCustomer(customer).subscribe((() => {
      customer.showForm = false;
    }));
  }

  async clickedSearch() {
    await this.getListCustomer();
    this.pagination.setPage(this.customerFilterPagination.currentPage);

  }

  clickedPage() {
    this.getListCustomer();
  }

}