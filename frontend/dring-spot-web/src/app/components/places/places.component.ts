import { Component, OnInit } from '@angular/core';
import { MeetingPlaceService } from 'src/app/services/categories.service';
import { Observable } from 'rxjs';
import { MeetingPlace } from 'src/app/models/meeting-place.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PlaceDetailsComponent } from "../place-details/place-details.component";

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
      const modalRef = this.modalService.open(PlaceDetailsComponent, {size: 'xl'});
      const id = event.target.attributes.id.nodeValue.substring(2);
      console.log("Show place with id: ", id);
      modalRef.componentInstance.meetingPlace = x.find(x => x.id == id);
    });
  }

  constructor(private meetingPlaceService: MeetingPlaceService, private modalService: NgbModal) { }

  ngOnInit() {
  }

}
