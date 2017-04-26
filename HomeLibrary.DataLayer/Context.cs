using System.Data.Entity;

namespace HomeLibrary.DataLayer
{
    public class Context : DbContext, IContext
    {
        public Context(string connectionString) : base(connectionString)
        {
        }

        public IDbSet<Book> Books { get; set; }
    }
}