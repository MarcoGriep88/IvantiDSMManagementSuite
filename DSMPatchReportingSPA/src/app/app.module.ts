import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HttpModule, Http, BaseRequestOptions, Headers } from '@angular/http';
import { NavigationMainComponent } from './navigation-main/navigation-main.component';
import { TourenComponent } from './DashboardWidgets/touren/touren.component';
import { NavigationLeftComponent } from './navigation-left/navigation-left.component';
import { HomeComponent } from './home/home.component';

import { LoginComponent } from './Views/Profile/login/login.component';
import { AdminComponent } from './admin/admin.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { NoAccessComponent } from './no-access/no-access.component';
import { FormsModule } from '@angular/forms';

import { RouterModule } from '@angular/router';

import { AuthHttp, AUTH_PROVIDERS, provideAuth, AuthConfig } from 'angular2-jwt/angular2-jwt';
import { OrderService } from './services/order.service';
import { AuthService } from './services/auth.service';
import { ToursService } from './services/tours.service';

import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { TourenControlComponent } from './Views/Touren/tourenControl/tourenControl.component';
import { RegisterComponent } from './Views/Profile/Register/Register.component';
import { GearService } from './services/gear.service';
import { GearOverviewComponent } from './Views/Gear/GearOverview/GearOverview.component';

import { GoogleChartsModule } from 'angular-google-charts';

import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MomentModule } from 'angular2-moment';

@NgModule({
   declarations: [
      AppComponent,
      NavigationMainComponent,
      TourenComponent,
      NavigationLeftComponent,
      HomeComponent,
      LoginComponent,
      AdminComponent,
      NotFoundComponent,
      NoAccessComponent,
      TourenControlComponent,
      RegisterComponent,
      GearOverviewComponent,
   ],
   imports: [
      BrowserAnimationsModule,
      GoogleChartsModule,
      BrowserModule,
      HttpModule,
      MomentModule,
      ChartsModule,
      FormsModule,
      RouterModule.forRoot([
        { path: '', component: HomeComponent },
        { path: 'touren', component: TourenControlComponent},
        { path: 'patchOverview/:id', component: GearOverviewComponent},
        { path: 'admin', component: AdminComponent },
        { path: 'login', component: LoginComponent },
        { path: 'register', component: RegisterComponent },
        { path: 'no-access', component: NoAccessComponent, },
        { path: '**', component: HomeComponent }
      ])
   ],
   providers: [
    OrderService,
    AuthService,
    ToursService,
    GearService
  ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
