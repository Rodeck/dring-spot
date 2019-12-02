import { Component, OnInit, NgZone } from '@angular/core';
import { AuthService } from "../../shared/services/auth.service";
import { Router } from "@angular/router";
import { Observable } from 'rxjs';
import { UserModel } from 'src/app/models/user.model';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public activeComponent: string = "places";

  public actiavateComponent(component: string) {
    this.activeComponent = component;
  }

  constructor(
    public router: Router,
    public ngZone: NgZone
  ) { }

  ngOnInit() { }

}
