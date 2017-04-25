using System.Collections.Generic;

namespace HomeLibrary.DataLayer
{
    public class LibraryRepository : ILibraryRepository
    {
        public IList<BookInfo> GetAllBooks()
        {
            return new List<BookInfo>
            {
                new BookInfo {Id = 0, Title = "Gra Endera", Author = "Orson Scott Card", Localisation = "Gliwice", ISBN = "9788376482514"},
                new BookInfo {Id = 1, Title = "Cień Endera", Author = "Orson Scott Card", Localisation = "Gliwice", ISBN = "9788378397649"}
            };
        }

        public BookInfo FindBookWithGivenIsbn(string isbn)
        {
            return new BookInfo {ISBN = isbn};
        }
    }
}