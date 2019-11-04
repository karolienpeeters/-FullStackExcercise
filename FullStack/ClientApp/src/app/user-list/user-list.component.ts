import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';
import { User } from '../interfaces/user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  public userList;
  public  userName:string;
  public passWord:string;
  public user:User;

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

  registerUser(form){
   
    this.service.registerUser(form.value).subscribe((result => {
      console.log(result,"register user");
      form.reset();

      console.log(form.value)
      this.getListUser();
     
    }));
    

  }


  createUser(form){
    this.service.registerUser(form.value).subscribe((result => {
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

  clearForm(myform){
    console.log(myform, "myform create user");
  }

}
