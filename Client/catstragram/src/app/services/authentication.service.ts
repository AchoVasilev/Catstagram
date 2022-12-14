import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private loginPath: string = environment.apiUrl + 'identity/login';
  private registerPath: string = environment.apiUrl + 'identity/register';

  constructor(private httpClient: HttpClient) { }

  login(data: any): Observable<any> {
    return this.httpClient.post(this.loginPath, data);
  }

  register(data: any): Observable<any> {
    return this.httpClient.post(this.registerPath, data);
  }

  saveToken(token: any) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isAuthenticated() {
    if (this.getToken()) {
      return true;
    }

    return false;
  }
}
