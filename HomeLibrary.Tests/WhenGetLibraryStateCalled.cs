using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Castle.Core.Internal;
using HomeLibrary.Api.Hubs;
using HomeLibrary.Services;
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
            var hub = new BooksHub(new Mock<IQueryHandler<GetLibraryStateQuery, LibraryState>>().Object);
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
        public void ShouldReturnLibraryStateFromQueryHandler()
        {
            LibraryState libraryStateFromHub = null;
            var getLibraryStateQueryMock = new Mock<IQueryHandler<GetLibraryStateQuery, LibraryState>>();

            var expectedLibraryState = new LibraryState();
            getLibraryStateQueryMock.Setup(x => x.Handle(It.IsAny<GetLibraryStateQuery>())).Returns(expectedLibraryState);

            var hub = new BooksHub(getLibraryStateQueryMock.Object);
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();

            hub.Clients = mockClients.Object;

            dynamic caller = new ExpandoObject();
            caller.updateLibraryState = new Action<LibraryState>((libraryState) => {
                libraryStateFromHub = libraryState;
            });

            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            hub.GetLibraryState("test");

            Assert.Equal(expectedLibraryState, libraryStateFromHub);
        }

        [Fact]
        public void ShouldCallGetLibraryStateQueryHandler()
        {
            var getLibraryStateQueryHandlerMock = new Mock<IQueryHandler<GetLibraryStateQuery, LibraryState>>();
            var hub = new BooksHub(getLibraryStateQueryHandlerMock.Object);
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();

            hub.Clients = mockClients.Object;

            dynamic caller = new ExpandoObject();
            caller.updateLibraryState = new Action<LibraryState>((libraryState) => { });

            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            hub.GetLibraryState("test");

            getLibraryStateQueryHandlerMock.Verify(x => x.Handle(It.IsAny<GetLibraryStateQuery>()));
        }
    }
}