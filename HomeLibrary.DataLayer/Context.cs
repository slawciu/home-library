using System.Data.Entity;

namespace HomeLibrary.DataLayer
{
    public class Context : DbContext, IContext
    {
        public IDbSet<Book> Books { get; set; }
    }
}