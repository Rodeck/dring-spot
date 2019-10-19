import React from 'react';
import { View, Text, StyleSheet, FlatList, ActivityIndicator } from 'react-native';

export default class FetchExample extends React.Component {

    constructor(props){
      super(props);
      this.state ={ isLoading: true}
    }
  
    componentDidMount(){
      return fetch('http://192.168.43.215:5005/User/')
        .then((response) => response.json())
        .then((responseJson) => {
  
          this.setState({
            isLoading: false,
            dataSource: responseJson.movies,
          }, function(){
  
          });
  
        })
        .catch((error) =>{
          console.error(error);
        });
    }
  
  
  
    render(){
  
      if(this.state.isLoading){
        return(
          <View style={{flex: 1, padding: 20}}>
            <ActivityIndicator/>
          </View>
        )
      }
  
      return(
        <View style={{flex: 1, paddingTop:20}}>
          <FlatList
            data={this.state.dataSource}
            renderItem={({item}) => <Text>{item.title}, {item.releaseYear}</Text>}
            keyExtractor={({id}, index) => id}
          />
        </View>
      );
    }
  }