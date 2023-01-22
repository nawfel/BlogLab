import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';
import { catchError } from 'rxjs/operators'
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private toastr: ToastrService,
    private accountService: AccountService
  ) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch (error.status) {
            case 400:
            //  this.handle400Error(error);

              break;
            case 401:
              this.handle401Error(error);

              break;
            case 500:
              this.handle500Error(error);
              break;
            default:
              this.handleUnexpected(error);
              break;
          }
        }
        return throwError(error);
      })
    );
  }

  handle400Error(error: any) {
    if (!!error.error && Array.isArray(error.error)) {
      let errorMessage = '';
      for (const key in error.error) {
        if (!!error.error[key]) {

          const errorElement = error.error[key];
          errorMessage = (`${errorMessage}${errorElement.code}-${errorElement.description}\n`)
        }
      }
      this.toastr.error(errorMessage, error.statusText);
      console.log(error.error);
    }
    else if (!!error?.error.errors?.Content && (typeof error.error.error.Content) === 'object') {
      let errorObject = error.error.errors.Content;
      let errorMessage = '';
      for (const key in errorObject) {
        const errorElement = errorObject[key];
        errorMessage = (`${errorElement}\n`)
      }
      this.toastr.error(errorMessage, error.statusCode);
      console.log(error.error);
    }
    else if (!!error.error) {
      let errorMessage = ((typeof error.error)=== 'string')
        ? error.error
        : 'There was a validation error.';
      this.toastr.error(errorMessage, error.statusCode);
      console.log(error.error);
    }
    else {
      this.toastr.error(error.statusText, error.status);
      console.log(error.error);
    }
  }
  handle401Error(error: any) {
    let errorMessage = 'Please login yo your account';
    this.accountService.logout();
    this.toastr.error(errorMessage, error.statusCode);
  }
  handle500Error(error: any) {
    this.toastr.error('Please contact the administrator. An error happened is the server');
    console.log(error);
  }
  handleUnexpected(error: any) {
    this.toastr.error('some unexpected happened');
    console.log(error);
  }

}
