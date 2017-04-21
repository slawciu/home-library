import React, { Component } from 'react';
import {
  Text,
  View,
} from 'react-native';
import { Toolbar } from 'react-native-material-ui';
import { connect } from 'react-redux'

class BookDetails extends Component {
    constructor(props) {
        super(props);
    }

    render () {
        return (<View>
                    <Toolbar
                        leftElement="arrow-back"
                        centerElement={ this.props.route.title }
                        onLeftElementPress={ () => { this.props.navigator.pop() } }
                    />
                    <Text>Tytuł: { this.props.book.Title }</Text>
                    <Text>Autor: { this.props.book.Author }</Text>
                    <Text>ISBN: { this.props.book.ISBN }</Text>
                </View>)
    }
}

function mapStateToProps(state) {
    return {
        book: state.selectedBook
    }
}

export default connect(mapStateToProps)(BookDetails);