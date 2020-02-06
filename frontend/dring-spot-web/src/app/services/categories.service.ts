import { Inject, Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { Category } from '../models/category.model';
import { HttpClient } from '@angular/common/http';
import { MeetingPlace } from '../models/meeting-place.model';
import { SerachPlaceModel } from '../models/search-place.model';
import { EventModel } from '../models/event.model';

@Injectable({
  providedIn: 'root'
})
export class MeetingPlaceService {
  private baseUrl: string = this.url + "MeetingPlace";

  constructor(
    private client: HttpClient,
    @Inject('BASE_URL') private url: string
  ) {

  }

  getCategories(): Observable<Category[]> {
    return this.client.get<Category[]>(this.baseUrl + "/GetCategories");
  }

  addPlace(name: string, lat: number, lng: number, text: string, categories: string[]): Observable<any> {
    return this.client.post(this.baseUrl + "/", {
      name: name,
      lat: lat,
      lon: lng,
      text: text,
      categories: categories,
      })
  };

  addReview(text: string, placeId: number) {
    return this.client.post(this.baseUrl + "/Review/" + placeId, { 
      text: text,
      date: new Date(),
    })
  }

  getPlaces(): Observable<MeetingPlace[]> {
    return this.client.get<MeetingPlace[]>(this.baseUrl + "/");
  }

  getPlace(id: number): Observable<MeetingPlace> {
    return this.client.get<MeetingPlace>(this.baseUrl + "/" + id);
  }

  searchPlaces(criteria: SerachPlaceModel): Observable<MeetingPlace[]> {
    return this.client.post<MeetingPlace[]>(this.baseUrl + "/Search", criteria);
  }

  loadEvents(): Observable<EventModel[]> {
    return this.client.get<EventModel[]>(this.url + "/Users/getEvents");
  }
}
