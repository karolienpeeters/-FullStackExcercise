import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MaterialModule } from "./modules/material.module";
//import {JwPaginationComponent } from 'jw-angular-pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { PaginationComponent } from './pagination/pagination.component';
import { LoginComponent } from './login/login.component';
import { UserListComponent } from './user-list/user-list.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthRequestOptions } from './requestHandlers/auth-request';
import { RequestOptions } from '@angular/http';






@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CustomerListComponent,
    PaginationComponent,
    LoginComponent,
    UserListComponent
  ],

  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MaterialModule,
    //JwPaginationComponent ,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'customers', component: CustomerListComponent, canActivate: [AuthGuard], },
      { path: 'users', component: UserListComponent, canActivate: [AuthGuard], },

    ])
  ],
  providers: [
    {
      provide: RequestOptions, 
      useClass: AuthRequestOptions
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
