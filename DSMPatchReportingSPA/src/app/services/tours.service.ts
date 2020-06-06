import { Http, RequestOptions, Headers } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ToursService {
  issues: any[];

  constructor(private http: Http) {  }

  getStatus() {
    const head = new Headers({ 'Content-Type': 'application/json' });
    const token = localStorage.getItem('token');
    head.append('Authorization', 'Bearer ' + token);
    const requestOptions = new RequestOptions({headers: head});

    return this.http.get(environment.baseUrl + '/Status/', requestOptions)
      .map(response => response.json());
  }

  getTourCount() {
    this.getStatus().subscribe(response => {
     this.issues = response;
    });

    console.log(this.issues);

    return this.issues;
  }
}
