/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow
 */

import React, {Component} from 'react';
import {Platform, StyleSheet, Text, View, TouchableOpacity} from 'react-native';
import { getQueue, getTicket, getStatus } from './services';

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
        queue: "",
        queueType: "",
        ticketsBefore: 0,
        ticketId: "",
        drawn: false,
        postalQueue: "",
        postalTicketsBefore: 0,
        postalTicketId: "",
        firstSucess: false,
        postalDrawn: false
      }

      this.state.postal = {

      }

      this.getTicket = this.getTicket.bind(this);
  }

  componentDidMount () {
    setInterval(this.getStatus, 10000)
    setInterval(this.getStatusPostal, 9000)
    return getQueue()
    .then((response) => {
      this.setState((state) => {
        return {firstSucess: true}
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
        <View style={styles.deliContainer}>
          <Text style={styles.type}>{this.state.queue && this.state.firstSucess ? "Deli" : "" }</Text>
          <Text style={styles.instructions}>{this.state.queue}</Text>
          <Text style={styles.welcome}>{this.state.ticketsBefore  && this.state.firstSucess ? "Antal nummer före: " + this.state.ticketsBefore : ""} </Text>
          {!this.state.drawn && <TouchableOpacity onPress={() => this.getTicket('Deli')} style={styles.button}><Text style={styles.buttontext}>Deli</Text></TouchableOpacity>}
        </View>
        <View style={styles.postContainer}>
          <Text style={styles.type}>{this.state.postalQueue  && this.state.firstSucess ? "Post" : "" }</Text>
          <Text style={styles.instructions}>{this.state.postalQueue}</Text>
          <Text style={styles.welcome}>{this.state.postalTicketsBefore  && this.state.firstSucess ? "Antal nummer före: " + this.state.postalTicketsBefore : ""} </Text>
          {!this.state.postalDrawn && <TouchableOpacity onPress={() => this.getTicket('PostalService')} style={styles.button}><Text style={styles.buttontext}>Post</Text></TouchableOpacity>}
        </View>
      </View>
    );
  }

  getTicket = (queueType) => {
    return getTicket(queueType)
    .then((response) => {
      if (queueType === "Deli") {
        this.setState((state) => {
          return {
            queue: response.ticketNumber,
            ticketsBefore: response.ticketsBefore,
            ticketId: response.id,
            drawn: true
          }
          })
        } else if (queueType === "PostalService") {
          this.setState((state) => {
            return {
              postalQueue: response.ticketNumber,
              postalTicketsBefore: response.ticketsBefore,
              postalTicketId: response.id,
              postalDrawn: true
            }
            })
        }
      })
    .catch(() => {
      this.setState((state) => {
        return {queue: "Fail"}
      })
    })
  }

  getStatus = () => {
    return getStatus(this.state.ticketId)
    .then((response) => {
      this.setState((state) => {
        return {
          ticketsBefore: response.ticketsBefore
        }
        })
      })
    .catch((error) => {

      })
  }

  getStatusPostal = () => {
    return getStatus(this.state.postalTicketId)
    .then((response) => {
      this.setState((state) => {
        return {
          postalTicketsBefore: response.ticketsBefore
        }
        })
      })
    .catch((error) => {

      })
  }
}



const styles = StyleSheet.create({
  container: {
    backgroundColor: '#F5FCFF',
    justifyContent: 'center',
    alignItems: 'center'
  },
  deliContainer: {
    backgroundColor: '#FCECE7',
    color: '#EC512E',
    alignSelf: 'stretch',
    justifyContent: 'center',
    alignItems: 'center',
    height: "50%"
  },
  postContainer: {
    backgroundColor: '#EC512E',
    color: '#FCECE7',
    alignSelf: 'stretch',
    justifyContent: 'center',
    alignItems: 'center',
    height: "50%"
  },
  welcome: {
    fontSize: 20,
    textAlign: 'center',
    margin: 10,
  },
  instructions: {
    textAlign: 'center',
    marginBottom: 5,
    fontSize: 50
  },
  button: {
    backgroundColor: "#3a3a3a",
    width: 100,
    height: 50
  },
  buttontext: {
    color: "white",
    textAlign: 'center',
    fontSize: 30
  },
  divider: {
    borderStyle: "solid",
    borderBottomWidth: 2,
    borderBottomColor: "black",
    height: 4
  },
  type: {
    fontWeight: "bold",
    fontSize: 32
  }
});
