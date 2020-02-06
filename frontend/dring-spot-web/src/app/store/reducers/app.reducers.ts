import { createReducer, on, Action, ActionReducerMap } from "@ngrx/store";
import { initialState, AppState, BaseState } from '../state/app.state';
import { placesLoaded, userLoggedIn, showPlaceDetails, hidePlaceDetails, placeLoaded, categoriesLoaded, searchForPlaces, addressLoaded, placesSearched, eventsLoaded } from '../actions/app.actions';

const _appReducer = createReducer(initialState,
    on(placesLoaded, (state: AppState, { places }) => 
    ({
        ...state,
        places: places
    })),
    on(placesSearched, (state: AppState, { places }) => 
    ({
        ...state,
        places: places
    })),
    on(categoriesLoaded, (state: AppState, { categories }) => 
    ({
        ...state,
        categories: categories
    })),
    on(userLoggedIn, (state: AppState, { user }) => 
    ({
        ...state,
        user: user
    })),
    on(showPlaceDetails, (state: AppState, { id }) => 
    ({
        ...state,
        placeDetails: state.places.find(x => x.id === id),
    })),
    on(hidePlaceDetails, (state: AppState, { }) => 
    ({
        ...state,
        placeDetails: null,
    })),
    on(addressLoaded, (state: AppState, { result }) => 
    ({
        ...state,
        currentAddress: result.results[0].formatted_address,
    })),
    on(placeLoaded, (state: AppState, { place }) => 
    ({
        ...state,
        placeDetails: place,
        places: state.places.map(x => {
            if (x.id == place.id) return place;
            return x;
        }),
    })),
    on(eventsLoaded, (state: AppState, { events }) => 
    ({
        ...state,
        events: events,
    })),
);

export function reducer(state: AppState | undefined, action: Action) {
    return _appReducer(state, action);
}

export const appReducers: ActionReducerMap<BaseState, any> = {
    appState: reducer,
}