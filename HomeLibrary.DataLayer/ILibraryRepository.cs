using System.Collections.Generic;

namespace HomeLibrary.DataLayer
{
    public interface ILibraryRepository
    {
        IList<BookInfo> GetAllBooks();
        BookInfo FindBookWithGivenIsbn(string isbn);
    }
}