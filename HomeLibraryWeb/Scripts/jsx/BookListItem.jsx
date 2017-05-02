class BookListItem extends React.Component {
    constructor(props) {
        super(props);
    }

    render () {
        return (
           <div className='bg-primary book-body'>
                <h3>
                    { this.props.book.Title }
                </h3>
                <div className='book-entry-author'>
                    { this.props.book.Author }
                </div>
                <div className='book-date'>
                    { this.props.book.Localisation }
                </div>
                <div className='book-quantity'>
                    { this.props.book.Quantity}
                </div>
            </div>
        );
    }
};