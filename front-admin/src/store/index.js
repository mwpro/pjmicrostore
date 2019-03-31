import Vue from 'vue';
import Vuex from 'vuex';

import productsModule from './products';

Vue.use(Vuex);

export default new Vuex.Store({
  strict: true,
  modules: {
    products: productsModule
  },
  state: {

  },
  mutations: {

  },
  actions: {

  },
});
