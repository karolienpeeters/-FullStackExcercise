import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';
import { Pagination } from '../interfaces/pagination';
import { CustomerFilterPagination } from '../interfaces/customerFilterPagination';
import { Customer } from '../interfaces/customer';
import { AuthService } from '../services/auth.service';
import { User } from '../interfaces/user';
import { CustomerDataService } from '../services/customer-data.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  
  public customerFilterPagination: CustomerFilterPagination;
  currentUser: User;

  pager: any = {};
 


  constructor(private customerService: CustomerDataService, authService: AuthService) {
    this.currentUser = authService.currentUserValue;
    console.log(this.currentUser)

  }

  ngOnInit() {
    this.customerFilterPagination =
      {
        filterFirstName:"",
        filterLastName:"",
        filterAccountNumber: "",
        filterSumTotalDueHigher: 0,
        filterSumTotalDueLower: 0,
        pageSize: 15,
        currentPage: 0,
        totalItems: 0,
        customerItemList: [],
        maxPages:10
      };
    this.getListCustomer();

  }

  setPage(page: number) {
    console.log(page, "page of method set page")
    this.pager = this.paginate(this.customerFilterPagination.totalItems, page, this.customerFilterPagination.pageSize, this.customerFilterPagination.maxPages);
    console.log(this.pager, "pager of method set page")
    
    if (page !== 0) {
      this.getListCustomer();
    }
  }

  getInitialListCustomer() {

  }

  getListCustomer() {
    this.customerService.getCustomersPage("api/customers", this.customerFilterPagination)
      .subscribe((result => {
        console.log(result);
        this.customerFilterPagination = result as CustomerFilterPagination;
        
        console.log(this.customerFilterPagination, "result from GetListCustomer");

        if (this.customerFilterPagination.currentPage === 0) {
          this.setPage(this.customerFilterPagination.currentPage);
        }

      }));

  }

  filter() {
    console.log("in filter method");
    this.customerFilterPagination.currentPage = 0;
    this.getListCustomer();
  }

  clearForm() {
    this.customerFilterPagination.filterFirstName = "";
    this.customerFilterPagination.filterLastName = "";
    this.customerFilterPagination.filterAccountNumber = "";
    this.customerFilterPagination.filterSumTotalDueHigher = 0;
    this.customerFilterPagination.filterSumTotalDueLower = 0;
    this.getListCustomer();
  }

  editCustomer(customer: Customer) {
    console.log(customer, "editcustomer");
    customer.showForm = true;
  }

  saveCustomer(customer: Customer) {
    console.log(customer, "savecustomer");
    this.customerService.updateCustomer(customer).subscribe((result => {
      console.log(result);
      customer.showForm = false;
    }));
  }

  exitLine(customer: Customer) {
    customer.showForm = false;
  }


  paginate(totalItems, currentPage, pageSize, maxPages) {
    if (currentPage === void 0) { currentPage = 1; }
    if (pageSize === void 0) { pageSize = 15; }
    if (maxPages === void 0) { maxPages = 10; }
    // calculate total pages
    var totalPages = Math.ceil(totalItems / pageSize);
    // ensure current page isn't out of range
    if (currentPage < 1) {
      currentPage = 1;
    }
    else if (currentPage > totalPages) {
      currentPage = totalPages;
    }
    var startPage, endPage;
    if (totalPages <= maxPages) {
      // total pages less than max so show all pages
      startPage = 1;
      endPage = totalPages;
    }
    else {
      // total pages more than max so calculate start and end pages
      var maxPagesBeforeCurrentPage = Math.floor(maxPages / 2);
      var maxPagesAfterCurrentPage = Math.ceil(maxPages / 2) - 1;
      if (currentPage <= maxPagesBeforeCurrentPage) {
        // current page near the start
        startPage = 1;
        endPage = maxPages;
      }
      else if (currentPage + maxPagesAfterCurrentPage >= totalPages) {
        // current page near the end
        startPage = totalPages - maxPages + 1;
        endPage = totalPages;
      }
      else {
        // current page somewhere in the middle
        startPage = currentPage - maxPagesBeforeCurrentPage;
        endPage = currentPage + maxPagesAfterCurrentPage;
      }
    }

    // create an array of pages to ng-repeat in the pager control
    var pages = Array.from(Array((endPage + 1) - startPage).keys()).map(function (i) { return startPage + i; });
    // return object with all pager properties required by the view
    this.customerFilterPagination.currentPage = currentPage;
    this.customerFilterPagination.totalItems = totalItems;
    this.customerFilterPagination.pageSize = pageSize;
    
    
    return {
      totalItems: totalItems,
      currentPage: currentPage,
      pageSize: pageSize,
      totalPages: totalPages,
      pages: pages
    };
  }






}