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
    this.GetListUser();
  }

  GetListUser() {
    this.service.GetUsers("api/users", ).subscribe((result => {
      console.log(result);
      this.userList =result;
      console.log(this.userList, "result from Getusers");
    }));
  }

}
