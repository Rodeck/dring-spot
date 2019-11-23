import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import FetchExample from './Components/UsersComponent'
import {
  createAppContainer,
} from 'react-navigation';
import firebase from 'react-native-firebase';
import { createStackNavigator } from "react-navigation-stack";

import Loading from './Components/Loading'
import SignUp from './Components/SignUp'
import Login from './Components/Login'
import Main from './Components/Main'

var config = {
  apiKey: "AIzaSyBuPL2Rm0w0BU4WY3E38dvaVeXOJTmpKmY",
  authDomain: "dringspotapp-d8e8d.firebaseapp.com",
  databaseURL: "https://dringspotapp-d8e8d.firebaseio.com",
  projectId: "dringspotapp-d8e8d",
  storageBucket: "dringspotapp-d8e8d.appspot.com",
  messagingSenderId: "671972225716",
  appId: "1:671972225716:web:aacd5822bb791b55a0dde1",
  measurementId: "G-YP8YJE7HG6"
};

firebase.initializeApp(config);

const AppNavigator = createStackNavigator({
  Home: { screen: Loading }
});

export default createAppContainer(AppNavigator);
