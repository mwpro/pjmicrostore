import Vue from 'vue';
import Vuex from 'vuex';

import productsModule from './products';
import ordersModule from './orders';


Vue.use(Vuex);

export default new Vuex.Store({
  strict: true,
  modules: {
    products: productsModule,
    orders: ordersModule
  },
  state: {

  },
  mutations: {

  },
  actions: {

  },
});
