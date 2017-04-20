using System.Collections.Generic;

namespace HomeLibrary.Api.Hubs
{
    public class LibraryState
    {
        public IList<BookInfo> Books { get; set; }
    }
}