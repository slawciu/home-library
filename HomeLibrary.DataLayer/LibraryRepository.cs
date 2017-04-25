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

            return new List<Book>
            {
                new Book {Id = 0, Title = "Gra Endera", Author = "Orson Scott Card", Localisation = "Gliwice", ISBN = "9788376482514"},
                new Book {Id = 1, Title = "Cień Endera", Author = "Orson Scott Card", Localisation = "Gliwice", ISBN = "9788378397649"}
            };
        }

        public Book FindBookWithGivenIsbn(string isbn)
        {
            return _context.Books.FirstOrDefault(x => x.ISBN == isbn);
            return new Book {ISBN = isbn};
        }
    }
}