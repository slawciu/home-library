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
    }
}