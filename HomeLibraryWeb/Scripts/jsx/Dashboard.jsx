class Dashboard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            libraryState: { Books: [] },
            filterText: ''
        };
        this.libraryHub = null;
    }
  
    componentDidMount() {
        $.connection.hub.url = "/signalr";

        this.libraryHub = $.connection.library;

        this.libraryHub.client.newBookAddedSuccessfully = function() {
            this._refreshLibraryState();
        }.bind(this);

        this.libraryHub.client.updateLibraryState = function (libraryState) {
            this.setState((prevState, props) => {
                return { libraryState: libraryState }
            });
            this.setState({ libraryState: libraryState });
        }.bind(this);

        $.connection.hub.start().done(function () {
            this._refreshLibraryState();
        }.bind(this));

        $.connection.hub.disconnected(function () {
            setTimeout(function () {
                $.connection.hub.start().done(function() {
                    this._refreshLibraryState();
                }.bind(this));
            }.bind(this), 5000); // Restart connection after 5 seconds.
        });

        $.connection.hub.reconnected(function () {
            this._refreshLibraryState();
        }.bind(this));
    }

    _refreshLibraryState() {
        this.libraryHub.server.getLibraryState("Maurice");
    }

    _handleFilterChange(event) {
        this.setState({ filterText: event.target.value })
    }

    render() {
        var filteredBooks = this.state.libraryState.Books.filter(function(book){
            return this.state.filterText.length == 0 || book.Title.toLowerCase().indexOf(this.state.filterText.toLowerCase()) > -1 || book.Author.toLowerCase().indexOf(this.state.filterText.toLowerCase()) > -1
        }.bind(this));

        return (<div>
                    <h1>Domowa Biblioteka</h1>
                    <h3>Szukaj: 
                    <input type="text" value={this.state.filterText} onChange={ this._handleFilterChange.bind(this) } />
                    </h3>
                    <BookList books={filteredBooks}/>
                </div>);
    }
};

React.render(<Dashboard />, document.getElementById("container"));