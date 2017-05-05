import signalr from 'react-native-signalr';
import Config from 'react-native-config'

const connection = null;
function getSignalRConnection() {
    if (connection === null) {
        connection = signalr.hubConnection(Config.SIGNALR_URL)
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