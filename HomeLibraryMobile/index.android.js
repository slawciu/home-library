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
  View
} from 'react-native';

export default class HomeLibraryMobile extends Component {
  constructor(props) {
    super(props);
    this.state = {
      signalRconnected: false
    }
  }
  
  _renderHomeView() {
    if (this.state.signalRconnected) {
      return (<Text>Połączono z serwerem :)</Text>)
    } else {
      return (<Text>Brak połączenia z serwerem :(</Text>)
    }
  }
  
  render() {
    return (
      <View style={styles.container}>
        <Text>Domowa Biblioteka</Text>
        { this._renderHomeView() }
      </View>
    );
  }
}

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
