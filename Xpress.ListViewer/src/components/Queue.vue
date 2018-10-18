<template>
  <div>
    <h1>{{ number }}</h1>
  </div>
</template>

<script>
export default {
  name: 'Queue',
  data () {
    return {
      number: '7'
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: bold;
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
