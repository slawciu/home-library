import createReducer from '../lib/createReducer'
import * as types from '../actions/types'

export const retrievedBooks = createReducer(
    {}, {

    }
);

export const selectedBook = createReducer(-1, {
    [types.SELECT_BOOK](state, action){
        return state + 1;
    }
});