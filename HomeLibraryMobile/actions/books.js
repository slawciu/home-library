import * as types from './types'
import signalrClient from '../lib/signalrClient.js';

let proxy = undefined;

export function selectBook(bookId) {
    return(dispatch, getState) => {
         var books = getState().books;
         var selectedBook = books.find(function(book){ return book.Id === bookId});
         dispatch(bookSelected(selectedBook))
    }
}

export function bookSelected(book) {
    return {
        type: types.SELECT_BOOK,
        selectedBook: book
    }
}

export function updateLibraryState({ libraryState }) {
    return {
        type: types.UPDATE_LIBRARY_STATE,
        libraryState : libraryState
    }
}

export function newBookInfoReceived({ newBooks }) {
    return {
        type: types.NEW_BOOK_RECEIVED,
        newBooks: newBooks
    }
}

export function getLibraryState(deviceName) {
    return(dispatch, getState) => {
        if (proxy === undefined) {
            proxy = signalrClient.getSignalRProxy()
        }
        proxy.invoke('getLibraryState', 'Mobile')
        .fail(() => {
          console.log('getLibraryState fail');
        });
    }
}

export function blockBarcodeProcessing() {
    return {
        type: types.BARCODE_PROCESSING,
        canProcessBarcode: false
    }
}

export function unblockBarcodeProcessing() {
    return {
        type: types.BARCODE_PROCESSING,
        canProcessBarcode: true
    }
}

export function codeHasBeenScanned(isbn) {
    return(dispatch, getState) => {
        if (proxy === undefined) {
            proxy = signalrClient.getSignalRProxy()
        }

        proxy.invoke('isbnScanned', isbn)
        .fail(() => {
            console.log('isbnScanned fail');
        });
    }
}

export function addNewBook(book) {
    return(dispatch, getState) => {
        if (proxy === undefined) {
            proxy = signalrClient.getSignalRProxy()
        }

        proxy.invoke('addNewBook', { Author: book.author, ISBN: book.isbn, Title: book.title, Localisation: book.localisation })
        .fail(() => {
            console.log('isbnScanned fail');
        });
    }
}

export function connectToSignalR() {
    return(dispatch, getState) => {
        const connection = signalrClient.getSignalRConnection();
        connection.logging = true;

        proxy = signalrClient.getSignalRProxy();
        proxy.on('updateLibraryState', (libraryState) => {
            dispatch(updateLibraryState({ libraryState: libraryState }))
        });

        proxy.on('newBookInfo', (newBooks) => {
            dispatch(unblockBarcodeProcessing());
            dispatch(newBookInfoReceived({ newBooks: newBooks }))
        });

        proxy.on('newBookAddedSuccessfully', () => {
            dispatch(getLibraryState('Mobile'));
        });

        proxy.on('failureWhileAddingNewBook', () => {

        });

        connection.start()
            .done(() => {
                console.log('signalr connected');
                dispatch(signalRStateChanged({ signalRState: 'connected' }));
                proxy.invoke('getLibraryState', 'Mobile')
                .fail(() => {
                    console.log('getLibraryState fail');
                });
            })
            .fail(() => {
                console.log('signalr connection failed');
            });

        connection.error((error) => {
            const errorMessage = error.message;
            let detailedError = '';
            if (error.source && error.source._response) {
                detailedError = error.source._response;
            }
            dispatch(signalRStateChanged({ signalRState: 'disconnected' }))
            console.log('signalr connection failed'+ errorMessage);
        });
    }
}

export function signalRStateChanged({ signalRState }) {
    return {
        type: types.SIGNALR_STATE_CHANGED,
        signalRState : signalRState
    }
}