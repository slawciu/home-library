class BookList extends React.Component {
    constructor(props) {
        super(props);
    }

    render () {
        var books = this.props.books.map(function(book) {
            return <BookListItem book={book} />;
        });
        return (
            <div>
                { books }
            </div>
        );
    }
};