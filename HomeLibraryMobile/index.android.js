/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 * @flow
 */

import React, { Component } from 'react';
import {
  AppRegistry,
} from 'react-native';
import { COLOR, ThemeProvider } from 'react-native-material-ui';
import { Provider } from 'react-redux'
import { createStore, applyMiddleware, combineReduxers, compose } from 'redux'
import reducer from './reducers'

import AppContainer from './app/AppContainer'

function configureStore(initialState) {
  return createStore(reducer, initialState);
}

const store = configureStore({});

const App = () => (
  <ThemeProvider uiTheme={uiTheme}>
  <Provider store={store}>
    <AppContainer />
  </Provider>
  </ThemeProvider>
);

const uiTheme = {
    palette: {
        primaryColor: COLOR.green500,
    },
    toolbar: {
        container: {
            height: 50,
        },
    },
};

AppRegistry.registerComponent('HomeLibraryMobile', () => App);
