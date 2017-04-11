import React, { Component } from 'react';
import {
  Text,
  View,
  BackAndroid
} from 'react-native';
import { Toolbar } from 'react-native-material-ui';
import { connect } from 'react-redux'
import ScanIsbn from './ScanIsbn'

class NewBookForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
            title: props.newBook.title,
            author: props.newBook.author,
            isbn: props.newBook.isbn
        }

        BackAndroid.addEventListener('hardwareBackPress', () => {
                this.props.navigator.replace({ index: 2, title: 'Skan ISBN', page: ScanIsbn });
                return true; 
            } );
    }

    render () {
        return (<View>
                    <Toolbar
                        leftElement="arrow-back"
                        centerElement={ this.props.route.title }
                        onLeftElementPress={ () => { this.props.navigator.replace({ index: 2, title: 'Skan ISBN', page: ScanIsbn }) } }
                    />
                    <Text>Tytuł: { this.state.title }</Text>
                    <Text>Autor: { this.state.author }</Text>
                    <Text>ISBN: { this.state.isbn }</Text>
                </View>)
    }
}

function mapStateToProps(state) {
    return {
        newBook: state.newBook
    }
}

export default connect(mapStateToProps)(NewBookForm);