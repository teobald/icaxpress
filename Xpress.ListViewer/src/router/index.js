import Vue from 'vue'
import Router from 'vue-router'
import Queue from '@/components/Queue'
import Printer from '@/components/Printer'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Queue',
      component: Queue
    },
    {
      path: '/printer',
      name: 'Printer',
      component: Printer
    }
  ]
})
