import { UserModel } from 'src/app/models/user.model';
import { MeetingPlace } from 'src/app/models/meeting-place.model';
import { Category } from 'src/app/models/category.model';

export interface AppState {
    user: UserModel;
    places: MeetingPlace[],
    placeDetails: MeetingPlace,
    categories: Category[],
    searched: boolean;
    currentAddress?: string;
};

export const initialState: AppState = retrieveState() !== null ? retrieveState() : 
{
    user: null,
    places: null,
    placeDetails: null,
    categories: null,
    searched: false,
};

export const initalBaseState: BaseState = {
    appState: initialState,
}

function retrieveState(): AppState {
    return JSON.parse(localStorage.getItem('state'));
}

export interface BaseState {
    appState: AppState;
};