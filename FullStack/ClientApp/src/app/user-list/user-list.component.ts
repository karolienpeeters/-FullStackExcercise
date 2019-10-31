import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';

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

  registerUser(form){
    this.service.registerUser(form.value).subscribe((result => {
      console.log(result,"register user");
      this.getListUser();
     
    }));
    

  }

}
