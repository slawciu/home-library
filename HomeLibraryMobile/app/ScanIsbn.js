import React, { Component } from 'react';
import {
  Text,
  View,
  BackAndroid,
  ToastAndroid,
  StyleSheet
} from 'react-native';
import { connect } from 'react-redux'
import Camera from 'react-native-camera';
import NewBookForm from './NewBookForm';

class ScanIsbn extends Component {
    constructor(props) {
        super(props);
        BackAndroid.addEventListener('hardwareBackPress', () => {
                this.props.navigator.pop();
                return true; 
            } );
    }

    _onBarCodeRead (code) {
        if (!this.props.canProcessBarcode) {
            return;
        }
        this.props.blockBarcodeProcessing();
        ToastAndroid.show(code, ToastAndroid.SHORT);
        this.props.codeHasBeenScanned(code);
        this.props.navigator.replace({ index: 3, title: 'Nowa książka', page: NewBookForm })
    }

    render () {
        return (<View style={styles.container}>
                    <Camera
                        ref={(cam) => {
                            this.camera = cam;
                        }}
                        style={styles.preview}
                        aspect={Camera.constants.Aspect.fill}
                        onBarCodeRead={ (event) => this._onBarCodeRead(event.data) }>
                    </Camera>
                </View>)
    }
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: 'row',
  },
  preview: {
    flex: 1,
    justifyContent: 'flex-end',
    alignItems: 'center'
  },
  capture: {
    flex: 0,
    backgroundColor: '#fff',
    borderRadius: 5,
    color: '#000',
    padding: 10,
    margin: 40
  }
});

function mapStateToProps(state) {
    return {
        canProcessBarcode: state.canProcessBarcode
    }
}

export default connect(mapStateToProps)(ScanIsbn)