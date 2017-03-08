class Dashboard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            libraryState: ":("
        };
        this.sHub = null;
    }
  
    componentDidMount() {
        $.connection.hub.url = "/signalr";

        this.sHub = $.connection.library;

        this.sHub.client.updateLibraryState = function (libraryState) {
            this.setState((prevState, props) => {
                return { libraryState: libraryState }
            });
            this.setState({ libraryState: libraryState });
        }.bind(this);

        $.connection.hub.start().done(function () {
            this.sHub.server.getLibraryState("Maurice");
        }.bind(this));

        $.connection.hub.disconnected(function () {
            setTimeout(function () {
                $.connection.hub.start().done(function() {
                    this.sHub.server.GetLibraryState("Maurice");
                }.bind(this));
            }.bind(this), 5000); // Restart connection after 5 seconds.
        });

        $.connection.hub.reconnected(function () {
            this.sHub.server.getLibraryState("Maurice");
        }.bind(this));
    }

    render() {
        return (<div>
                    <h1>Domowa Biblioteka</h1>
                    <div>Gdzieś poniżej wyświetlimy książki...</div>
                    <div>Stan biblioteki: { this.state.libraryState }</div>
                </div>);
    }
};

React.render(<Dashboard />, document.getElementById("container"));