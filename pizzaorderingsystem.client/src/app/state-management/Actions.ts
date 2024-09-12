import { createAction, props } from '@ngrx/store';

export const toggleSpinnerAction = createAction('[Page] App Toggling Spinner');
export const failedRequestAction = createAction(
  '[API] App Failed Request',
  props<{ error: string }>()
);
