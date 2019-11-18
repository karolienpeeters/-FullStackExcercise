import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEditCustomerComponent } from './modal-edit-customer.component';

describe('ModalEditCustomerComponent', () => {
  let component: ModalEditCustomerComponent;
  let fixture: ComponentFixture<ModalEditCustomerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModalEditCustomerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalEditCustomerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
