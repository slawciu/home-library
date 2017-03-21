import * as types from './types'
import signalr from 'react-native-signalr';

let proxy = null;

export function selectBook(bookId) {
    return(dispatch, getState) => {
         dispatch(bookSelected(getState().books[bookId]))
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

export function getLibraryState(deviceName) {
    return(dispatch, getState) => {
        proxy.invoke('getLibraryState', 'Mobile')
        .fail(() => {
          console.log('getLibraryState fail');
        });
    }
}

export function connectToSignalR() {
    return(dispatch, getState) => {
        const connection = signalr.hubConnection('http://192.168.0.19:57123');
        connection.logging = true;

        proxy = connection.createHubProxy('library');
        proxy.on('updateLibraryState', (libraryState) => {
            dispatch(updateLibraryState({ libraryState: libraryState }))
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