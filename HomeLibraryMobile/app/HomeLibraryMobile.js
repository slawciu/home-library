import React, { Component } from 'react';
import {
  AppRegistry,
  Text,
  View,
  ToastAndroid,
  Navigator
} from 'react-native';
import signalr from 'react-native-signalr';
import { COLOR, ThemeProvider, Toolbar } from 'react-native-material-ui';
import { connect } from 'react-redux'
import BooksList from './BooksList'
import BookDetails from './BookDetails'

class HomeLibraryMobile extends Component {
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
    const routesArray = [
      { index: 0, title: 'Domowa Biblioteka', page: BooksList },
      { index: 1, title: 'Książka', page: BookDetails },    
    ];

    return (
        <Navigator
          configureScene={ this._configureScene }
          initialRoute={ routesArray[0] }
          initialRouteStack={ routesArray }
          renderScene={ this._renderScene }  
        />
    );
  }
}

function mapStateToProps(state) {
    return {
        selectedBook: state.selectedBook
    }
}

export default connect(mapStateToProps)(HomeLibraryMobile);