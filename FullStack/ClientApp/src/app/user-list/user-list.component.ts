import { Component, OnInit } from '@angular/core';
import { User } from '../interfaces/user';
import { UserDataService } from '../services/user-data.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  public userList;
  public user:User;

  constructor(private userService: UserDataService) { }

  ngOnInit() {
    this.getListUser();
  }

  getListUser() {
    this.userService.getUsers("api/users", ).subscribe((result => {
      this.userList =result;
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
    this.getListUser();
  }

}
