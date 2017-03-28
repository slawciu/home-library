const materialUi = jest.genMockFromModule('react-native-material-ui');

function render() {
    return (
        <View>Mock</View>
    );
}

materialUi.render = render;

module.exports = materialUi;