import { Component, OnInit, Input } from '@angular/core';
import { MeetingPlace } from 'src/app/models/meeting-place.model';

@Component({
  selector: 'app-place-details',
  templateUrl: './place-details.component.html',
  styleUrls: ['./place-details.component.css']
})
export class PlaceDetailsComponent implements OnInit {

  @Input() meetinPlace: MeetingPlace;

  constructor() { }

  ngOnInit() {
  }

}
