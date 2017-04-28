using HomeLibrary.DataLayer;

namespace HomeLibrary.Services
{
    public class AddNewBook : IQueryHandler<AddNewBookQuery, bool>
    {
        private readonly ILibraryRepository _libraryRepository;

        public AddNewBook(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public bool Handle(AddNewBookQuery query)
        {
            var numberOfAddedItems = _libraryRepository.AddNewBook(new Book
            {
                ISBN = query.ISBN
            });

            return numberOfAddedItems == 1;
        }
    }
}