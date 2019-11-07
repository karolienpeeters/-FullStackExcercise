import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { CustomerFilterPagination } from '../interfaces/customerFilterPagination';
import { PaginationComponent } from '../pagination/pagination.component';

@Component({
  selector: 'app-customer-search',
  templateUrl: './customer-search.component.html',
  styleUrls: ['./customer-search.component.css']
})
export class CustomerSearchComponent implements OnInit {
  @Input() customerFilterPagination: CustomerFilterPagination;
  @Output() onClicked = new EventEmitter();
  @ViewChild(PaginationComponent) pagination:PaginationComponent;
  
  constructor() { }

  ngOnInit() {
  }

  async filter() {
    console.log("Filtermethod called")
    this.customerFilterPagination.currentPage = 0; 
    this.onClicked.emit();
    console.log(this.customerFilterPagination.totalItems,"filter method");
    this.pagination.setPage(this.customerFilterPagination.currentPage);
  }

 async clearForm() {
    this.customerFilterPagination.filterFirstName = "";
    this.customerFilterPagination.filterLastName = "";
    this.customerFilterPagination.filterAccountNumber = "";
    this.customerFilterPagination.filterSumTotalDueHigher = 0;
    this.customerFilterPagination.filterSumTotalDueLower = 0;
    this.customerFilterPagination.currentPage = 0; 
    this.onClicked.emit();
  }

}
