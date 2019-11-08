import { Component, OnInit } from '@angular/core';
import { User } from '../interfaces/user';
import { UserDataService } from '../services/user-data.service';
import { Pagination } from '../interfaces/pagination';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  public userList;
  public user:User;
  public pagination:Pagination;

  constructor(private userService: UserDataService) { }

  ngOnInit() {
    console.log("user list on init")
    this.pagination={
      pageSize:5,
      currentPage: 1,
      totalItems: 0,
      customerList:[],
      userList:[]
    };
    this.getListUser();
  }

  getListUser() {
    console.log("activated get list user")
    this.userService.getUsers("api/users", this.pagination.currentPage,this.pagination.pageSize).subscribe((result => {
     this.pagination.totalItems = result.totalItems;
     this.pagination.userList = result.userList;
      console.log(this.pagination,"getListUser")
     
    }));
  }

  deleteUser(user:User)
  {
     this.userService.deleteUser(user).subscribe((()=>{this.getListUser();}))
  }

  saveUser(user){
    user.rolesList = [user.rolesList];
    this.userService.updateUser(user).subscribe((() => {
       user.showForm = false;
    }));
  }

  clicked(){
    console.log("user list clicked ")

    this.getListUser();
  }
  
  clickedPage(){
    console.log("user list clicked page")
    this.getListUser();
  }
}
