import Config from 'react-native-config'

function getCurrentLocation() {
    var url = Config.GEOLOCATION_API;

    fetch(url)
        .then((response) => response.json())
        .then((responseJson) => {
            return responseJson.city_name;
        })
        .catch((error) => {
            return "";
        })
}

let geolocationClient = {}

geolocationClient.getCurrentLocation = getCurrentLocation;
module.exports = geolocationClient;