import React, { Component } from 'react';
import {
  Text,
  View,
} from 'react-native';
import { Toolbar } from 'react-native-material-ui';

export default class BookDetails extends Component {
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
                    <Text>Tytuł: </Text>
                    <Text>Autor: </Text>
                    <Text>ISBN: </Text>
                </View>)
    }
}