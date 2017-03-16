import React, { Component } from 'react'
import { connect } from 'react-redux'
import { ActionCreators } from '../actions'
import { View, Text, TouchableHighlight } from 'react-native'
import { bindActionCreators } from 'redux'
import { HomeLibraryMobile } from './HomeLibraryMobile'

class AppContainer extends Component {
    _selectBook () {
        this.props.selectBook();
    }

    render() {
        return (
            <View>
                <Text>
                    Zaznaczona książka: { this.props.selectedBook } 
                </Text>
                <TouchableHighlight onPress={() => this._selectBook()}>
                    <Text>Select!</Text>
                </TouchableHighlight>
            </View> );
    }
}
 
function mapDispatchToProps(dispatch) {
    return bindActionCreators(ActionCreators, dispatch);
}

export default connect((state) => { 
    return {
        selectedBook: state.selectedBook
    }
 }, mapDispatchToProps)(AppContainer);