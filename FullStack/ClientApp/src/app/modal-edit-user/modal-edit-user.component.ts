import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { User } from '../_interfaces/user';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-edit-user',
  templateUrl: './modal-edit-user.component.html',
  styleUrls: ['./modal-edit-user.component.css']
})
export class ModalEditUserComponent implements OnInit {

  editUserForm: FormGroup;
  submitted = false;
  @Input() user: User;
  @Input() title: string;

  constructor(private formBuilder: FormBuilder, public activeModal: NgbActiveModal) { }

  ngOnInit() {
    this.editUserForm = this.formBuilder.group({
      id: [this.user.id],
      email: [this.user.email, [Validators.required, Validators.email]],
      rolesList: [this.user.rolesList, [Validators.required]],
    })
  }

  get f() { return this.editUserForm.controls; }

  saveUser(form) {
    this.submitted = true;
    if (this.editUserForm.invalid) {
      return;
    }
    this.activeModal.close(form.value);
  }

}
