import { Customer } from "./customer";

export interface CustomerFilterPagination{
     totalItems : number;
     currentPage:number;
     pageSize:number;
     filterFirstName:string;
     filterLastName:string;
     filterAccountNumber:string;
     filterSumTotalDueHigher:number;
     filterSumTotalDueLower:number;
     maxPages:number;
     customerItemList:Customer[];
   
    
  
}