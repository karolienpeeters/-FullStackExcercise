import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { User } from '../interfaces/user';
import { UserDataService } from '../services/user-data.service';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.css']
})
export class UserCreateComponent implements OnInit {
  @Output() onClicked = new EventEmitter();
  public user: User;
  
  constructor(private userService: UserDataService) { }

  ngOnInit() {
  }

  registerUser(form) {
    this.userService.registerUser(form.value).subscribe((() => {
      form.reset();
      this.onClicked.emit();
    }));
  }


}
