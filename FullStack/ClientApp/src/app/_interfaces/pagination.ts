import { User } from "./user";
import { Customer } from "./customer";

export interface Pagination {
    totalItems: number;
    currentPage: number;
    pageSize: number;
    userList: User[];
    customerList: Customer[];
}
