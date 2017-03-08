using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace HomeLib.Api.Hubs
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