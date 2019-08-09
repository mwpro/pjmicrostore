import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import Axios from 'axios';
import App from './App.vue';
import router from './router';
import store from './store';
import AuthService from './auth/AuthService';
import dictionaries from './assets/Dictionaries';

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
Vue.filter('dictionaryValue', (value, dictionaryName) => dictionaries.translate(dictionaryName, value));

Axios.defaults.baseURL = process.env.VUE_APP_API_URL;
Axios.interceptors.request.use((config) => {
  if (Vue.prototype.$auth.isAuthenticated) {
    config.headers.Authorization = `Bearer ${Vue.prototype.$auth.accessToken}`;
  }
  return config;
}, error => Promise.reject(error));

Vue.prototype.$auth.getUser().then((x) => {
  new Vue({
    router,
    store,
    render: h => h(App),
  }).$mount('#app');
});
