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
import ScanIsbn from './ScanIsbn.js'
class BooksList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            searchText: ''
        }
    }

    _renderListItem (data) {
        var routes = this.props.navigator.getCurrentRoutes();
        return (
            <ListItem key={ data.Id }  
                numberOfLines={2} 
                leftElement={ <Text>{ data.Localisation[0] }</Text>}
                centerElement={ data.Title } 
                onPress={ () => {
                    this.props.selectBook(data.Id);
                    this.props.navigator.push(routesArray[1]);}
                }/>
        )
    }

    render () {
        const ds = new ListView.DataSource({rowHasChanged: (r1, r2) => r1 !== r2});
        var filteredBooks = this.props.books.filter(function(book,i) { 
                    return this.state.searchText.length == 0 || book.Title.toLowerCase().indexOf(this.state.searchText.toLowerCase()) > -1 
                }.bind(this));
        var booksLists = (<ListView
                            dataSource={ ds.cloneWithRows(filteredBooks) }
                            renderRow={ this._renderListItem.bind(this) }
                        />);
        if (filteredBooks.length === 0) {
            booksLists = (<Text>Brak książek do wyświetlenia</Text>)
        }
        return (<View style={{ flex: 1}}>
                    <Toolbar
                        leftElement="menu"
                        centerElement={ this.props.route.title }
                        searchable={{
                            autoFocus: true,
                            placeholder: 'Szukaj',
                            onChangeText: value => this.setState({ searchText: value }),
                            onSearchClosed: () => this.setState({ searchText: ''})
                        }}
                    />
                    <View>
                        { booksLists }
                    </View>
                    <ActionButton onPress={ () => { this.props.navigator.push({ index: 2, title: 'Skan ISBN', page: ScanIsbn } ); } }/>  
                </View>)
    }
}

function mapStateToProps(state) {
    return {
        books: state.libraryState.Books,
    }
}

export default connect(mapStateToProps)(BooksList);