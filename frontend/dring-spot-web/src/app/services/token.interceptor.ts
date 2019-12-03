import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { AuthService } from '../shared/services/auth.service';
import { Observable } from 'rxjs';
@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public auth: AuthService) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (request.url.indexOf('maps.googleapis.com') == -1 )
    {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.auth.GetToken()}`
        }
      });
    }
    return next.handle(request);
  }
}