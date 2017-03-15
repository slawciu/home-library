/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 * @flow
 */

import React, { Component } from 'react';
import {
  AppRegistry,
  StyleSheet,
  Text,
  View,
  ToastAndroid,
  Navigator
} from 'react-native';
import signalr from 'react-native-signalr';
import { COLOR, ThemeProvider, Toolbar } from 'react-native-material-ui';

import BooksList from './app/BooksList'

export default class HomeLibraryMobile extends Component {
  constructor(props) {
    super(props);
    this.state = {
      signalRconnected: false,
      libraryState: ''
    }
  }

  componentDidMount() {
    const connection = signalr.hubConnection('http://192.168.0.19:57123');
    connection.logging = true;

    const proxy = connection.createHubProxy('library');
    proxy.on('updateLibraryState', (libraryState) => {
      this.setState({ libraryState: libraryState });
    });

    connection.start().done(() => {
      ToastAndroid.show('signalr connected', ToastAndroid.SHORT);
      this.setState({ signalRconnected: true })
      proxy.invoke('getLibraryState', 'Mobile')
        .fail(() => {
          ToastAndroid.show('getLibraryState fail', ToastAndroid.SHORT);
        });
    }).fail(() => {
      ToastAndroid.show('signalr connection failed', ToastAndroid.SHORT);
    });

    connection.error((error) => {
      const errorMessage = error.message;
      let detailedError = '';
      if (error.source && error.source._response) {
        detailedError = error.source._response;
      }
      ToastAndroid.show('signalr connection failed'+ errorMessage, ToastAndroid.SHORT);
    });
  }

  _renderHomeView() {
    if (this.state.signalRconnected) {
      return (
        <View>
          <Text>Połączono z serwerem :)</Text>
          <Text>Stan biblioteki: { this.state.libraryState }</Text>
        </View>)
    } else {
      return (<Text>Brak połączenia z serwerem :(</Text>)
    }
  }
  
  _configureScene(route) {
    return route.animationType || Navigator.SceneConfigs.FloatFromRight;
  }

  _renderScene(route, navigator) {
    return ( 
          <View>
            <route.page route={route} navigator={navigator}/>
          </View>
      );
  }

  render() {
    const routes = {
      home: {
        Title: 'Domowa Biblioteka',
        Page: BooksList
      }
    };

    const routesArray = [
      { index: 'home', title: 'Domowa Biblioteka', page: BooksList }
    ];
    return (
      <ThemeProvider uiTheme={uiTheme}>
        <Navigator
          configureScene={ this._configureScene }
          initialRoute={ routesArray['home'] }
          initialRouteStack={ routesArray }
          renderScene={ this._renderScene }  
        />
      </ThemeProvider>
    );
  }
}

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

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#F5FCFF',
  },
  welcome: {
    fontSize: 20,
    textAlign: 'center',
    margin: 10,
  },
  instructions: {
    textAlign: 'center',
    color: '#333333',
    marginBottom: 5,
  },
});

AppRegistry.registerComponent('HomeLibraryMobile', () => HomeLibraryMobile);
