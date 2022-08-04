import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError, Observable, retry, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor {

  constructor(private toastrService: ToastrService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        retry(1),
        catchError((err) => {
          let message = '';
          if (err.status === 401) {
            message = 'Token has expired or you should be logged in.'
          } else if (err.status == 404) {
            message = 'This resource is not found';
            this.toastrService.error(message);
          } else if (err.status == 400) {
            message = 'Invalid data';
          } else {
            message = 'Unexpected error';
          }

          this.toastrService.error(message);

          return throwError(() => err);
        })
      )
  }
}
