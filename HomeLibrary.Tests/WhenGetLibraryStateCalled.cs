﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Castle.Core.Internal;
using HomeLibrary.Api.Hubs;
using HomeLibrary.DataLayer;
using HomeLibrary.Services;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using Xunit;

namespace HomeLibrary.Tests
{
    public class WhenGetLibraryStateCalled
    {
        private readonly BooksHub _hub;
        private readonly Mock<IHubCallerConnectionContext<object>> _mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
        private readonly Mock<IQueryHandler<GetLibraryStateQuery, LibraryState>> _getLibraryStateQueryHandlerMock = new Mock<IQueryHandler<GetLibraryStateQuery, LibraryState>>();

        public WhenGetLibraryStateCalled()
        {
            _hub = new BooksHub(_getLibraryStateQueryHandlerMock.Object, new Mock<IQueryHandler<FindBookQuery, IList<Book>>>().Object, new Mock<IQueryHandler<AddNewBookQuery, bool>>().Object);
            _hub.Clients = _mockClients.Object;
        }

        [Fact]
        public void ShouldCallLibraryStateUpdate()
        {
            var updateLibraryStateCalled = false;
            
            dynamic caller = new ExpandoObject();
            caller.updateLibraryState = new Action<object>((book) => {
                updateLibraryStateCalled = true;
            });

            _mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            _hub.GetLibraryState("test");

            Assert.True(updateLibraryStateCalled);
        }

        [Fact]
        public void ShouldReturnLibraryStateFromQueryHandler()
        {
            LibraryState libraryStateFromHub = null;
            var expectedLibraryState = new LibraryState();

            _getLibraryStateQueryHandlerMock.Setup(x => x.Handle(It.IsAny<GetLibraryStateQuery>())).Returns(expectedLibraryState);

            dynamic caller = new ExpandoObject();
            caller.updateLibraryState = new Action<LibraryState>((libraryState) => {
                libraryStateFromHub = libraryState;
            });

            _mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            _hub.GetLibraryState("test");

            Assert.Equal(expectedLibraryState, libraryStateFromHub);
        }

        [Fact]
        public void ShouldCallGetLibraryStateQueryHandler()
        {
            dynamic caller = new ExpandoObject();
            caller.updateLibraryState = new Action<LibraryState>((libraryState) => { });

            _mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            _hub.GetLibraryState("test");

            _getLibraryStateQueryHandlerMock.Verify(x => x.Handle(It.IsAny<GetLibraryStateQuery>()));
        }
    }
}