import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ToursService } from '../../../services/tours.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tourenControl',
  templateUrl: './tourenControl.component.html',
  styleUrls: ['./tourenControl.component.css']
})
export class TourenControlComponent implements OnInit {

  touren: any[];
  finishedLoading: boolean;

  constructor(private http: Http,
    private tours: ToursService,
    private router: Router) { 
      this.finishedLoading = false;
    }

  ngOnInit() {
    this.tours.getStatus().subscribe(response => {
      this.touren = response;
    }, (error: Response) => {
      if (error.status === 200) {
        console.log('Error: ' + error.status);
      } else {
        console.log('Unexpected: ' + error.status);
        alert('An unexpected error occured.');
      }
    });
  }
}
