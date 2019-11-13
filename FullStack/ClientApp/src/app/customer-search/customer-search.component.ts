import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { CustomerFilterPagination } from '../interfaces/customerFilterPagination';

@Component({
  selector: 'app-customer-search',
  templateUrl: './customer-search.component.html',
  styleUrls: ['./customer-search.component.css']
})
export class CustomerSearchComponent implements OnInit {
  @Input() customerFilterPagination: CustomerFilterPagination;
  @Output() onClicked = new EventEmitter();
  activatedNr: number = 0;
    
  constructor() { }

  ngOnInit() {
  }

  async filter() {
    this.activatedNr +=1;
    // console.log("filter activated",this.activatedNr);
    this.customerFilterPagination.pagination.currentPage = 1; 
    // console.log(this.customerFilterPagination);
    this.onClicked.emit();
  }

 async clearForm() {
    this.customerFilterPagination.filterFirstName = "";
    this.customerFilterPagination.filterLastName = "";
    this.customerFilterPagination.filterAccountNumber = "";
    this.customerFilterPagination.filterSumTotalDueHigher = 0;
    this.customerFilterPagination.filterSumTotalDueLower = 0;
    this.customerFilterPagination.pagination.currentPage = 1; 
    this.onClicked.emit();
  }

}
