import signalr from 'react-native-signalr';

function getSignalRConnection() {
    return signalr.hubConnection('http://192.168.0.19:57123')
}

let signalrClient = {}

signalrClient.getSignalRConnection = getSignalRConnection;

module.exports = signalrClient;