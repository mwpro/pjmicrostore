import axios from 'axios';
import Vue from 'vue';

export default {
  namespaced: true,
  state: {
    productsList: [

    ],
  },
  mutations: {
    getProducts(state, products) {
      state.productsList = products;
    },
  },
  actions: {
    getProductsAction({ commit }) {
      return axios
        .get('/api/products')
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let products = response.data;
          if (typeof products !== 'object') {
            products = [];
          }

          commit('getProducts', products);
          return products;
        });
      // .catch(captains.error)
    },
  },
};
