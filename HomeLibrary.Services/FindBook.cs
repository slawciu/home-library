using System.Collections.Generic;
using System.Linq;
using HomeLib.BooksInformationService;
using HomeLibrary.DataLayer;

namespace HomeLibrary.Services
{
    public class FindBook : IQueryHandler<FindBookQuery, IList<Book>>
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IBooksInformationService _booksInformationService;

        public FindBook(ILibraryRepository libraryRepository, IBooksInformationService booksInformationService)
        {
            _libraryRepository = libraryRepository;
            _booksInformationService = booksInformationService;
        }

        public IList<Book> Handle(FindBookQuery query)
        {
            var foundBooks = new List<Book>();

            var bookFromRepository = _libraryRepository.FindBookWithGivenIsbn(query.ISBN);

            if (bookFromRepository != null)
            {
                foundBooks.Add(bookFromRepository);
            }

            var bookInformation = _booksInformationService.GetByIsbn(query.ISBN);

            if (bookInformation != null)
            {
                foundBooks.Add(MapToBook(bookInformation));
            }

            if (!foundBooks.Any())
            {
                foundBooks.Add(new Book
                {
                    Author = "",
                    ISBN = query.ISBN,
                    Localisation = "",
                    Title = ""
                });
            }

            return foundBooks;
        }

        private Book MapToBook(BookInformation bookInformation)
        {
            return new Book
            {
                Author = bookInformation.Author,
                ISBN = bookInformation.ISBN,
                Title = bookInformation.Title
            };
        }
    }
}