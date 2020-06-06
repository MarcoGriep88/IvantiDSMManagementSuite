import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { AuthService } from '../services/auth.service';
import { ToursService } from '../services/tours.service';

@Component({
  selector: 'app-navigation-left',
  templateUrl: './navigation-left.component.html',
  styleUrls: ['./navigation-left.component.css']
})
export class NavigationLeftComponent implements OnInit {
  issues: any[];

  constructor(private http: Http,
    private authService: AuthService,
    private tourService: ToursService) { }

  ngOnInit() {
    this.tourService.getStatus().subscribe(response => {
      this.issues = response;
    });
  }

  getCurrentUser() {
    return this.authService.currentUser.unique_name;
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

}
