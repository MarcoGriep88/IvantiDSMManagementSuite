import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import { JwtHelper, tokenNotExpired } from 'angular2-jwt';
import { tokenKey } from '@angular/core/src/view';
import { environment } from '../../environments/environment';

@Injectable()
export class AuthService {
  constructor(private http: Http) {
  }

  login(credentials) {
  console.log(credentials);
  const head = new Headers({ 'Content-Type': 'application/json' });
  const requestOptions = new RequestOptions({headers: head});
   return this.http.post(environment.baseUrl + '/auth/login',
      JSON.stringify(credentials), requestOptions).
      map(response => {
        console.log(response);
        const result = response.json();
        if (result && result.token) {
          localStorage.setItem('token', result.token);
          return true;
        }
        return false;
      });
  }

  logout() {
    localStorage.removeItem('token');
  }

  isLoggedIn() {
    return tokenNotExpired(); /*Build in Funktionalität */

    /*let jwtHelper = new JwtHelper();
    let token = localStorage.getItem('token');

    if (!token)
      return false;

    let expirationDate = jwtHelper.getTokenExpirationDate(token);
    let isExpired = jwtHelper.isTokenExpired(token);

    return !isExpired;*/
  }

  getUserId() {
    if (!this.isLoggedIn) {
      return false;
    }

      const jwtHelper = new JwtHelper();
      const token = localStorage.getItem('token');

    if (!token) {
      return false;
    }

    const decoded = jwtHelper.decodeToken(token);
    const nameid = decoded['nameid'];

    console.log(nameid);

    return nameid;
  }

  get currentUser() {
    const token = localStorage.getItem('token');
    if (!token) { return null; }

    return new JwtHelper().decodeToken(token);
  }
}

