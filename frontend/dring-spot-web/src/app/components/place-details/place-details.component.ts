import { Component, OnInit, Input } from '@angular/core';
import { MeetingPlace } from 'src/app/models/meeting-place.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import { ReviewService } from 'src/app/services/review.service';
import { AppState, BaseState } from 'src/app/store/state/app.state';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs/internal/Observable';
import { selectPlaceDetails } from 'src/app/store/selectors/app.selectors';
import { loadPlace } from 'src/app/store/actions/app.actions';

@Component({
  selector: 'app-place-details',
  templateUrl: './place-details.component.html',
  styleUrls: ['./place-details.component.css']
})
export class PlaceDetailsComponent implements OnInit {

  meetingPlace$: Observable<MeetingPlace> = this.store.select(selectPlaceDetails);
  private showAddButton: boolean;
  description: string;
  isLoading: boolean = true;

  constructor(public activeModal: NgbActiveModal, 
    private reviewService: ReviewService,
    private store: Store<BaseState>
  ) {}

  ngOnInit() {
    //this.isLoading = false;
  }

  calcDate(date: Date) {
    const today =  moment();
    const diff = today.diff(moment(date), 'days');
    return diff == 0 ? 'today' : diff === 1 ? `${diff} day ago` : `${diff} days ago`;
  }

  addReview(placeId: string) {
    const id = parseInt(placeId);
    this.reviewService.addReview(this.description, id)
      .subscribe(x => {
        alert("Success!");
        this.store.dispatch(loadPlace({id: id}));
      })
  }

  commentFocus() {
    this.showAddButton = true;
  }

  commentFocusOut() {
    this.showAddButton = false;
  }

  voteForComment(reviewId: number, isPositive: boolean) {
    console.log("Vote for ", reviewId, ", Positive:", isPositive);
    this.reviewService.voteForReview(reviewId, isPositive)
      .subscribe(x => alert("Success!"));
  }

}
