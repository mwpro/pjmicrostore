import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';

Vue.config.productionTip = false;

Vue.filter('currency', (price) => `${price != null ? price.toFixed(2) : "0,00"} PLN`);
Vue.filter('date', date => new Date(date).toLocaleDateString());

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app');
