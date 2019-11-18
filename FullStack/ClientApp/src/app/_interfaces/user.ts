export interface User {
    id: string;
    email: string;
    rolesList: string[];
    token?:string;
    isAdmin:boolean;

}