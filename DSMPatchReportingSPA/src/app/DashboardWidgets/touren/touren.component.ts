import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ToursService } from '../../services/tours.service';

@Component({
  selector: 'app-touren',
  templateUrl: './touren.component.html',
  styleUrls: ['./touren.component.css']
})
export class TourenComponent implements OnInit {
  touren: any[];
  finishedLoading: boolean;

  constructor(private http: Http, private tours: ToursService) { 
    this.finishedLoading = false;
  }

  ngOnInit() {
    this.tours.getStatus().subscribe(response => {
      this.touren = response;
      this.finishedLoading = true;
    }, (error: Response) => {
      if (error.status === 200) {
        console.log('Error: ' + error.status);
      } else {
        console.log('Enexpected: ' + error.status);
        alert('An unexpected error occured.');
      }
    });
  }
}
