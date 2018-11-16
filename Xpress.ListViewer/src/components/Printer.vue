<template>
  <div>
      <button v-on:click="counter += 1">Add 1</button>
  </div>
</template>

<script>
import queueService from "@/services/queque-service"
import printerService from "@/services/printer-service"
import axios from 'axios'

export default {
  name: 'Printer',

  data () {
    return {
      queue: {
        nextTicketNumberToServe: 0,
        tickets: []
      }
  }
},
methods: {
  createTicket: function () {
    var ticket = {
      "queueType": "Deli"
    }
    return queueService.createTicket(ticket)
      .then(response => this.queue = response)
  },
  printTicket: function () {
    return queueService.fetchQueue("Deli")
      .then(response => this.queue = response)
  }
}
}
</script>

<style scoped>
h1 {
  font-weight: bold;
  font-size: 25em;
  margin:auto;
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
