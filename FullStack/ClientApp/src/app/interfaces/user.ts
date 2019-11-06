export interface User {
    userId: string;
    email: string;
    rolesList: string[];
    showForm: boolean;
    token?:string;
    isAdmin:boolean;

}