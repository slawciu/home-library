import * as types from '../../actions/types'
import * as actions from '../../actions/books'

jest.mock('../../lib/signalrClient');

describe('books actions', () => {
    it('should select book', () => {
        const book = { id: 1 };
        const expectedAction = {
            type: types.SELECT_BOOK,
            selectedBook: book
        }

        expect(actions.bookSelected(book)).toEqual(expectedAction);
    })
})