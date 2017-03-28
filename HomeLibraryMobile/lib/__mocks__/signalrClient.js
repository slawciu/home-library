const signalrClient = jest.mock('signalrClient');

function getSignalRConnection() {
    return {};
}
signalrClient.getSignalRConnection = getSignalRConnection;
module.exports = signalrClient;