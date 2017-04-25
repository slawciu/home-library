using System;
using System.Collections.Generic;
using System.Dynamic;
using HomeLibrary.Api.Hubs;
using HomeLibrary.DataLayer;
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
        private readonly Mock<IQueryHandler<FindBookQuery, IList<Book>>> _findBookQueryHandlerMock;

        public WhenIsbnScannedCalled()
        {
            _findBookQueryHandlerMock = new Mock<IQueryHandler<FindBookQuery, IList<Book>>>();

            _booksHub = new BooksHub(new Mock<IQueryHandler<GetLibraryStateQuery, LibraryState>>().Object, _findBookQueryHandlerMock.Object);
            _clientsMock = new Mock<IHubCallerConnectionContext<dynamic>>();

            _booksHub.Clients = _clientsMock.Object;
        }

        [Fact]
        public void ShouldCallFindBookQueryHandler()
        {
            dynamic caller = new ExpandoObject();
            caller.newBookInfo = new Action<IList<Book>>((book) => { });

            _clientsMock.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            _booksHub.IsbnScanned("9788375106626");

            _findBookQueryHandlerMock.Verify(x => x.Handle(It.Is<FindBookQuery>(query => query.ISBN == "9788375106626")));
        }

        [Fact]
        public void ShouldCallNewBookInfoWithFoundItems()
        {
            IList<Book> foundBooks = new List<Book>();

            var expectedBooks = new List<Book>
            {
                new Book
                {
                    ISBN = "9788375106626"
                }
            };

            dynamic caller = new ExpandoObject();
            caller.newBookInfo = new Action<IList<Book>>((books) =>
            {
                foundBooks = books;
            });

            _clientsMock.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            _findBookQueryHandlerMock.Setup(x => x.Handle(It.Is<FindBookQuery>(query => query.ISBN == "9788375106626"))).Returns(expectedBooks);

            _booksHub.IsbnScanned("9788375106626");

            Assert.Equal(expectedBooks, foundBooks);
        }
    }
}