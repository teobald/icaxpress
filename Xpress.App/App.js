/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow
 */

import React, {Component} from 'react';
import {Platform, StyleSheet, Text, View, TouchableOpacity} from 'react-native';
import { getQueue, getTicket } from './services';

const instructions = Platform.select({
  ios: 'Press Cmd+R to reload,\n' + 'Cmd+D or shake for dev menu',
  android:
    'Double tap R on your keyboard to reload,\n' +
    'Shake or press menu button for dev menu',
});

type Props = {};
export default class App extends Component<Props> {

  constructor (props) {
      super(props)
      this.state = {
        queue: 0,
        queueType: "",
        nextNumber: undefined,
        ticketsBefore: "",
        ticketId: ""
      }
  }

  componentDidMount () {
    this.setState((state) => {
      return {queue: 0}
    });
    const baseurl = "http://sec31098.ica.ia-hc.net:8088/"
    return getQueue()
    .then((response) => {
      // this.state.queue = response;
      console.log(response);
      this.setState((state) => {
        return {
          queueType: response.tickets[0].queueType,
          nextNumber: response.nextTicketNumberToServe
        }
      });
    })
    .catch(() => {
      this.setState((state) => {
        return {queue: "Inget nummer"}
      });
      })
  }

  render() {
    return (
      <View style={styles.container}>
        <Text style={styles.welcome}>Ditt nummer för {this.state.queueType} är: </Text>
        <Text style={styles.instructions}>{this.state.queue}</Text>
        <Text style={styles.welcome}>Nästa nummer är: {this.state.nextNumber} </Text>
        <Text style={styles.welcome}>Antal nummer före dig i kön: {this.state.ticketsBefore} </Text>
        <TouchableOpacity onPress={this.getTicket} style={styles.button}><Text style={styles.buttontext}>Deli</Text></TouchableOpacity>
      </View>
    );
  }

  getTicket = () => {
    return getTicket()
    .then((response) => {
      this.setState((state) => {
        return {
          queue: response.ticketNumber,
          ticketsBefore: response.ticketsBefore,
          ticketId: response.id
        }
        })
      })
    .catch(() => {
      this.setState((state) => {
        return {queue: "Fail"}
      })
    })
  }
}



const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#F5FCFF',
  },
  welcome: {
    fontSize: 20,
    textAlign: 'center',
    margin: 10,
  },
  instructions: {
    textAlign: 'center',
    color: '#333333',
    marginBottom: 5,
    fontSize: 50
  },
  button: {
    backgroundColor: "orange",
    width: 100,
    height: 50
  },
  buttontext: {
    color: "white",
    textAlign: 'center',
    fontSize: 30
  }
});
