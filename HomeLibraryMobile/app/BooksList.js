import React, { Component } from 'react';
import {
  Text,
  View,
  ListView
} from 'react-native';
import { Toolbar, ListItem } from 'react-native-material-ui';

export default class BooksList extends Component {
    constructor(props) {
        super(props);
        const ds = new ListView.DataSource({rowHasChanged: (r1, r2) => r1 !== r2});
        this.state = {
        dataSource: ds.cloneWithRows([
                { id: 0, title: 'Gra Endera', localisation: 'Gliwice'},
                { id: 1, title: 'Cień Endera', localisation: 'Gliwice'}
            ])
        };
    }

    _renderListItem (data) {
        var routes = this.props.navigator.getCurrentRoutes();
        return (
            <ListItem key={ data.id }  
                numberOfLines={2} 
                leftElement={ <Text>{ data.localisation[0] }</Text>}
                centerElement={ data.title } 
                onPress={ () => { this.props.navigator.push(routes[1])} }/>
        )
    }

    render () {
        return (<View>
                    <Toolbar
                        leftElement="menu"
                        centerElement={ this.props.route.title }
                        searchable={{
                            autoFocus: true,
                            placeholder: 'Szukaj',
                        }}
                    />
                    <View>
                        <ListView
                            dataSource={ this.state.dataSource }
                            renderRow={ this._renderListItem.bind(this) }
                        />
                    </View>
                </View>)
    }
}