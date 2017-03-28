const signalrClient = jest.mock('signalrClient');

const proxy = { invoke: jest.fn()}

function getSignalRConnection() {
    return {};
}

function getSignalRProxy() {
    return proxy;
}

signalrClient.getSignalRProxy = getSignalRProxy;
signalrClient.getSignalRConnection = getSignalRConnection;
module.exports = signalrClient;