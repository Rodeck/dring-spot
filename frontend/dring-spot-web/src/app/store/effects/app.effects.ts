import { Injectable } from '@angular/core';
import { createEffect, Actions, ofType } from "@ngrx/effects";
import { loadPlaces, placesLoaded, userLoggedIn, loadPlace, placeLoaded, loadCategories, categoriesLoaded, loadAddress, addressLoaded, searchForPlaces, placesSearched, loadEvents, eventsLoaded } from '../actions/app.actions';
import { mergeMap, tap, map, catchError } from 'rxjs/operators';
import { MeetingPlaceService } from 'src/app/services/categories.service';
import { Store } from '@ngrx/store';
import { AppState, BaseState } from '../state/app.state';
import { EMPTY } from 'rxjs';
import { MapsService } from 'src/app/services/maps.service';

@Injectable()
export class AppEffects {

    loadActions$ = createEffect(
        () => this.actions$.pipe(
            ofType(loadPlaces),
            mergeMap(() => this.placeService.getPlaces().pipe(
                map(result => this.store.dispatch(placesLoaded({ places: result }))),
                catchError(() => EMPTY)
            )
            )),
        { dispatch: false }
    );

    loadPlace$ = createEffect(
        () => this.actions$.pipe(
            ofType(loadPlace),
            mergeMap((action) => this.placeService.getPlace(action.id).pipe(
                map(result => this.store.dispatch(placeLoaded({ place: result }))),
                catchError(() => EMPTY)
            )
            )),
        { dispatch: false }
    );

    loadCategories$ = createEffect(
        () => this.actions$.pipe(
            ofType(loadCategories),
            mergeMap((action) => this.placeService.getCategories().pipe(
                map(result => this.store.dispatch(categoriesLoaded({ categories: result }))),
                catchError(() => EMPTY)
            )
            )),
        { dispatch: false }
    );


    loadAddress$ = createEffect(
        () => this.actions$.pipe(
            ofType(loadAddress),
            mergeMap((action) => this.mapsService.getAddress(action.lat, action.lng).pipe(
                map(result => this.store.dispatch(addressLoaded({ result: result }))),
                catchError(() => EMPTY)
            )
            )),
        { dispatch: false }
    );

    searchForPlaces$ = createEffect(
        () => this.actions$.pipe(
            ofType(searchForPlaces),
            mergeMap((action) => this.placeService.searchPlaces(action.criteria).pipe(
                map(result => this.store.dispatch(placesSearched({ places: result }))),
                catchError(() => EMPTY)
            )
            )),
        { dispatch: false }
    );

    userLogged$ = createEffect(
        () => this.actions$.pipe(
            ofType(userLoggedIn),
            tap(() => {
                this.store.dispatch(loadPlaces());
                this.store.dispatch(loadCategories());
            })
        ),
        { dispatch: false }
    );
    
    constructor(
        private actions$: Actions,
        private placeService: MeetingPlaceService,
        private mapsService: MapsService,
        private store: Store<BaseState>
    ) { }
}