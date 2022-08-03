import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
    return this.httpClient.post<Cat>(this.path, data);
  }

  getCats(): Observable<Cat[]> {
    return this.httpClient.get<Cat[]>(this.path);
  }

  getCat(id): Observable<Cat> {
    return this.httpClient.get<Cat>(this.path + `/${id}`);
  }
}
