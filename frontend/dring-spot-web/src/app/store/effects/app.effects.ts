import { Injectable } from '@angular/core';
import { createEffect, Actions, ofType } from "@ngrx/effects";
import { loadPlaces, placesLoaded, userLoggedIn, loadPlace, placeLoaded, loadCategories, categoriesLoaded } from '../actions/app.actions';
import { mergeMap, tap, map, catchError } from 'rxjs/operators';
import { MeetingPlaceService } from 'src/app/services/categories.service';
import { Store } from '@ngrx/store';
import { AppState, BaseState } from '../state/app.state';
import { EMPTY } from 'rxjs';

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
        private store: Store<BaseState>
    ) { }
}