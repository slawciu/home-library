import { combineReducers } from 'redux'
import * as bookReducer from './books';

export default combineReducers(Object.assign(
    bookReducer
));