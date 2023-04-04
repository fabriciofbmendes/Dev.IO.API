import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly API = 'http://localhost:5000/api';

  constructor(private http: HttpClient) { }

  logout() {
    return this.http.post(`${this.API}/sair`, {})
      .pipe(
        tap(() => {
          localStorage.removeItem('access_token');
        })
      );
  }
}