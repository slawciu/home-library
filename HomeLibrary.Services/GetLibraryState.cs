namespace HomeLibrary.Services
{
    public class GetLibraryState : IQueryHandler<GetLibraryStateQuery, LibraryState>
    {
        private readonly ILibraryRepository _libraryRepository;

        public GetLibraryState(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public LibraryState Handle(GetLibraryStateQuery query)
        {
            var bookInfos = _libraryRepository.GetAllBooks();

            return new LibraryState
            {
                Books = bookInfos
            };
        }
    }
}