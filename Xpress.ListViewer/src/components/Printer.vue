<template>
  <div>
      <button class="deli-button" v-on:click="createTicket()">Deli</button>
      <button class="post-button" v-on:click="createTicket()">Post</button>
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
  }
},
methods: {
  createTicket: function () {
    var ticketType = {
      "queueType": "Deli"
    }
    return queueService.createTicket(ticketType)
      .then(response => printTicket(response))
  },
  printTicket: function(ticket) {
    console.log(ticket)
    var _rows = [];
    _rows.push('\u001b|3C\u001b|bC' + ticket.ticketNumber + '\n');
    var _footerRows = [];

    var data = {
    "apiKey": "fewfew",
    "rows": _rows,
    "footerRows": _footerRows
    };
    /*return printerService.printTicket(data)
      .then(response => this.queue = response)*/
  }
}
}
</script>

<style scoped>
.deli-button {
    background-color: #4CAF50;
}
.post-button {
    background-color: #dd4447;
}
button {
    border: none;
    color: white;
    padding: 15px 32px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
}
</style>
