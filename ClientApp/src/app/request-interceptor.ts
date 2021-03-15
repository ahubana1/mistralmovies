import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Subject, BehaviorSubject, Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export abstract class NoopInterceptor implements HttpInterceptor {
    constructor() { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(this.injectApiKey(request))
    } //intercept

    injectApiKey(request: HttpRequest<any>) {
        return request.clone({
            setHeaders: {
                ApiKey: "5tBVCubvYk"
            }
        });
    }
}

export const httpInterceptorProviders = [
    { provide: HTTP_INTERCEPTORS, useClass: NoopInterceptor, multi: true },
];