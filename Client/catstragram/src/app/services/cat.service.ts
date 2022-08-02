import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Cat } from '../models/cat';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class CatService {
  private path: string = environment.apiUrl + 'cats'
  constructor(private httpClient: HttpClient, private authService: AuthenticationService) { }

  create(data: any): Observable<Cat> {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.httpClient.post<Cat>(this.path, data, { headers });
  }
}
