using System;
using System.Dynamic;
using HomeLibrary.Api.Hubs;
using HomeLibrary.Services;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using Xunit;

namespace HomeLibrary.Tests
{
    public class WhenIsbnScannedCalled
    {
        private readonly BooksHub _booksHub;
        private readonly Mock<IHubCallerConnectionContext<dynamic>> _clientsMock;

        public WhenIsbnScannedCalled()
        {
            _booksHub = new BooksHub(new Mock<IQueryHandler<GetLibraryStateQuery, LibraryState>>().Object);
            _clientsMock = new Mock<IHubCallerConnectionContext<dynamic>>();

            _booksHub.Clients = _clientsMock.Object;
        }

        [Fact]
        public void ShouldCallNewBookInfoWithBookInfo()
        {
            BookInfo receivedBook = null;

            dynamic caller = new ExpandoObject();
            caller.newBookInfo = new Action<BookInfo>((book) => {
                receivedBook = book;
            });

            _clientsMock.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            _booksHub.IsbnScanned("test");

            Assert.NotNull(receivedBook);
        }

        [Fact]
        public void ShouldCallFindBookQueryHandler()
        {
            throw new NotImplementedException();
        }
    }
}