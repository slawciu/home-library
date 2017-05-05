class BookList extends React.Component {
    constructor(props) {
        super(props);
    }

    render () {
        var books = this.props.books.map(function(book) {
            return <BookListItem book={book} />;
        });
        
        if (books.length > 0) {
            return (
                        <div>
                            { books }
                        </div>
                    );
        } else {
            return (<div>Brak książek do wyświetlenia.</div>)
        } 
    }
};