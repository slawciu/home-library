using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace HomeLibrary.Services.Tests
{
    public class WhenGetLibraryStateQueryHandlerCalled
    {
        [Fact]
        public void ShouldReturnLibraryState()
        {
            var getLibraryState = new GetLibraryState();

            var libraryState = getLibraryState.Handle(new GetLibraryStateQuery());

            Assert.NotNull(libraryState);
        }

        [Fact]
        public void ShouldRetrieveBookListFromRepository()
        {
            var libraryRepository = new Mock<ILibraryRepository>();
            var getLibraryState = new GetLibraryState();

            getLibraryState.Handle(new GetLibraryStateQuery());

            libraryRepository.Verify(x => x.GetAllBooks());
        }
    }
}
