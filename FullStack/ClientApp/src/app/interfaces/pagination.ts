import { Customer } from "./customer";

export interface Pagination {
    totalItems: number;
    currentPage: number;
    pageSize: number;
    totalPages: number;
    pages: number[];
    customerItemList: Customer[];
}

