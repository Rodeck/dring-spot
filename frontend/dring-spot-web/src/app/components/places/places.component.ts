import { Component, OnInit } from '@angular/core';
import { MeetingPlaceService } from 'src/app/services/categories.service';
import { Observable, of, combineLatest } from 'rxjs';
import { MeetingPlace } from 'src/app/models/meeting-place.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PlaceDetailsComponent } from "../place-details/place-details.component";
import { AppState, BaseState } from 'src/app/store/state/app.state';
import { Store } from '@ngrx/store';
import { selectPlaces, selectPlaceDetails, selectCategories, selectSearchedPlaces, selectAddress } from 'src/app/store/selectors/app.selectors';
import { showPlaceDetails, hidePlaceDetails, searchForPlaces, loadAddress } from 'src/app/store/actions/app.actions';
import { Category } from 'src/app/models/category.model';
import { map, withLatestFrom } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { MapsService } from 'src/app/services/maps.service';

@Component({
  selector: 'app-places',
  templateUrl: './places.component.html',
  styleUrls: ['./places.component.css']
})
export class PlacesComponent implements OnInit {

  searchedPlaces$: Observable<MeetingPlace[]> = this.store.select(selectPlaces);
  categories$: Observable<Category[]> = this.store.select(selectCategories);
  filteredCategories$: Observable<Category[]>;
  showDetails: boolean;
  categoryFilter$: Observable<string>;
  categoryFilterValue;
  isFiltered: boolean = false;
  location: FormControl;
  range: number = 5.0;
  rangeJump: number = 0.5;
  locationAddress: Observable<string> = this.store.select(selectAddress);
  choosenCategories: string[] = [];
  lat: number;
  lng: number;

  get rangeString() {
    return `${this.range} km`; 
  }

  showPlaceDetails(event) {
    const id = event.target.attributes.id.nodeValue.substring(2);
    this.store.dispatch(showPlaceDetails({id: parseInt(id)}));
  }

  toogleCategory(category: Category) {
    if (this.choosenCategories.indexOf(category.name) === -1)
      this.choosenCategories.push(category.name);
    else 
      this.choosenCategories = this.choosenCategories.filter(x => x != category.name);
  }

  isSelected(name: string) {
    return this.choosenCategories.indexOf(name) != -1;
  }

  increaseRange() {
    this.range += this.rangeJump;
  }

  decreaseRange() {
    this.range -= this.rangeJump;
  }

  getLocation(): void{
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position)=>{
          this.lat = position.coords.latitude;
          this.lng = position.coords.longitude;
          this.store.dispatch(loadAddress({ lat: position.coords.latitude.toString(), lng: position.coords.longitude.toString() }))
        });
    } else {
       console.log("No support for geolocation")
    }
  }

  searchPlaces() {
    this.store.dispatch(searchForPlaces({
      criteria: {
        categories: this.choosenCategories,
        lat: this.lat,
        lng: this.lng,
        name: '',
        range: this.range * 1000
      }
    }));
  }

  constructor(
    private modalService: NgbModal,
    private store: Store<BaseState>,
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

    this.categoryFilterValue = new FormControl('');
    this.categoryFilter$ = this.categoryFilterValue.valueChanges;
    this.filteredCategories$ = combineLatest(this.categoryFilter$, this.categories$).pipe(
      map(([category, categories]) => {
        return categories.filter(x => x.name.toUpperCase().startsWith(category.toUpperCase()))
      })
    );

    this.categoryFilter$.subscribe(x => this.isFiltered = x !== null || x !== '');

    this.location = new FormControl('');

    this.locationAddress.subscribe(x => this.location.setValue(x));
  }

}
