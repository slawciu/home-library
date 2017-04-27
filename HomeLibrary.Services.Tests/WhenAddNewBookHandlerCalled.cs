using HomeLibrary.DataLayer;
using Moq;
using Xunit;

namespace HomeLibrary.Services.Tests
{
    public class WhenAddNewBookHandlerCalled
    {
        private Mock<ILibraryRepository> _repositoryMock;

        public WhenAddNewBookHandlerCalled()
        {
            _repositoryMock = new Mock<ILibraryRepository>();
        }

        [Fact]
        public void ShouldCallAddNewBookOnRepository()
        {
            var addNewBookHandler = new AddNewBook();
            var newBookQuery = new AddNewBookQuery {ISBN = "0123456789012"};
            addNewBookHandler.Handle(newBookQuery);

            _repositoryMock.Verify(x => x.AddNewBook(It.Is<Book>(book => book.ISBN == newBookQuery.ISBN)));
        }
    }
}