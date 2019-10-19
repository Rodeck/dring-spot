import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import FetchExample from './Components/UsersComponent'

export default function App() {
  return (
    <View style={styles.container}>
      <FetchExample> </FetchExample>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
