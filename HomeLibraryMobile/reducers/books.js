import createReducer from '../lib/createReducer'
import * as types from '../actions/types'

export const selectedBook = createReducer({ author: '', title: ''}, {
    [types.SELECT_BOOK](state, action){
        return action.selectedBook;
    }
});

export const signalRState = createReducer('disconnected',{
    [types.SIGNALR_STATE_CHANGED](state, action){
        if (action.signalRState === undefined) {
            return 'disconnected';
        }
        return action.signalRState;
    }
})

export const libraryState = createReducer({Books: []}, {
    [types.UPDATE_LIBRARY_STATE](state, action){
        return action.libraryState;
    }
})

export const books = createReducer([], {
    [types.UPDATE_LIBRARY_STATE](state, action){
        return action.libraryState.Books;
    }
})

export const newBook = createReducer([], {
    [types.NEW_BOOK_RECEIVED](state, action){
        return action.newBooks[0];
    }
})

export const canProcessBarcode = createReducer(true, {
    [types.BARCODE_PROCESSING](state, action) {
        return action.canProcessBarcode;
    }
})