import React, { Component } from 'react';
import {
  Text,
  TextInput,
  View,
  BackAndroid
} from 'react-native';
import { Toolbar, ActionButton } from 'react-native-material-ui';
import { connect } from 'react-redux'
import BooksList from './BooksList'
import ScanIsbn from './ScanIsbn'

class NewBookForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
                    title: '',
                    author: '',
                    isbn: '',
                    localisation: ''
                }

        BackAndroid.addEventListener('hardwareBackPress', () => {
                this.props.navigator.replace({ index: 2, title: 'Skan ISBN', page: ScanIsbn });
                return true; 
            } );
    }

    _getLatestValue(valueFromState, valueFromProps) {
        if (valueFromState === '') {
            return valueFromProps;
        } else {
            return valueFromState;
        }
    }

    _onSaveButtonPress () {
        var title = this._getLatestValue(this.state.title, this.props.newBook.Title);
        var author = this._getLatestValue(this.state.author, this.props.newBook.Author);
        var isbn = this._getLatestValue(this.state.isbn, this.props.newBook.ISBN);
        var localisation = this._getLatestValue(this.state.localisation, this.props.newBook.Localisation);

        this.props.addNewBook({title: title, author: author, isbn: isbn, localisation: localisation})
        this.props.navigator.push({ index: 0, title: 'Domowa Biblioteka', page: BooksList });
    }

    render () {
        return (<View style={{ flex: 1 }}>
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
                    <Text>Lokalizacja:</Text>
                    <TextInput onChangeText={ (text) => this.setState({ localisation: text })} value={ this.state.localisation || this.props.newBook.Localisation } />
                    <ActionButton icon='save' onPress={ () => this._onSaveButtonPress() }/>  
                </View>)
    }
}

function mapStateToProps(state) {
    return {
        newBook: state.newBook
    }
}

export default connect(mapStateToProps)(NewBookForm);