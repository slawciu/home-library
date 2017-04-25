using System.Collections.Generic;
using HomeLibrary.DataLayer;
using HomeLibrary.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace HomeLibrary.Api.Hubs
{
    [HubName("library")]
    public class BooksHub : Hub
    {
        private readonly IQueryHandler<GetLibraryStateQuery, LibraryState> _getLibraryStateQueryHandler;
        private readonly IQueryHandler<FindBookQuery, IList<BookInfo>> _findBookQueryHandler;

        public BooksHub(IQueryHandler<GetLibraryStateQuery, LibraryState> getLibraryStateQueryHandler, IQueryHandler<FindBookQuery, IList<BookInfo>> findBookQueryHandler)
        {
            _getLibraryStateQueryHandler = getLibraryStateQueryHandler;
            _findBookQueryHandler = findBookQueryHandler;
        }

        public void GetLibraryState(string myIdentity)
        {
            var libraryState = _getLibraryStateQueryHandler.Handle(new GetLibraryStateQuery());
            Clients.Caller.updateLibraryState(libraryState);
        }

        public void IsbnScanned(string isbn)
        {
            var bookInfos = _findBookQueryHandler.Handle(new FindBookQuery {ISBN = isbn});
            Clients.Caller.newBookInfo(bookInfos);
        }
    }
}