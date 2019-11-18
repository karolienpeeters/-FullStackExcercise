import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { LoginComponent } from './login/login.component';
import { UserListComponent } from './user-list/user-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { AdminGuard } from './_guards/admin.guard';
import { PaginationComponent } from './pagination/pagination.component';
import { CustomerSearchComponent } from './customer-search/customer-search.component';
import { UserCreateComponent } from './user-create/user-create.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { GlobalErrorHandler } from './_helpers/global-error-handler';
import { NgbModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from './modal/modal.component';
import { ModalEditCustomerComponent } from './modal-edit-customer/modal-edit-customer.component';
import { ModalEditUserComponent } from './modal-edit-user/modal-edit-user.component';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CustomerListComponent,
    LoginComponent,
    UserListComponent,
    PaginationComponent,
    CustomerSearchComponent,
    UserCreateComponent,
    ModalComponent,
    ModalEditCustomerComponent,
    ModalEditUserComponent
  ],

  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatSnackBarModule,
    ReactiveFormsModule,
    NgbModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: CustomerListComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'customers', component: CustomerListComponent, canActivate: [AuthGuard] },
      { path: 'users', component: UserListComponent, canActivate: [AuthGuard, AdminGuard], data: { role: "Admin" } },
      { path: '**', component: LoginComponent }

    ])
  ],
  providers: [
    NgbActiveModal,
    { provide: ErrorHandler, useClass: GlobalErrorHandler },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    ModalEditCustomerComponent,
    ModalEditUserComponent
  ]
})
export class AppModule { }
