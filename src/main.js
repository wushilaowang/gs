import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'


import JsonExcel from 'vue-json-excel'

import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import vueConfig from '../vue.config'


Vue.component('downloadExcel', JsonExcel)
Vue.use(ElementUI);

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
