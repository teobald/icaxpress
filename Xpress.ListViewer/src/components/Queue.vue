<template>
  <div>
    <h1>{{ queue.nextTicketNumberToServe }}</h1>
    <span v-if="queue.tickets && queue.tickets.length > 0">
      Nästa nummer:
      <li v-for="(value,index) in queue.tickets" :key=index>
        {{value.ticketNumber}} <span v-if="queue.tickets.length > (index+1)">,</span>
      </li>
    </span>
    <h2 v-if="!queue.tickets">
    Ingen i kö
  </h2>
  </div>
</template>

<script>
import queueService from "@/services/queque-service"
import axios from 'axios';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

axios
    .get('http://sec31098.ica.ia-hc.net:8088/notificationhub')
    .then((response) => execute(response.data));

export default {
  name: 'Queue',
  data () {
    return {
      queue: {
        nextTicketNumberToServe: 0,
        tickets: []
      }
    }
  },
  created() {
    return queueService.fetchQueue("Deli")
      .then(response => this.queue = response)
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1 {
  font-weight: bold;
  font-size: 25em;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
