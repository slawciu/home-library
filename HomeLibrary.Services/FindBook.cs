using System.Collections.Generic;

namespace HomeLibrary.Services
{
    public class FindBook : IQueryHandler<FindBookQuery, IList<BookInfo>>
    {
        private readonly ILibraryRepository _libraryRepository;

        public FindBook(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public IList<BookInfo> Handle(FindBookQuery query)
        {
            var bookFromRepository = _libraryRepository.FindBookWithGivenIsbn(query.ISBN);

            if (bookFromRepository == null)
            {
                return new List<BookInfo>
                {
                    new BookInfo
                    {
                        Author = "",
                        ISBN = query.ISBN,
                        Localisation = "",
                        Title = ""
                    }
                };
            }

            return new List<BookInfo>
            {
                bookFromRepository
            };
        }
    }
}