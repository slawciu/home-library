using System.Data.Entity.Migrations;

namespace HomeLibrary.DataLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<HomeLibrary.DataLayer.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "HomeLibrary.DataLayer.Context";
        }

        protected override void Seed(Context context)
        {
            //  This method will be called after migrating to the latest version.

            context.Books.AddOrUpdate(p => p.Id, new Book { Id = 1, Title = "Gra Endera", Author = "Orson Scott Card", Localisation = "Gliwice", ISBN = "9788376482514" });
            context.Books.AddOrUpdate(p => p.Id, new Book { Id = 2, Title = "Cieñ Endera", Author = "Orson Scott Card", Localisation = "Gliwice", ISBN = "9788378397649" });

            context.SaveChanges();
        }
    }
}
