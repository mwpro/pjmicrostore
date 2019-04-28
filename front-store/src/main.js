import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import App from './App.vue';
import router from './router';
import store from './store';
import AuthService from './auth/authService';

import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

import './css/store.css';

Vue.use(BootstrapVue);
Vue.use(AuthService);
Vue.config.productionTip = false;

Vue.filter('currency', price => `${price != null ? price.toFixed(2) : '0,00'} PLN`);
Vue.filter('date', date => new Date(date).toLocaleDateString());

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app');
