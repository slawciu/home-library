using System.Configuration;
using System.Data.Entity.Infrastructure;

namespace HomeLibrary.DataLayer
{
    public class LibraryContextFactory : IDbContextFactory<Context>
    {
        public Context Create()
        {
            return new Context(ConfigurationManager.ConnectionStrings["LibraryContext"].ToString());
        }
    }
}