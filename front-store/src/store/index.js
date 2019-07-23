import Vue from 'vue';
import Vuex from 'vuex';

import productsModule from './products';
import cartModule from './cart';
import usersModule from './user';
import ordersModule from './orders';

Vue.use(Vuex);

export default new Vuex.Store({
  strict: true,
  modules: {
    products: productsModule,
    cart: cartModule,
    user: usersModule,
    orders: ordersModule,
  },
  state: {

  },
  mutations: {

  },
  actions: {

  },
});
