using System.Data.Entity;

namespace HomeLibrary.DataLayer
{
    public class Context : DbContext, IContext
    {
        public Context(string connectionString) : base(connectionString)
        {
        }

        public IDbSet<Book> Books { get; set; }
        public IDbSet<BookCopy> BookCopies { get; set; }
        public IDbSet<Localisation> Localisations { get; set; }
        public IDbSet<Condition> Conditions { get; set; }
        public IDbSet<Author> Authors { get; set; }
        public IDbSet<Publisher> Publishers { get; set; }
    }
}