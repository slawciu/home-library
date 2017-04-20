using System;
using System.Dynamic;
using HomeLibrary.Api.Hubs;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using Xunit;

namespace HomeLibrary.Tests
{
    public class WhenIsbnScannedCalled
    {
        [Fact]
        public void ShouldCallNewBookInfoWithBookInfo()
        {
            BookInfo receivedBook = null;
            var hub = new BooksHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();

            hub.Clients = mockClients.Object;

            dynamic caller = new ExpandoObject();
            caller.newBookInfo = new Action<BookInfo>((book) => {
                receivedBook = book;
            });

            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            hub.IsbnScanned("test");

            Assert.NotNull(receivedBook);
        }
    }
}