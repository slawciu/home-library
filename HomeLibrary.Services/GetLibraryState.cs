namespace HomeLibrary.Services
{
    public class GetLibraryState : IQueryHandler<GetLibraryStateQuery, LibraryState>
    {
        public LibraryState Handle(GetLibraryStateQuery query)
        {
            //new LibraryState
            //{
            //    Books = new List<BookInfo>
            //    {
            //        new BookInfo { Id = 0, Title = "Gra Endera", Author = "Orson Scott Card", Localisation = "Gliwice"},
            //        new BookInfo { Id = 1, Title = "Cień Endera", Author = "Orson Scott Card", Localisation = "Gliwice"}
            //    }
            //}

            return new LibraryState();
        }
    }
}