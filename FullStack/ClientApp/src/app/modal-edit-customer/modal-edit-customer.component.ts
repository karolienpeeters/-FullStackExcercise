import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Customer } from '../_interfaces/customer';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-edit-customer',
  templateUrl: './modal-edit-customer.component.html',
  styleUrls: ['./modal-edit-customer.component.css']
})
export class ModalEditCustomerComponent implements OnInit {
  editCustomerForm: FormGroup;
  submitted = false;
  @Input() customer: Customer;
  @Input() title: string;

  constructor(private formBuilder: FormBuilder, public activeModal: NgbActiveModal) { }

  ngOnInit() {
    this.editCustomerForm = this.formBuilder.group({
      id: [this.customer.id],
      accountNumber: [this.customer.accountNumber],
      firstName: [this.customer.firstName, [Validators.required]],
      lastName: [this.customer.lastName, [Validators.required]],
    })
  }

  get f() { return this.editCustomerForm.controls; }

  saveCustomer(form) {
    // console.log('savecustomer', form.value)

    this.submitted = true;
    if (this.editCustomerForm.invalid) {
      return;
    }
    this.activeModal.close(form.value);
  }

}
