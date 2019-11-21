import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService, private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {

            if ([401, 403].indexOf(err.status) !== -1) {
                // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                console.log("errorinterceptor 401/403");
                this.router.navigate(['/login']);

                if (err.error === null || err.error === undefined || err.error === "") {
                    return throwError("Login again or contact your administrator");
                }

                const error = err.error.message || err.error.title || err.error.description || err.error;

                return throwError(error);

            }

            const error = err.error.message || err.error.title || err.error.description || err.error;
            return throwError(error);
        }))
    }
}