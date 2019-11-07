import { Component, OnInit, Input, Output,EventEmitter } from '@angular/core';
import { CustomerFilterPagination } from '../interfaces/customerFilterPagination';
//import { EventEmitter } from 'events';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {
  @Input() customerFilterPagination: CustomerFilterPagination;
  @Output() onClicked = new EventEmitter();
  pager: any = {};
 
  maxPages = 10;
 
  constructor() { }

  ngOnInit() {
    this.setPage(this.customerFilterPagination.currentPage);

  }

   setPage(page: number,) {
     this.pager = this.paginate(this.customerFilterPagination.totalItems, page, this.customerFilterPagination.pageSize, this.maxPages);
     this.onClicked.emit();
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
