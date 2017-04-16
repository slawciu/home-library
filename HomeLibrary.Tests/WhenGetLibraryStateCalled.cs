using System;
using System.Dynamic;
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
    }
}