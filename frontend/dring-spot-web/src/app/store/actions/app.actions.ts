import { createAction, props } from '@ngrx/store';
import { MeetingPlace } from 'src/app/models/meeting-place.model';
import { UserModel } from 'src/app/models/user.model';
import { Category } from 'src/app/models/category.model';
import { ReverseGeocodingResult } from 'src/app/models/reverse-geocoding.model';
import { SerachPlaceModel } from 'src/app/models/search-place.model';

export const userLoggedIn = createAction('[User] Logged in', props<{ user: UserModel}>());
export const loadPlaces = createAction('[Place] Load places');
export const placesLoaded = createAction('[Place] Places loaded', props<{ places: MeetingPlace[]}>());
export const showPlaceDetails = createAction('[Place] Show details', props<{ id: number}>());
export const hidePlaceDetails = createAction('[Place] Hide details');

export const loadPlace = createAction('[Place] Load place', props<{ id: number}>());
export const placeLoaded = createAction('[Place] Place loaded', props<{ place: MeetingPlace}>());

export const loadCategories = createAction('[Place] Load categories');
export const categoriesLoaded = createAction('[Place] Categories loaded', props<{ categories: Category[]}>());

export const searchForPlaces = createAction('[Place] Search', props<{ criteria: SerachPlaceModel }>());
export const placesSearched = createAction('[Place] Search completed', props<{ places: MeetingPlace[] }>());

export const loadAddress = createAction('[Location] Load address', props<{ lat: string, lng: string}>());
export const addressLoaded = createAction('[Location] Address loaded',  props<{ result: ReverseGeocodingResult}>());