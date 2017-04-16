using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace HomeLibrary.Api.Hubs
{
    [HubName("library")]
    public class BooksHub : Hub
    {
        public void GetLibraryState(string myIdentity)
        {
            Clients.Caller.updateLibraryState(new
            {
                books = new object[]
                        {
                            new { id = 0, title= "Gra Endera", author = "Orson Scott Card", localisation= "Gliwice"},
                            new { id= 1, title= "Cieñ Endera", author = "Orson Scott Card", localisation= "Gliwice"}
                        }
            });
        }

        public void IsbnScanned(string isbn)
        {
            Clients.Caller.newBookInfo(
                new {id = -1, title = "Cieñ Olbrzyma", author = "Orson Scott Card", localisation = "Gliwice", isbn = isbn});
        }

        public void Test(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }
    }
}