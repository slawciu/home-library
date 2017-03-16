import React, { Component } from 'react'
import { connect } from 'react-redux'
import { ActionCreators } from '../actions'
import { bindActionCreators } from 'redux'
import  HomeLibraryMobile  from './HomeLibraryMobile'

class AppContainer extends Component {
    render() {
        return <HomeLibraryMobile {...this.props} />;
    }
}
 
function mapDispatchToProps(dispatch) {
    return bindActionCreators(ActionCreators, dispatch);
}

export default connect(() => { return {} }, mapDispatchToProps)(AppContainer);