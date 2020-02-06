import { createSelector } from '@ngrx/store';
import { AppState, BaseState } from '../state/app.state';
import { MeetingPlace } from 'src/app/models/meeting-place.model';
import { Category } from 'src/app/models/category.model';
import { EventModel } from 'src/app/models/event.model';

export const selectState = (state: BaseState): AppState => state.appState;

export const selectPlaceDetails = createSelector(
    selectState,
    (app: AppState): MeetingPlace => app.placeDetails
);

export const selectPlaces = createSelector(
    selectState,
    (app: AppState): MeetingPlace[] => app.places
);

export const selectSearchedPlaces = createSelector(
    selectState,
    (app: AppState): MeetingPlace[] => app.searched ? app.places : null
);

export const selectAppState = createSelector(
    selectState,
    (app: AppState): AppState => app
);

export const selectCategories = createSelector(
    selectState,
    (app: AppState): Category[] => app.categories
);

export const selectAddress = createSelector(
    selectState,
    (app: AppState): string => app.currentAddress
);

