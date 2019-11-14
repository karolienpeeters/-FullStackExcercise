import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { CustomerFilterPagination } from '../interfaces/customerFilterPagination';
import { FormGroup, FormBuilder } from '@angular/forms';
import { PatternValidator } from '../_validators/pattern-validator';

@Component({
  selector: 'app-customer-search',
  templateUrl: './customer-search.component.html',
  styleUrls: ['./customer-search.component.css']
})
export class CustomerSearchComponent implements OnInit {
  @Input() customerFilterPagination: CustomerFilterPagination;
  @Output() onClicked = new EventEmitter();
  searchForm: FormGroup;
  submitted = false;
    
  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      filterFirstName:[''],
      filterLastName:[''],
      filterAccountNumber:[''],
      filterSumTotalDueHigher:[0,[PatternValidator(/\d+/,{hasNumber:true})]],
      filterSumTotalDueLower:[0,[PatternValidator(/\d+/,{hasNumber:true})]],
    })
  }
  get f() { return this.searchForm.controls; }

  filter(form) {
    console.log(form);
    this.submitted = true;
    if(this.searchForm.invalid)
    {
      return;
    }
    this.customerFilterPagination.filterFirstName = form.value.filterFirstName;
    this.customerFilterPagination.filterLastName = form.value.filterLastName;
    this.customerFilterPagination.filterAccountNumber = form.value.filterAccountNumber;
    this.customerFilterPagination.filterSumTotalDueHigher = form.value.filterSumTotalDueHigher;
    this.customerFilterPagination.filterSumTotalDueLower = form.value.filterSumTotalDueLower;
    this.customerFilterPagination.pagination.currentPage = 1; 

     // console.log(this.customerFilterPagination);
    this.onClicked.emit();
  }

 clearForm() {
   this.submitted = false;
   this.searchForm.reset({filterSumTotalDueHigher:0,filterSumTotalDueLower:0});
    this.customerFilterPagination.filterFirstName = "";
    this.customerFilterPagination.filterLastName = "";
    this.customerFilterPagination.filterAccountNumber = "";
    this.customerFilterPagination.filterSumTotalDueHigher = 0;
    this.customerFilterPagination.filterSumTotalDueLower = 0;
    this.customerFilterPagination.pagination.currentPage = 1; 
    this.onClicked.emit();
  }

}
