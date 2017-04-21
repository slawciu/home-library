import React, { Component } from 'react';
import {
  Text,
  TextInput,
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
                    title: '',
                    author: '',
                    isbn: ''
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
                    <Text>Tytuł:</Text>
                    <TextInput onChangeText={ (text) => this.setState({ title: text })} value={ this.state.title || this.props.newBook.Title } />
                    <Text>Autor:</Text>
                    <TextInput onChangeText={ (text) => this.setState({ author: text })} value={ this.state.author || this.props.newBook.Author } />
                    <Text>ISBN:</Text>
                    <TextInput onChangeText={ (text) => this.setState({ isbn: text })} value={ this.state.isbn || this.props.newBook.ISBN } />
                </View>)
    }
}

function mapStateToProps(state) {
    return {
        newBook: state.newBook
    }
}

export default connect(mapStateToProps)(NewBookForm);