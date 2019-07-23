import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import Axios from 'axios';
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
Vue.filter('dateTime', (date) => {
  const d = new Date(date);
  return `${d.toLocaleDateString()} ${d.toLocaleTimeString()}`;
});

Axios.interceptors.request.use((config) => {
  if (Vue.prototype.$auth.isAuthenticated) {
    config.headers.Authorization = `Bearer ${Vue.prototype.$auth.accessToken}`;
  } else {
    console.log('no auth');
  }
  return config;
}, error => Promise.reject(error));

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app');
