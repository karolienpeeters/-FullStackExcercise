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
      filterFirstName: [''],
      filterLastName: [''],
      filterAccountNumber: [''],
      filterSumTotalDueHigher: [0, [PatternValidator(/\d+/, { hasNumber: true })]],
      filterSumTotalDueLower: [0, [PatternValidator(/\d+/, { hasNumber: true })]],
    })
  }
  get f() { return this.searchForm.controls; }

  filter(form) {
    this.submitted = true;
    if (this.searchForm.invalid) {
      return;
    }
    this.setCustomerFilterPagination(form.value.filterFirstName, form.value.filterLastName, form.value.filterAccountNumber, form.value.filterSumTotalDueHigher, form.value.filterSumTotalDueLower, 1);
    this.onClicked.emit();
  }

  clearForm() {
    this.submitted = false;
    this.searchForm.reset({ filterSumTotalDueHigher: 0, filterSumTotalDueLower: 0 });
    this.setCustomerFilterPagination("", "", "", 0, 0, 1);
    this.onClicked.emit();
  }

  setCustomerFilterPagination(filterFirstName: string, filterLastName: string, filterAccountNumber: string, filterSumTotalDueHigher: number, filterSumTotalDueLower: number, currentPage: number) {
    this.customerFilterPagination.filterFirstName = filterFirstName;
    this.customerFilterPagination.filterLastName = filterLastName;
    this.customerFilterPagination.filterAccountNumber = filterAccountNumber;
    this.customerFilterPagination.filterSumTotalDueHigher = filterSumTotalDueHigher;
    this.customerFilterPagination.filterSumTotalDueLower = filterSumTotalDueLower;
    this.customerFilterPagination.pagination.currentPage = currentPage;
  }

}
