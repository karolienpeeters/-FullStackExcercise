import { Component, OnInit, ViewChild } from '@angular/core';
import { CustomerFilterPagination } from '../_interfaces/customerFilterPagination';
import { Customer } from '../_interfaces/customer';
import { AuthService } from '../_services/auth.service';
import { User } from '../_interfaces/user';
import { CustomerDataService } from '../_services/customer-data.service';
import { PaginationComponent } from '../pagination/pagination.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalEditCustomerComponent } from '../modal-edit-customer/modal-edit-customer.component';
import { NotificationService } from '../_services/notification.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  public customerFilterPagination: CustomerFilterPagination;
  currentUser: User;
  @ViewChild(PaginationComponent) pagination: PaginationComponent;

  constructor(private customerService: CustomerDataService, authService: AuthService,
    private notificationService: NotificationService, private modalService: NgbModal) {
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
        pagination: {
          pageSize: 15,
          currentPage: 1,
          totalItems: 0,
          customerList: [],
          userList: []
        },
      };

    this.getListCustomer();
  }

  getListCustomer() {
    this.customerService.getCustomersPage("api/customers", this.customerFilterPagination)
      .subscribe((result => {
        this.customerFilterPagination.pagination.customerList = result.customerList;
        this.customerFilterPagination.pagination.totalItems = result.totalItems;
        this.pagination.setPage(this.customerFilterPagination.pagination.currentPage);
      }));
  }

  openForm(customer: Customer) {
    const modalRef = this.modalService.open(ModalEditCustomerComponent, { backdrop: 'static', keyboard: false });
    modalRef.componentInstance.title = 'Edit customer';
    modalRef.componentInstance.customer = customer;
    modalRef.result.then((formvalues) => {
      if (formvalues) {
        this.customerService.updateCustomer(formvalues).subscribe((() => {
          this.notificationService.showSuccess(`update successful`)
          this.getListCustomer();
        }));
      }
    });
  }

}