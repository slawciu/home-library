using System.Collections.Generic;

namespace HomeLibrary.Services
{
    public class LibraryRepository : ILibraryRepository
    {
        public IList<BookInfo> GetAllBooks()
        {
            return new List<BookInfo>
            {
                new BookInfo {Id = 0, Title = "Gra Endera", Author = "Orson Scott Card", Localisation = "Gliwice"},
                new BookInfo {Id = 1, Title = "Cień Endera", Author = "Orson Scott Card", Localisation = "Gliwice"}
            };
        }
    }
}