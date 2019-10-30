import { Customer } from "./customer";

export interface CustomerFilterPagination{
     totalItems : number;
     currentPage:number;
     pageSize:number;
     filterFirstName:string;
     filterLastName:string;
     filterAccountNumber:string;
     filterSumTotalDue:number;
     higher:boolean;
     lower:boolean;
     customerItemList:Customer;
   
    
  
}