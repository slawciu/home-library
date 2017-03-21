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
import thunkMiddleware from 'redux-thunk'
import createLogger from 'redux-logger'
import AppContainer from './app/AppContainer'

const loggerMiddleware = createLogger({ predicate: (getState, action) => __DEV__  });

function configureStore(initialState) {
  const enhancer = compose(
    applyMiddleware(
      thunkMiddleware,
      loggerMiddleware)
  )
  return createStore(reducer, initialState, enhancer);
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
