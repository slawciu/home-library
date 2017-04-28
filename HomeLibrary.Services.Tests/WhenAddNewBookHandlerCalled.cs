using HomeLibrary.DataLayer;
using Moq;
using Xunit;

namespace HomeLibrary.Services.Tests
{
    public class WhenAddNewBookHandlerCalled
    {
        private readonly Mock<ILibraryRepository> _repositoryMock;

        public WhenAddNewBookHandlerCalled()
        {
            _repositoryMock = new Mock<ILibraryRepository>();
        }

        [Fact]
        public void ShouldCallAddNewBookOnRepository()
        {
            var addNewBookHandler = new AddNewBook(_repositoryMock.Object);
            var newBookQuery = new AddNewBookQuery {ISBN = "0123456789012"};
            addNewBookHandler.Handle(newBookQuery);

            _repositoryMock.Verify(x => x.AddNewBook(It.Is<Book>(book => book.ISBN == newBookQuery.ISBN)));
        }

        [Fact]
        public void ShouldReturnTrueIfAddedExactlyOneRecord()
        {
            _repositoryMock.Setup(mock => mock.AddNewBook(It.Is<Book>(x => x.ISBN == "0123456789012"))).Returns(1);

            var addNewBookHandler = new AddNewBook(_repositoryMock.Object);
            var newBookQuery = new AddNewBookQuery { ISBN = "0123456789012" };

            var handlerResult = addNewBookHandler.Handle(newBookQuery);

            Assert.True(handlerResult);
        }

        [Fact]
        public void ShouldReturnFalseWhenRepositoryDidNotReturnOne()
        {
            _repositoryMock.Setup(mock => mock.AddNewBook(It.Is<Book>(x => x.ISBN == "0123456789012"))).Returns(0);

            var addNewBookHandler = new AddNewBook(_repositoryMock.Object);
            var newBookQuery = new AddNewBookQuery { ISBN = "0123456789012" };

            var handlerResult = addNewBookHandler.Handle(newBookQuery);

            Assert.False(handlerResult);
        }
    }
}