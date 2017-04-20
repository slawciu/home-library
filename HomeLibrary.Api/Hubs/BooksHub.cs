using System.Collections.Generic;
using HomeLibrary.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace HomeLibrary.Api.Hubs
{
    [HubName("library")]
    public class BooksHub : Hub
    {
        private readonly IQueryHandler<GetLibraryStateQuery, LibraryState> _getLibraryStateQuery;

        public BooksHub(IQueryHandler<GetLibraryStateQuery, LibraryState>  getLibraryStateQuery)
        {
            _getLibraryStateQuery = getLibraryStateQuery;
        }

        public void GetLibraryState(string myIdentity)
        {
            var libraryState = _getLibraryStateQuery.Handle(new GetLibraryStateQuery());
            Clients.Caller.updateLibraryState(libraryState);
        }

        public void IsbnScanned(string isbn)
        {
            Clients.Caller.newBookInfo(
                new BookInfo { Id = -1, Title = "Cieñ Olbrzyma", Author = "Orson Scott Card", Localisation = "Gliwice", ISBN = isbn});
        }
    }
}