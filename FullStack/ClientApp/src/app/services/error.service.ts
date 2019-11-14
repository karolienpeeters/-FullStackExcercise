import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  getClientErrorMessage(error: Error): string {    
    console.log("errorservice get client errormessage",error)
    
    return error.message ? 
           error.message : 
           error.toString();
  }

  getServerErrorMessage(error: HttpErrorResponse): string {
    console.log("errorservice get server errormessage",error)
    
    return navigator.onLine ?    
           error.message :
           'No Internet Connection';
  }    
}