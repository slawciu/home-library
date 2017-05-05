using System;
using System.Collections.Generic;
using System.Dynamic;
using HomeLibrary.Api;
using HomeLibrary.Api.Hubs;
using HomeLibrary.DataLayer;
using HomeLibrary.Services;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using Xunit;

namespace HomeLibrary.Tests
{
    public class WhenAddNewBookCalled
    {
        private readonly BooksHub _booksHub;
        private readonly Mock<IHubCallerConnectionContext<object>> _mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
        private readonly Mock<IQueryHandler<GetLibraryStateQuery, LibraryState>> _getLibraryStateQueryHandlerMock = new Mock<IQueryHandler<GetLibraryStateQuery, LibraryState>>();
        private readonly Mock<IQueryHandler<AddNewBookQuery, bool>> _addNewBookQueryHandlerMock;

        public WhenAddNewBookCalled()
        {
            _addNewBookQueryHandlerMock = new Mock<IQueryHandler<AddNewBookQuery, bool>>();
            _booksHub = new BooksHub(_getLibraryStateQueryHandlerMock.Object, new Mock<IQueryHandler<FindBookQuery, IList<Book>>>().Object, _addNewBookQueryHandlerMock.Object);
            _booksHub.Clients = _mockClients.Object;
        }

        [Fact]
        public void ShouldCallAddNewBookQueryHandler()
        {
            var newBookRequest = new BookRequest
            {
                ISBN = "0123456789012"
            };

            dynamic caller = new ExpandoObject();
            caller.newBookAddedSuccessfully = new Action(() => {});
            caller.failureWhileAddingNewBook = new Action(() => { });
            _mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            _booksHub.AddNewBook(newBookRequest);

            _addNewBookQueryHandlerMock.Verify(mock => mock.Handle(It.Is<AddNewBookQuery>(x => x.ISBN == newBookRequest.ISBN)));
        }

        [Fact]
        public void ShouldNotifyAllConnectedClientsThatNewBookAddedSuccessfully()
        {
            var newBookAddedSuccessfullyCalled = false;
            _addNewBookQueryHandlerMock.Setup(
                mock => mock.Handle(It.Is<AddNewBookQuery>(x => x.ISBN == "0123456789012"))).Returns(true);

            var newBookRequest = new BookRequest
            {
                ISBN = "0123456789012"
            };

            dynamic allClients = new ExpandoObject();
            allClients.newBookAddedSuccessfully = new Action(() => {
                newBookAddedSuccessfullyCalled = true;
            });
            _mockClients.Setup(m => m.All).Returns((ExpandoObject)allClients);

            _booksHub.AddNewBook(newBookRequest);

            Assert.True(newBookAddedSuccessfullyCalled);
        }

        [Fact]
        public void ShouldCallFailureWhileAddingNewBookOnAddingFailure()
        {
            var failureWhileAddingNewBookCalled = false;
            _addNewBookQueryHandlerMock.Setup(
                mock => mock.Handle(It.Is<AddNewBookQuery>(x => x.ISBN == "0123456789012"))).Returns(false);

            var newBookRequest = new BookRequest
            {
                ISBN = "0123456789012"
            };

            dynamic caller = new ExpandoObject();
            caller.failureWhileAddingNewBook = new Action(() => {
                failureWhileAddingNewBookCalled = true;
            });
            _mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            _booksHub.AddNewBook(newBookRequest);

            Assert.True(failureWhileAddingNewBookCalled);
        }

        [Fact]
        public void ShouldCallFailureWhenPassedRequestWithEmptyIsbnNumber()
        {
            var failureWhileAddingNewBookCalled = false;
            _addNewBookQueryHandlerMock.Setup(
                mock => mock.Handle(It.Is<AddNewBookQuery>(x => x.ISBN == "0123456789012"))).Returns(true);

            var newBookRequest = new BookRequest
            {
                ISBN = ""
            };

            dynamic caller = new ExpandoObject();
            caller.failureWhileAddingNewBook = new Action(() => {
                failureWhileAddingNewBookCalled = true;
            });
            _mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            _booksHub.AddNewBook(newBookRequest);

            Assert.True(failureWhileAddingNewBookCalled);
        }

        [Fact]
        public void ShouldNotCallHandlerWhenPassedRequestWithEmptyIsbnNumber()
        {
            _addNewBookQueryHandlerMock.Setup(
                mock => mock.Handle(It.Is<AddNewBookQuery>(x => x.ISBN == "0123456789012"))).Returns(true);

            var newBookRequest = new BookRequest
            {
                ISBN = ""
            };

            dynamic caller = new ExpandoObject();
            caller.failureWhileAddingNewBook = new Action(() => { });
            _mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            _booksHub.AddNewBook(newBookRequest);

            _addNewBookQueryHandlerMock.Verify(x => x.Handle(It.IsAny<AddNewBookQuery>()), Times.Never);
        }
    }
}