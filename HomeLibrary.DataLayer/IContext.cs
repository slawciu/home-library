using System.Data.Entity;

namespace HomeLibrary.DataLayer
{
    public interface IContext
    {
        IDbSet<Book> Books { get; set; }
    }
}