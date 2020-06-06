import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import { JwtHelper, tokenNotExpired } from 'angular2-jwt';
import { tokenKey } from '@angular/core/src/view';
import { environment } from '../../environments/environment';
import { MomentModule } from 'angular2-moment';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class GearService {

  gear: any[];

  constructor(private http: Http) {  }

  getGear() {
    const head = new Headers({ 'Content-Type': 'application/json' });
    const token = localStorage.getItem('token');
    head.append('Authorization', 'Bearer ' + token);
    const requestOptions = new RequestOptions({headers: head});

    return this.http.get(environment.baseUrl + '/gear/', requestOptions)
      .map(response => response.json());
  }

  getGearById(id) {
    const myMoment: moment.Moment = moment(id);
    const dateString = myMoment.format('YYYYMMDD');
    const head = new Headers({ 'Content-Type': 'application/json' });
    const token = localStorage.getItem('token');
    head.append('Authorization', 'Bearer ' + token);
    const requestOptions = new RequestOptions({headers: head});

    return this.http.get(environment.baseUrl + '/PatchData/date/' + dateString, requestOptions)
      .map(response => response.json());
  }


  getGearCount() {
    this.getGear().subscribe(response => {
     this.gear = response;
    });

    console.log(this.gear);

    return this.gear;
  }
}
