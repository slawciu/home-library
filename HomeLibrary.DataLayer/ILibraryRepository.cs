using System.Collections.Generic;

namespace HomeLibrary.DataLayer
{
    public interface ILibraryRepository
    {
        IList<Book> GetAllBooks();
        Book FindBookWithGivenIsbn(string isbn);
    }
}