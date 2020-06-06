import { Component, OnInit } from '@angular/core';
import { GearService } from 'src/app/services/gear.service';
import { Http } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-GearOverview',
  templateUrl: './GearOverview.component.html',
  styleUrls: ['./GearOverview.component.css']
})
export class GearOverviewComponent implements OnInit {

  gearOriginal: any[];
  gear: any[];
  finishedLoading: boolean;

  constructor(private http: Http,
    private gearService: GearService,
    private router: Router,
    private route: ActivatedRoute) {
      this.finishedLoading = false;
    }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.gearService.getGearById(params.id).subscribe(response => {
        this.finishedLoading = true;
        this.gear = response;
        this.gearOriginal = this.gear;
      }, (error: Response) => {
        if (error.status === 200) {
          console.log('Error: ' + error.status);
        } else {
          console.log('Enexpected: ' + error.status);
          alert('An unexpected error occured.');
        }
      });
    });
  }

  filterData(newValue){
    if (newValue === '') {
      this.gear = this.gearOriginal;
      return;
    } else {
      const tmpgear: any[] = [];
      const filter = newValue.toLowerCase();
      this.gearOriginal.forEach(function (g) {
        if (g.patch.toLowerCase().includes(filter) ||
            g.computer.toLowerCase().includes(filter) ||
            g.compliance.toLowerCase().includes(filter)) {
          tmpgear.push(g);
      }
      });
      this.gear = tmpgear;
      }
    }
}
