using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Castle.Core.Internal;
using HomeLibrary.Api.Hubs;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using Xunit;

namespace HomeLibrary.Tests
{
    public class WhenGetLibraryStateCalled
    {
        [Fact]
        public void ShouldCallLibraryStateUpdate()
        {
            bool sendCalled = false;
            var hub = new BooksHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();

            hub.Clients = mockClients.Object;

            dynamic caller = new ExpandoObject();
            caller.updateLibraryState = new Action<object>((book) => {
                sendCalled = true;
            });

            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            hub.GetLibraryState("test");

            Assert.True(sendCalled);
        }

        [Fact]
        public void ShouldReturnListOfBooksInLibrary()
        {
            IList<BookInfo> booksFromHub = new List<BookInfo>();
            var hub = new BooksHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();

            hub.Clients = mockClients.Object;

            dynamic caller = new ExpandoObject();
            caller.updateLibraryState = new Action<LibraryState>((libraryState) => {
                booksFromHub = libraryState.Books;
            });

            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            hub.GetLibraryState("test");

            Assert.True(booksFromHub.Any());
        }
    }
}