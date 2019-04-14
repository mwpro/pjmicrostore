import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import BootstrapVue from 'bootstrap-vue'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import './css/dashboard.css'

Vue.use(BootstrapVue)
Vue.config.productionTip = false;

Vue.filter('currency', (price) => `${price != null ? price.toFixed(2) : "0,00"} PLN`);
Vue.filter('date', date => new Date(date).toLocaleDateString());
Vue.filter('dateTime', date => {
  let d = new Date(date);
  return `${d.toLocaleDateString()} ${d.toLocaleTimeString()}`
});

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app');
