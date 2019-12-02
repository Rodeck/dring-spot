import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private baseUrl: string = this.url + "Review";

  constructor(
    private client: HttpClient,
    @Inject('BASE_URL') private url: string
  ) {

  }

  addReview(text: string, placeId: number) {
    return this.client.post(this.baseUrl + "/Review/" + placeId, { 
      text: text,
      date: new Date(),
    })
  }

  voteForReview(id: number, isPositive: boolean) {
    return this.client.post(this.baseUrl + "/Vote/" + id, { 
      isPositive: isPositive,
      date: new Date(),
    })
  }
}
