import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReverseGeocodingResult } from '../models/reverse-geocoding.model';

@Injectable({
  providedIn: 'root'
})
export class MapsService {

  constructor(private client: HttpClient, @Inject("MAPS_URL") private mapsUrl, @Inject("MAPS_APIKEY") private mapsApiKey) { }

  /**
   * getAddress
lat: string, lng: string   */
  public getAddress(lat: string, lng: string) {
    return this.client.get<ReverseGeocodingResult>(this.mapsUrl + `?latlng=${lat},${lng}&key=${this.mapsApiKey}`);
  }
}
