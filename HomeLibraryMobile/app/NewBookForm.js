import React, { Component } from 'react';
import {
  Text,
  View,
} from 'react-native';
import { Toolbar } from 'react-native-material-ui';
import { connect } from 'react-redux'

class NewBookForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
            title: props.newBook.title,
            author: props.newBook.author,
            isbn: props.newBook.isbn
        }
    }

    render () {
        return (<View>
                    <Toolbar
                        leftElement="arrow-back"
                        centerElement={ this.props.route.title }
                        onLeftElementPress={ () => { this.props.navigator.pop() } }
                    />
                    <Text>Tytuł: { this.state.title }</Text>
                    <Text>Autor: { this.state.author }</Text>
                    <Text>ISBN: { this.state.author }</Text>
                </View>)
    }
}

function mapStateToProps(state) {
    return {
        newBook: state.newBook
    }
}

export default connect(mapStateToProps)(NewBookForm);