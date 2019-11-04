import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';
import { User } from '../interfaces/user';
import { ThrowStmt } from '@angular/compiler';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  public userList;
  

  constructor(private service: DataService) { }

  ngOnInit() {
    this.getListUser();
  }

  getListUser() {
    this.service.getUsers("api/users", ).subscribe((result => {
      console.log(result);
      this.userList =result;
      console.log(this.userList, "result from Getusers");
    }));
  }

  createUser(form){
    this.service.createUser(form.value).subscribe((result => {
      console.log(result,"register user");
      this.getListUser();
     
    }));
  }

  exitLine(user:User)
  {
    user.showForm = false;
  }

  editUser(user:User)
  {
    console.log(user, "editUser");
    user.showForm=true;
  }

  deleteUser(user:User)
  {
    console.log(user, "deleteUser");
    this.service.deleteUser(user).subscribe((
      result=>{
        console.log(result,"result deletuser");
        this.getListUser();
            }
    ))
  }

  saveUser(user){
    console.log(user, "saveUser");
    var string = user.rolesList.toString();
    user.rolesList = string.split(",");
    this.service.updateUser(user).subscribe((result => {
      console.log(result);
      user.showForm = false;
    }));
  }

}
