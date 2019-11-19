import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../_interfaces/user';
import { UserDataService } from '../_services/user-data.service';
import { Pagination } from '../_interfaces/pagination';
import { PaginationComponent } from '../pagination/pagination.component';
import { NotificationService } from '../_services/notification.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalEditUserComponent } from '../modal-edit-user/modal-edit-user.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  public pagination: Pagination;
  @ViewChild(PaginationComponent) paginationComponent: PaginationComponent;

  constructor(private userService: UserDataService,
    private notificationService: NotificationService, private modalService: NgbModal) { }

  ngOnInit() {
    this.pagination = {
      pageSize: 10,
      currentPage: 1,
      totalItems: 0,
      customerList: [],
      userList: []
    };
    this.getListUser();
  }

  getListUser() {
    this.userService.getUsers(this.pagination.currentPage, this.pagination.pageSize).subscribe((result => {
      this.pagination.totalItems = result.totalItems;
      this.pagination.userList = result.userList;
      this.paginationComponent.setPage(this.pagination.currentPage);
    }));
  }

  deleteUser(user: User) {
    this.userService.deleteUser(user).subscribe((() => {
      this.notificationService.showSuccess(user.email + " deleted successful")
      this.getListUser();
    }))
  }

  openUpdateForm(user: User) {
    const modalRef = this.modalService.open(ModalEditUserComponent, { backdrop: 'static', keyboard: false });
    modalRef.componentInstance.title = 'Edit user';
    modalRef.componentInstance.user = user;
    modalRef.result.then((result) => {
      if (result) {
        var string = user.rolesList.toString();
        user.rolesList = string.split(",");
        this.userService.updateUser(user).subscribe((result => {
          this.notificationService.showSuccess(`update successful`)
          this.getListUser();
        }));
      }
    });
  }
}
