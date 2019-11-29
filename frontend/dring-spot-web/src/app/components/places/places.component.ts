import { Component, OnInit } from '@angular/core';
import { MeetingPlaceService } from 'src/app/services/categories.service';
import { Observable } from 'rxjs';
import { MeetingPlace } from 'src/app/models/meeting-place.model';

@Component({
  selector: 'app-places',
  templateUrl: './places.component.html',
  styleUrls: ['./places.component.css']
})
export class PlacesComponent implements OnInit {

  places: Observable<MeetingPlace[]> = this.meetingPlaceService.getPlaces();
  currentPlace: MeetingPlace;
  showDetails: boolean;

  showPlaceDetails(event) {
    this.places.subscribe(x => {
      this.currentPlace = x.find(x => x.id == event.target.attributes.id);
    });
    this.showDetails = true;
  }

  constructor(private meetingPlaceService: MeetingPlaceService) { }

  ngOnInit() {
  }

}
