using System;
using System.Collections.Generic;
using HomeLibrary.DataLayer;
using Moq;
using Xunit;

namespace HomeLibrary.Services.Tests
{
    public class WhenFindBookQueryHandlerCalled
    {
        private readonly FindBook _findBook;
        private readonly Mock<ILibraryRepository> _libraryRepositoryMock;

        public WhenFindBookQueryHandlerCalled()
        {
            _libraryRepositoryMock = new Mock<ILibraryRepository>();
            _findBook = new FindBook(_libraryRepositoryMock.Object);
        }

        [Fact]
        public void ShouldReturnListOfBookInformationForGivenIsbn()
        {
            var bookInfo = _findBook.Handle(new FindBookQuery {ISBN = "9788375106626" });

            Assert.IsType<List<BookInfo>>(bookInfo);
        }

        [Fact]
        public void ShouldCheckIfBookWithGivenIsbnExistsInRepository()
        {
            _findBook.Handle(new FindBookQuery {ISBN = "9788375106626" });

            _libraryRepositoryMock.Verify(x => x.FindBookWithGivenIsbn("9788375106626"));
        }

        [Fact]
        public void ShouldReturnBookInfoFromRepositoryIfExists()
        {
            var existingBook = new BookInfo
            {
                Id = 1,
                ISBN = "9788375106626",
                Title = "Diuna",
                Author = "Frank Herbert",
                Localisation = "Gliwice"
            };
            _libraryRepositoryMock.Setup(x => x.FindBookWithGivenIsbn("9788375106626")).Returns(() => existingBook);

            var bookInfos = _findBook.Handle(new FindBookQuery { ISBN = "9788375106626" });

            Assert.Collection(bookInfos, bookInfo => Assert.Equal(existingBook, bookInfo));
        }

        [Fact]
        public void ShouldReturnEmptyBookInformationWhenNotFoundInRepository()
        {
            _libraryRepositoryMock.Setup(x => x.FindBookWithGivenIsbn("9788375106626")).Returns(() => null);

            var bookInfos = _findBook.Handle(new FindBookQuery { ISBN = "9788375106626" });

            Assert.Collection(bookInfos, AssertIsEmpty);
        }

        [Fact]
        public void ShouldCallGoogleApiInOrderToFindBookWithGivenIsbn()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldCallKsiazkiOrgApiInOrderToFindBookWithGivenIsbn()
        {
            throw new NotImplementedException();
        }

        private static void AssertIsEmpty(BookInfo bookInfo)
        {
            Assert.Equal("", bookInfo.Author);
            Assert.Equal("", bookInfo.Localisation);
            Assert.Equal("", bookInfo.Title);
            Assert.Equal(0, bookInfo.Id);
            Assert.NotEqual("", bookInfo.ISBN);
        }
    }
}