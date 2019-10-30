import { Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import {  Pagination } from '../interfaces/pagination';
//import {JwPaginationComponent,paginate} from 'jw-angular-pagination';


//import paginate as _ from 'jw-paginate';

@Component({
  //moduleId: module.id,
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',

//   template: `<ul *ngIf="pager.pages && pager.pages.length" class="pagination">
//   <li [ngClass]="{disabled:pager.currentPage === 1}" class="page-item first-item">
//       <a (click)="setPage(1)" class="page-link">First</a>
//   </li>
//   <li [ngClass]="{disabled:pager.currentPage === 1}" class="page-item previous-item">
//       <a (click)="setPage(pager.currentPage - 1)" class="page-link">Previous</a>
//   </li>
//   <li *ngFor="let page of pager.pages" [ngClass]="{active:pager.currentPage === page}" class="page-item number-item">
//       <a (click)="setPage(page)" class="page-link">{{page}}</a>
//   </li>
//   <li [ngClass]="{disabled:pager.currentPage === pager.totalPages}" class="page-item next-item">
//       <a (click)="setPage(pager.currentPage + 1)" class="page-link">Next</a>
//   </li>
//   <li [ngClass]="{disabled:pager.currentPage === pager.totalPages}" class="page-item last-item">
//       <a (click)="setPage(pager.totalPages)" class="page-link">Last</a>
//   </li>
// </ul>`
})

export class PaginationComponent implements OnInit, OnChanges {
  @Input() items: Array<any>;
  @Output() changePage = new EventEmitter<any>(true);
  @Input() initialPage = 1;
  @Input() pageSize = 10;
  @Input() maxPages = 10;

  @Input() pagination:Pagination ;

  pager: any = {};

  /**
   *
   */
  constructor() {
    console.log("paginationcomponent" , this.pagination);
    console.log(this.pager);
    
  }

  ngOnInit() {
    // set page if items array isn't empty
    // if (this.items && this.items.length)  {
    //   this.setPage(this.initialPage);
    // }
    this.setPage(this.initialPage);
  }

  ngOnChanges(changes: SimpleChanges) {
    // reset page if items array has changed
    if (changes.items.currentValue !== changes.items.previousValue) {
       this.setPage(this.initialPage);
     }

    
  }
  setPage(page: number) {
    this.pager = this.paginate(this.pagination.totalItems, page, this.pageSize, this.maxPages);

    // get new page of items from items array
    
    // call change page function in parent component
    this.changePage.emit(this.pagination);
  }


  paginate(totalItems, currentPage, pageSize, maxPages) {
    if (currentPage === void 0) { currentPage = 1; }
    if (pageSize === void 0) { pageSize = 10; }
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
    // calculate start and end item indexes
    var startIndex = (currentPage - 1) * pageSize;
    var endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);
    // create an array of pages to ng-repeat in the pager control
    var pages = Array.from(Array((endPage + 1) - startPage).keys()).map(function (i) { return startPage + i; });
    // return object with all pager properties required by the view
    return {
        totalItems: totalItems,
        currentPage: currentPage,
        pageSize: pageSize,
        totalPages: totalPages,
        startPage: startPage,
        endPage: endPage,
        startIndex: startIndex,
        endIndex: endIndex,
        pages: pages
    };
}

  // paginate(pagination:Pagination) {
  //   if (pagination.currentPage === void 0) { pagination.currentPage = 1; }
  //   if (pagination.pageSize === void 0) {pagination. pageSize = 10; }
  //   if (pagination.maxPages === void 0) { pagination.maxPages = 10; }
  //   // calculate total pages
  //   pagination.totalPages = Math.ceil(pagination.totalItems / pagination.pageSize);
  //   // ensure current page isn't out of range
  //   if (pagination.currentPage < 1) {
  //     pagination.currentPage = 1;
  //   }
  //   else if (pagination.currentPage > pagination.totalPages) {
  //     pagination.currentPage = pagination.totalPages;
  //   }
  //   //var startPage, endPage;
  //   if (pagination.totalPages <= pagination.maxPages) {
  //       // total pages less than max so show all pages
  //       pagination.startPage = 1;
  //       pagination.endPage = pagination.totalPages;
  //   }
  //   else {
  //       // total pages more than max so calculate start and end pages
  //       var maxPagesBeforeCurrentPage = Math.floor(pagination.maxPages / 2);
  //       var maxPagesAfterCurrentPage = Math.ceil(pagination.maxPages / 2) - 1;
  //       if (pagination.currentPage <= maxPagesBeforeCurrentPage) {
  //           // current page near the start
  //           pagination.startPage = 1;
  //           pagination.endPage = pagination.maxPages;
  //       }
  //       else if (pagination.currentPage + maxPagesAfterCurrentPage >= pagination.totalPages) {
  //           // current page near the end
  //           pagination.startPage = pagination.totalPages - pagination.maxPages + 1;
  //           pagination.endPage = pagination.totalPages;
  //       }
  //       else {
  //           // current page somewhere in the middle
  //           pagination.startPage = pagination.currentPage - maxPagesBeforeCurrentPage;
  //           pagination.endPage = pagination.currentPage + maxPagesAfterCurrentPage;
  //       }
  //   }
  //   // calculate start and end item indexes
  //   pagination.startIndex = (pagination.currentPage - 1) * pagination.pageSize;
  //   pagination.endIndex = Math.min(pagination.startIndex + pagination.pageSize - 1, pagination.totalItems - 1);
  //   // create an array of pages to ng-repeat in the pager control
  //   pagination.pages = Array.from(Array((pagination.endPage + 1) - pagination.startPage).keys()).map(function (i) { return pagination.startPage + i; });
  //   // return object with all pager properties required by the view
  //   // return {
  //   //     totalItems: totalItems,
  //   //     currentPage: currentPage,
  //   //     pageSize: pageSize,
  //   //     totalPages: totalPages,
  //   //     startPage: startPage,
  //   //     endPage: endPage,
  //   //     startIndex: startIndex,
  //   //     endIndex: endIndex,
  //   //     pages: pages
  //   //};
  // }
}