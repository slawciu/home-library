import * as types from '../../actions/types'
import * as actions from '../../actions/books'

jest.mock('signalr',() => {
    return {};
})

describe('books actions', () => {
    it('should select book', () => {
        const bookId = 1;
        const expectedAction = {
            type: types.SELECT_BOOK,
            selectedBookId: 1
        }
    })

    expect(actions.selectBook(bookId)).toEqual(expectedAction);
})