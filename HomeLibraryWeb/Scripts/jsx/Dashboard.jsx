class Dashboard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            libraryState: { Books: [] }
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

    render() {
        return (<div>
                    <h1>Domowa Biblioteka</h1>
                    <BookList books={this.state.libraryState.Books}/>
                </div>);
    }
};

React.render(<Dashboard />, document.getElementById("container"));