import { Component, OnInit } from '@angular/core';
import { MeetingPlaceService } from 'src/app/services/categories.service';
import { Observable } from 'rxjs';
import { MeetingPlace } from 'src/app/models/meeting-place.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PlaceDetailsComponent } from "../place-details/place-details.component";
import { AppState, BaseState } from 'src/app/store/state/app.state';
import { Store } from '@ngrx/store';
import { selectPlaces, selectPlaceDetails, selectCategories, selectSearchedPlaces } from 'src/app/store/selectors/app.selectors';
import { showPlaceDetails, hidePlaceDetails, searchForPlaces } from 'src/app/store/actions/app.actions';
import { Category } from 'src/app/models/category.model';

@Component({
  selector: 'app-places',
  templateUrl: './places.component.html',
  styleUrls: ['./places.component.css']
})
export class PlacesComponent implements OnInit {

  searchedPlaces: Observable<MeetingPlace[]> = this.store.select(selectSearchedPlaces);
  categories: Observable<Category[]> = this.store.select(selectCategories);
  showDetails: boolean;

  showPlaceDetails(event) {
    const id = event.target.attributes.id.nodeValue.substring(2);
    this.store.dispatch(showPlaceDetails({id: parseInt(id)}));
  }

  searchPlaces() {
    this.store.dispatch(searchForPlaces());
  }


  constructor(
    private modalService: NgbModal,
    private store: Store<BaseState>
  ) { }

  ngOnInit() {
    this.store.select(selectPlaceDetails).subscribe(x => {
      if (x != null)
      {
        this.modalService.open(PlaceDetailsComponent, {size: 'xl', beforeDismiss: () => {
          this.store.dispatch(hidePlaceDetails());
          return true;
        }}); 
        console.log("Show place with id: ", x.id);
      }
    });
  }

}
