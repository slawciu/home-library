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

    _onSaveButtonPress () {
        var title, author, isbn;
        if (this.state.title === '') {
            title = this.props.newBook.Title;
        } else {
            title = this.state.title;
        }

        if (this.state.author === '') {
            author = this.props.newBook.Author;
        } else {
            author = this.state.author;
        }

        if (this.state.isbn === '') {
            isbn = this.props.newBook.ISBN;
        } else {
            isbn = this.state.isbn;
        }

        this.props.addNewBook({title: title, author: author, isbn: isbn})
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