import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { User } from '../_interfaces/user';
import { UserDataService } from '../_services/user-data.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PatternValidator } from '../_validators/pattern-validator'

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.css']
})
export class UserCreateComponent implements OnInit {
  @Output() onClicked = new EventEmitter();
  public user: User;
  registerForm: FormGroup;
  submitted = false;

  constructor(private userService: UserDataService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6),
      PatternValidator(/\d/, { hasNumber: true }),
      PatternValidator(/[A-Z]/, { hasCapitalCase: true }),
      PatternValidator(/[a-z]/, { hasSmallCase: true }),
      PatternValidator(/[ !@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/, { hasSpecialCharacters: true })]
      ]
    });
  }

  get f() { return this.registerForm.controls; }

  registerUser(form) {
    console.log(form);
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    this.userService.registerUser(form.value).subscribe((() => {
      this.submitted = false;
      form.reset();
      this.onClicked.emit();
    }));
  }


}
