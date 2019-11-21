import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitted = false;


  constructor(private auth: AuthService, private route: Router, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]
      ]
    });
  }

  get f() { return this.loginForm.controls; }


  login(form) {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    this.auth.login(form.value).subscribe(() => {
      this.route.navigate(["/customers"]);
    })
  }

}
