using System.Collections.Generic;
using System.Linq;

namespace HomeLibrary.DataLayer
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly IContext _context;

        public LibraryRepository(IContext context)
        {
            _context = context;
        }

        public IList<Book> GetAllBooks()
        {
            return _context.Books.AsQueryable().ToList();
        }

        public Book FindBookWithGivenIsbn(string isbn)
        {
            return _context.Books.FirstOrDefault(x => x.ISBN == isbn);
        }

        public int AddNewBook(Book book)
        {
            _context.Books.Add(book);
            return _context.SaveChanges();
        }
    }
}