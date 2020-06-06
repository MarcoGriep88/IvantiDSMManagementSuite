import { Component, OnInit, ElementRef, Renderer, ViewChild } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { AuthService } from './../services/auth.service';
import { ToursService } from '../services/tours.service';
import { Router } from '@angular/router';
import { GearService } from '../services/gear.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  notfixedCount: number;
  fixedCount: number;
  issues: any[];

  gearCount: number;
  gear: any[];

  title = 'Verlauf offener Sicherheitslücken';
   type = 'LineChart';
   data = [
   ];
   columnNames = ['Date', 'offene Sicherheitslücken'];
   options = {
   };
   height = 400;

  constructor(
    private http: Http,
    private authService: AuthService,
    private tourService: ToursService,
    private gearService: GearService,
    private router: Router) {

    }

  ngOnInit() {
    this.tourService.getStatus().subscribe(response => {
      this.issues = response;

      console.log(this.issues);
      this.notfixedCount = this.issues[this.issues.length - 1].notFixed;
      this.fixedCount = this.issues[this.issues.length - 1].fixed;
      for (let i = 0; i < this.issues.length; i++) {
        this.data.push([this.issues[i].date, this.issues[i].notFixed]);
        console.log(this.data);
      }
    });

    this.gearService.getGear().subscribe(response => {
      this.gear = response;
      this.gearCount = this.gear.length;
    });
  }

  isLoggedIn() {
    if (this.authService.isLoggedIn()) {
      return true;
    }
    this.router.navigate(['/login']);
  }

  getCurrentUser() {
    return this.authService.currentUser.unique_name;
  }
}
