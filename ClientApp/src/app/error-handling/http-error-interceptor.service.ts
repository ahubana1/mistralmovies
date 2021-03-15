import {
  HttpHandler,
  HttpRequest,
  HttpEvent,
  HttpErrorResponse,
  HttpInterceptor
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { ErrorDialogService } from "../shared/error-dialog/error-dialog.service";
import { LoadingDialogService } from "../shared/loading-dialog/loading-dialog.service";

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(
    private errorDialogService: ErrorDialogService,
    private loadingDialogService: LoadingDialogService
  ) { }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.loadingDialogService.openDialog();
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error("Error from error interceptor", error);
        this.errorDialogService.openDialog(error.message ? error.message : JSON.stringify(error), error.status);
        return throwError(error);
      }),
      finalize(() => {
        this.loadingDialogService.hideDialog();
      })
    ) as Observable<HttpEvent<any>>;
  }
}
