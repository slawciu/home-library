import React, { Component } from 'react';
import {
  Text,
  View,
  ListView
} from 'react-native';
import { 
    Toolbar,
    ListItem,
    ActionButton } from 'react-native-material-ui';
import { connect } from 'react-redux'
import { routesArray } from './routes.js'
class BooksList extends Component {
    constructor(props) {
        super(props);
    }

    _renderListItem (data) {
        var routes = this.props.navigator.getCurrentRoutes();
        return (
            <ListItem key={ data.id }  
                numberOfLines={2} 
                leftElement={ <Text>{ data.localisation[0] }</Text>}
                centerElement={ data.title } 
                onPress={ () => {
                    this.props.selectBook(data.id);
                    this.props.navigator.push(routesArray[1]);}
                }/>
        )
    }

    render () {
        const ds = new ListView.DataSource({rowHasChanged: (r1, r2) => r1 !== r2});
        var booksLists = (<ListView
                            dataSource={ ds.cloneWithRows(this.props.books) }
                            renderRow={ this._renderListItem.bind(this) }
                        />);
        if (this.props.books.length === 0) {
            booksLists = (<Text>Brak książek do wyświetlenia</Text>)
        }
        return (<View >
                    <Toolbar
                        leftElement="menu"
                        centerElement={ this.props.route.title }
                        searchable={{
                            autoFocus: true,
                            placeholder: 'Szukaj',
                        }}
                    />
                    <View>
                        { booksLists }
                    </View>
                     
                </View>)
    }
}

function mapStateToProps(state) {
    return {
        books: state.libraryState.books,
    }
}

export default connect(mapStateToProps)(BooksList);