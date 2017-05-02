using System.Data.Entity;
using Moq;
using Xunit;

namespace HomeLibrary.DataLayer.Tests
{
    public class WhenAddBookCalled
    {
        private readonly Mock<IContext> _contextMock;
        private readonly Mock<IDbSet<Book>> _booksDbSetMock;
        private readonly LibraryRepository _libraryRepository;

        public WhenAddBookCalled()
        {
            _contextMock = new Mock<IContext>();
            _booksDbSetMock = new Mock<IDbSet<Book>>();

            _contextMock.Setup(x => x.Books).Returns(_booksDbSetMock.Object);

            _libraryRepository = new LibraryRepository(_contextMock.Object);
        }

        [Fact]
        public void ShouldAddNewBookToDatabase()
        {
            var book = new Book();
            _libraryRepository.AddNewBook(book);

            _booksDbSetMock.Verify(x => x.Add(book));
        }

        [Fact]
        public void ShouldSaveContext()
        {
            var book = new Book();

            _libraryRepository.AddNewBook(book);

            _contextMock.Verify(x => x.SaveChanges());
        }

        [Fact]
        public void ShouldReturnStatusReturnedByContext()
        {
            var book = new Book();
            var expectedAddedItems = 1;

            _contextMock.Setup(x => x.SaveChanges()).Returns(expectedAddedItems);

            var itemsAdded = _libraryRepository.AddNewBook(book);

            Assert.Equal(expectedAddedItems, itemsAdded);
        }
    }
}