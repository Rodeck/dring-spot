import { Component, OnInit, Input } from '@angular/core';
import { MeetingPlace } from 'src/app/models/meeting-place.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';

@Component({
  selector: 'app-place-details',
  templateUrl: './place-details.component.html',
  styleUrls: ['./place-details.component.css']
})
export class PlaceDetailsComponent implements OnInit {

  @Input() meetingPlace: MeetingPlace;

  constructor(public activeModal: NgbActiveModal) {}

  ngOnInit() {
  }

  calcDate(date: Date) {
    const today =  moment();
    const diff = moment(date).diff(today, 'days');
    return diff == 0 ? 'today' : `${diff} days ago`;
  }

}
