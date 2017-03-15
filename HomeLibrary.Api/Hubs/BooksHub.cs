using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace HomeLibrary.Api.Hubs
{
    [HubName("library")]
    public class BooksHub : Hub
    {
        public void GetLibraryState(string myIdentity)
        {
            Clients.Caller.updateLibraryState($"new state for you, {myIdentity}");
        }
    }
}