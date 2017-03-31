import signalr from 'react-native-signalr';
const connection = null;
function getSignalRConnection() {
    if (connection === null) {
        connection = signalr.hubConnection('http://10.57.200.76:57123')
    }
    return connection
}

function getSignalRProxy() {
    return getSignalRConnection().createHubProxy('library');;
}

let signalrClient = {}

signalrClient.getSignalRConnection = getSignalRConnection;
signalrClient.getSignalRProxy = getSignalRProxy;
module.exports = signalrClient;