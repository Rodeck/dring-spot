import { Component, OnInit } from '@angular/core';
import { MeetingPlaceService } from 'src/app/services/categories.service';
import { Observable } from 'rxjs';
import { Category } from 'src/app/models/category.model';
import { MeetingPlace } from 'src/app/models/meeting-place.model';

@Component({
  selector: 'app-meeting-place-add',
  templateUrl: './meeting-place-add.component.html',
  styleUrls: ['./meeting-place-add.component.css']
})
export class MeetingPlaceAddComponent implements OnInit {

  lat = 51.678418;
  lng = 7.809007;

  allCategories: Observable<Category[]> = this.meetingPlaceService.getCategories();
  selectedCategories: string[] = new Array<string>();
  name: string;
  text: string;

  getLocation(): void{
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position)=>{
          this.lng= position.coords.longitude;
          this.lat = position.coords.latitude;
        });
    } else {
       console.log("No support for geolocation")
    }
  }

  moveMarker(lat, lng) {
    this.lat = lat;
    this.lng = lng;
  }

  selectCategory(event) {
    console.log(event);
    const text = event.target.innerText;
    if (this.selectedCategories.indexOf(text) == -1)
    {
      this.selectedCategories.push(text);
    }
  }

  unselectCategory(event) {
    console.log(event);
    const text = event.target.innerText;
    if (this.selectedCategories.indexOf(text) != -1)
    {
      this.selectedCategories = this.selectedCategories.filter(x => x != text);
    }
  }

  addPlace() {
    this.meetingPlaceService.addPlace(this.name, this.lat, this.lng, this.text, this.selectedCategories).subscribe(x =>
    {
      alert("Success!");
    });
  }
  
  constructor(private meetingPlaceService: MeetingPlaceService) { }

  ngOnInit() {
    this.getLocation();
  }

}
