import Vue from 'vue';
import Vuex from 'vuex';

import productsModule from './products';
import cartModule from './cart';
import usersModule from './user';

Vue.use(Vuex);

export default new Vuex.Store({
  strict: true,
  modules: {
    products: productsModule,
    cart: cartModule,
    user: usersModule,
  },
  state: {

  },
  mutations: {

  },
  actions: {

  },
});
