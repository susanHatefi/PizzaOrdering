import { createAction, createReducer, on } from '@ngrx/store';
import { AppState } from './state';
import * as Actions from './Actions';

const initialAppState: AppState = {
  error: '',
  toggleSpinner: false,
};

export const AppReducer = createReducer(
  initialAppState,
  on(Actions.failedRequestAction, (state, { error }): AppState => {
    return {
      ...state,
      error,
    };
  }),
  on(Actions.toggleSpinnerAction, (state): AppState => {
    return {
      ...state,
      toggleSpinner: !state.toggleSpinner,
    };
  })
);
