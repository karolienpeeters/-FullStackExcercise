import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService,private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if ([401, 403].indexOf(err.status) !== -1) {
                // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                console.log ("errorinterceptor 401/403")
            
                this.router.navigate(['/login']);
                return throwError("Please login again or contact your administrator");

            }

            console.log("error interceptor")
            console.log(err)

                


            const error = err.error.message || err.error.title || err.error.description;
            console.log(err.error)
          
            console.log(error)
            return throwError(error);
        }))
    }
}