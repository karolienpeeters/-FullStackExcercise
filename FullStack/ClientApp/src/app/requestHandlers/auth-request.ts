import { Headers, Http, BaseRequestOptions, RequestOptionsArgs, RequestOptions } from '@angular/http';
import { TOKEN_NAME } from '../services/auth.service';
import { Injectable } from '@angular/core';

const AUTH_HEADER_KEY = 'Authorization';
const AUTH_PREFIX = 'Bearer';

@Injectable() 

export class AuthRequestOptions extends BaseRequestOptions {
  
    constructor() {
        super();
        this.headers.set('Content-Type', 'application/json');
    }
    merge(options?: RequestOptionsArgs): RequestOptions {
        const token = localStorage.getItem(TOKEN_NAME);
        const newOptions = super.merge(options);
        if (token) {
            newOptions.headers.set('Authorization: ', `Bearer ${token}`);
        }

        return newOptions;
    }

}