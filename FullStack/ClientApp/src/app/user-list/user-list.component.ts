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
  public userName:string;
  public passWord:string;
  public user:User;

  constructor(private userService: UserDataService) { }

  ngOnInit() {
    this.getListUser();
  }

  getListUser() {
    this.userService.getUsers("api/users", ).subscribe((result => {
      console.log(result);
      this.userList =result;
      console.log(this.userList, "result from Getusers");
    }));
  }

  registerUser(form){
    this.userService.registerUser(form.value).subscribe((result => {
      form.reset();
      this.getListUser();   
    }));
  }

  exitLine(user:User)
  {
    user.showForm = false;
  }

  editUser(user:User)
  {
    user.showForm=true;
  }

  deleteUser(user:User)
  {
     this.userService.deleteUser(user).subscribe((
      result=>{this.getListUser();}))
  }

  saveUser(user){
    var string = user.rolesList.toString();
    user.rolesList = string.split(",");
    this.userService.updateUser(user).subscribe((result => {
       user.showForm = false;
    }));
  }



}
