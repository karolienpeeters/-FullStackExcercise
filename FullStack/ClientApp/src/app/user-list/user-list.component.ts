import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../interfaces/user';
import { UserDataService } from '../services/user-data.service';
import { Pagination } from '../interfaces/pagination';
import { PaginationComponent } from '../pagination/pagination.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  public userList;
  public user:User;
  public pagination:Pagination;
  @ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
    

  constructor(private userService: UserDataService) { }

  ngOnInit() {
    // console.log("user list on init")
    this.pagination={
      pageSize:10,
      currentPage: 1,
      totalItems: 0,
      customerList:[],
      userList:[]
    };
    this.getListUser();
  }

  getListUser() {
    // console.log("activated get list user")
    this.userService.getUsers("api/users", this.pagination.currentPage,this.pagination.pageSize).subscribe((result => {
     this.pagination.totalItems = result.totalItems;
     this.pagination.userList = result.userList;
      // console.log(this.pagination,"getListUser");
      this.paginationComponent.setPage(this.pagination.currentPage);
    }));
  }

  deleteUser(user:User)
  {
     this.userService.deleteUser(user).subscribe((()=>{this.getListUser();}))
  }

  saveUser(user){
    var string = user.rolesList.toString();
    user.rolesList = string.split(",");
    this.userService.updateUser(user).subscribe((result => {
      // console.log(result,"save user");
       user.showForm = false;
       this.getListUser();

    }));
  }


  

}
