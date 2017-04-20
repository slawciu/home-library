using System.Collections.Generic;

namespace HomeLibrary.Services
{
    public interface ILibraryRepository
    {
        IList<BookInfo> GetAllBooks();
    }
}