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
        private readonly IQueryHandler<FindBookQuery, IList<Book>> _findBookQueryHandler;
        private readonly IQueryHandler<AddNewBookQuery, bool> _addNewBookQueryHandler;

        public BooksHub(IQueryHandler<GetLibraryStateQuery, LibraryState> getLibraryStateQueryHandler, IQueryHandler<FindBookQuery, IList<Book>> findBookQueryHandler, IQueryHandler<AddNewBookQuery, bool> addNewBookQueryHandler)
        {
            _getLibraryStateQueryHandler = getLibraryStateQueryHandler;
            _findBookQueryHandler = findBookQueryHandler;
            _addNewBookQueryHandler = addNewBookQueryHandler;
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

        public void AddNewBook(BookRequest newBookRequest)
        {
            if (!newBookRequest.IsValid())
            {
                Clients.Caller.failureWhileAddingNewBook();
                return;
            }

            var bookAddedSuccessfully = _addNewBookQueryHandler.Handle(new AddNewBookQuery {ISBN = newBookRequest.ISBN, Author = newBookRequest.Author, Title = newBookRequest.Title, Localisation = newBookRequest.Localisation});

            if (bookAddedSuccessfully)
            {
                Clients.All.newBookAddedSuccessfully();
            }
            else
            {
                Clients.Caller.failureWhileAddingNewBook();
            }
        }
    }
}