import 'react-native';
import React from 'react';
import BooksList from '../../app/BooksList.js';

// Note: test renderer must be required after react-native.
import renderer from 'react-test-renderer';
jest.mock('react-native-material-ui');

const route = { id:1, title: 'Test' }
    
    const navigator = {
        getCurrentRoutes: () => {
            return route
        }
    }


it('renders correctly when library is empty', () => {
    const store = {
        subscribe: () => {},
        dispatch: () => {},
        getState: () => { return {
            libraryState: {
                books: []
            }
        }}
    }
  renderer.create(
    <BooksList store={ store } navigator={navigator} route={ route }/>
  );
});

it('renders correctly with books in library', () => {
    
    const store = {
        subscribe: () => {},
        dispatch: () => {},
        getState: () => { return {
            libraryState: {
                books: [{
                    title: 'test',
                    author: 'test',
                    localisation: 'test'
                }]
            }
        }}
    }
    

  const tree = renderer.create(
    <BooksList store={ store } navigator={navigator} route={ route }/>
  );
});