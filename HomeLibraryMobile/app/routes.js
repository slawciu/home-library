import BooksList from './BooksList'
import BookDetails from './BookDetails'
import ScanIsbn from './ScanIsbn'
import NewBookForm from './NewBookForm'

export const routesArray = [
      { index: 0, title: 'Domowa Biblioteka', page: BooksList },
      { index: 1, title: 'Książka', page: BookDetails }, 
      { index: 2, title: 'Skan ISBN', page: ScanIsbn },
      { index: 3, title: 'Nowa książka', page: NewBookForm}   
    ];