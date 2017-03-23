import * as reducer from '../../reducers/books'
import * as types from '../../actions/types'

describe('books reducer', () => {
    it('should return disconnected status as default', () => {
        const action = {
            type: 'SIGNALR_STATE_CHANGED'
        }

        expect(reducer.signalRState({}, action)).toEqual('disconnected');
    });

    it('should return status given in action object', () => {
        const action = {
            type: 'SIGNALR_STATE_CHANGED',
            signalRState: 'connected'
        }

        expect(reducer.signalRState({}, action)).toEqual('connected');
    });

    it('should return selected book', () => {
        const action = {
            type: types.SELECT_BOOK,
            selectedBook: { title: 'test book'}
        }
        
        expect(reducer.selectedBook({}, action))
            .toEqual({ title: 'test book'});
    });

    it('should return books as part of library state', () => {
        const expectedState = { books: [] }
        const action = {
            type: types.UPDATE_LIBRARY_STATE,
            libraryState: expectedState
        }

        expect(reducer.libraryState({}, action))
            .toMatchObject(expectedState)
    });

    it('should return books', () => {
        const expectedState = { books: [] }
        const action = {
            type: types.UPDATE_LIBRARY_STATE,
            libraryState: expectedState
        }

        expect(reducer.books({}, action))
            .toMatchObject(expectedState.books)
    });
});