import { Component, OnInit } from '@angular/core';
import { AuthService } from './../services/auth.service';

@Component({
  selector: 'app-navigation-main',
  templateUrl: './navigation-main.component.html',
  styleUrls: ['./navigation-main.component.css']
})
export class NavigationMainComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  logout() {
    this.authService.logout();
  }

  getCurrentUser() {
    return this.authService.currentUser.unique_name;
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

}
