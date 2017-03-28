import BooksList from './BooksList'
import BookDetails from './BookDetails'
import ScanIsbn from './ScanIsbn'

export const routesArray = [
      { index: 0, title: 'Domowa Biblioteka', page: BooksList },
      { index: 1, title: 'Książka', page: BookDetails }, 
      { index: 2, title: 'Skan ISBN', page: ScanIsbn }   
    ];