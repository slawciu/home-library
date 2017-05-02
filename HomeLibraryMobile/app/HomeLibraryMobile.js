import React, { Component } from 'react';
import {
  AppRegistry,
  Text,
  View,
  ToastAndroid,
  Navigator,
  BackAndroid
} from 'react-native';
import signalr from 'react-native-signalr';
import { COLOR, ThemeProvider, Toolbar, ActionButton } from 'react-native-material-ui';
import { connect } from 'react-redux'
import BooksList from './BooksList'
import BookDetails from './BookDetails'
import ScanIsbn from './ScanIsbn'
const routesArray = [
      { index: 0, title: 'Domowa Biblioteka', page: BooksList },
      { index: 1, title: 'Książka', page: BookDetails }, 
      { index: 2, title: 'Skan ISBN', page: ScanIsbn }   
    ];
class HomeLibraryMobile extends Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
     this.props.connectToSignalR();
  }

  _configureScene(route) {
    return route.animationType || Navigator.SceneConfigs.FadeAndroid;
  }

  _renderScene(route, navigator) {
  
    return ( 
          <View style={{ flex: 1 }}>
            <route.page {...this.props} route={route} navigator={navigator} />
            
          </View>
      );
  }

  render() {
    

    // ToastAndroid.show(this.props.signalRState, ToastAndroid.SHORT);
      
    return (
        <Navigator
          configureScene={ this._configureScene }
          initialRoute={ routesArray[0] }
          renderScene={ this._renderScene.bind(this) }  
        />
    );
  }
}

function mapStateToProps(state) {
    return {
        selectedBook: state.selectedBook,
        signalRState: state.signalRState,
        libraryState: state.libraryState
    }
}

export default connect(mapStateToProps)(HomeLibraryMobile);