import { Customer } from "./customer";
import { Pagination } from "./pagination";

export interface CustomerFilterPagination{
     filterFirstName:string;
     filterLastName:string;
     filterAccountNumber:string;
     filterSumTotalDueHigher:number;
     filterSumTotalDueLower:number;
   
     pagination:Pagination;
}