using System.Collections.Generic;
using HomeLibrary.DataLayer;

namespace HomeLibrary.Services
{
    public class FindBook : IQueryHandler<FindBookQuery, IList<Book>>
    {
        private readonly ILibraryRepository _libraryRepository;

        public FindBook(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public IList<Book> Handle(FindBookQuery query)
        {
            var bookFromRepository = _libraryRepository.FindBookWithGivenIsbn(query.ISBN);

            if (bookFromRepository == null)
            {
                return new List<Book>
                {
                    new Book
                    {
                        Author = "",
                        ISBN = query.ISBN,
                        Localisation = "",
                        Title = ""
                    }
                };
            }

            return new List<Book>
            {
                bookFromRepository
            };
        }
    }
}