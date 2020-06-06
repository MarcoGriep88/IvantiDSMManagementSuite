import { Http, RequestOptions, Headers } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TourRoutesService {



constructor(private http: Http) { }


getRoutesByTourId(id) {
    const head = new Headers({ 'Content-Type': 'application/json' });
    const token = localStorage.getItem('token');
    head.append('Authorization', 'Bearer ' + token);
    const requestOptions = new RequestOptions({headers: head});

    return this.http.get(environment.baseUrl + '/tourroutes/' + id, requestOptions)
      .map(response => response.json());
  }
}
