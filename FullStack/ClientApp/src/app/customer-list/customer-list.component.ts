import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';
import { Pagination } from '../interfaces/pagination';
import { CustomerFilterPagination } from '../interfaces/customerFilterPagination';
import { Customer } from '../interfaces/customer';
import { AuthService } from '../services/auth.service';
import { User } from '../interfaces/user';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  public customerlist;
  public customerPagination: Pagination;
  public filterFirstName: string = "";
  public filterLastName: string = "";
  public filterAccountNumber: string = "";
  public filterSumTotalDueHigher: number = 0;
  public filterSumTotalDueLower: number = 0;
 currentUser :User;

  pager: any = {};
  pageSize = 15;
  maxPages = 10;
  initialPage = 0;


  constructor(private service: DataService, authService:AuthService) { 
    this.currentUser = authService.currentUserValue;
  }

  ngOnInit() {
    this.getListCustomer(this.initialPage, this.pageSize, this.filterFirstName, this.filterAccountNumber, this.filterLastName, this.filterSumTotalDueHigher, this.filterSumTotalDueLower);
  }

  setPage(page: number) {
    this.pager = this.paginate(this.customerPagination.totalItems, page, this.pageSize, this.maxPages);
    if (page !== 0) {
      this.getListCustomer(page, this.pageSize, this.filterFirstName, this.filterAccountNumber, this.filterLastName, this.filterSumTotalDueHigher, this.filterSumTotalDueLower);
    }
  }

  getListCustomer(pageNumber: number, pageItems: number, filterFirstName: string, filterAccountNumber: string, filterLastName: string, filterSumTotalDueHigher: number, filterSumTotalDueLower) {
    this.service.getCustomersPage("api/customers", pageNumber, pageItems, filterFirstName, filterAccountNumber, filterLastName, filterSumTotalDueHigher, filterSumTotalDueLower)
      .subscribe((result => {
        console.log(result);
        this.customerPagination = result as Pagination;
        this.customerlist = this.customerPagination.customerItemList;
        console.log(this.customerPagination, "result from GetListCustomer");

        if (pageNumber === 0) {
          this.setPage(pageNumber);
        }

      }));

  }

  filter(pageNumber: number) {
    console.log("in filter method");
    this.getListCustomer(pageNumber, this.pageSize, this.filterFirstName, this.filterAccountNumber, this.filterLastName, this.filterSumTotalDueHigher, this.filterSumTotalDueLower);
  }

  clearForm() {
    this.filterAccountNumber = "";
    this.filterFirstName = "";
    this.filterLastName = "";
    this.filterSumTotalDueHigher = 0;
    this.filterSumTotalDueLower = 0;
    this.getListCustomer(this.initialPage, this.pageSize, this.filterFirstName, this.filterAccountNumber, this.filterLastName, this.filterSumTotalDueHigher, this.filterSumTotalDueLower);
  }

  editCustomer(customer: Customer) {
    console.log(customer, "editcustomer");
    customer.showForm = true;
  }

  saveCustomer(customer: Customer) {
    console.log(customer, "savecustomer");
    this.service.updateCustomer(customer).subscribe((result => {
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
    return {
      totalItems: totalItems,
      currentPage: currentPage,
      pageSize: pageSize,
      totalPages: totalPages,
      pages: pages
    };
  }






}