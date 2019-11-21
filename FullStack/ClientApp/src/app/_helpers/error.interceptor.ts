import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService,private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if ([401, 403].indexOf(err.status) !== -1) {
                // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                console.log ("errorinterceptor 401/403");
                console.log (err);
                this.router.navigate(['/login']);
                if(err.error !="" || err.error != undefined || err.err != null)
                {
                    console.log("if err.error")
                    const error = err.error.message || err.error.title || err.error.description || err.error;

                   return throwError(error);

                }
                return throwError("Login again or contact your administrator");

            }

            const error = err.error.message || err.error.title || err.error.description;
            return throwError(error);
        }))
    }
}