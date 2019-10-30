import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';
import { Customer } from '../interfaces/customer';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  private customerlist;
  
  public displayedColumns = ["accountNumber", "firstName","lastName","sumTotalDue"];
  public dataSource = new MatTableDataSource<Customer>();


  constructor(private service:DataService) { 
    this.GetListCustomer();
  }

  ngOnInit() {
    this.GetDataSourceCustomer();
  }

  GetListCustomer(){
    this.service.GetAllCustomers("api/customers").subscribe(
      (result) =>{
        console.log(result);
        this.customerlist = result;
      }
    )
  }

  GetDataSourceCustomer = ()=>{
    this.service.GetAllCustomers("api/customers").subscribe(
      (result) =>{
        console.log(result);
        this.dataSource.data = result as Customer[];
      }
    )
  }

}
