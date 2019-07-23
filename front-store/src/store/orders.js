import axios from 'axios';
import Vue from 'vue';

export default {
  namespaced: true,
  state: {
    ordersList: [

    ],
  },
  mutations: {
    getOrders(state, orders) {
      state.ordersList = orders;
    },
  },
  actions: {
    getOrdersAction({ commit }) {
      return axios
        .get('/api/orders')
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          const orders = response.data;
          /* hangs on Mac?
          if (typeof orders !== 'object') {
            orders = [];
          } */

          commit('getOrders', orders);
          return orders;
        });
      // TODO .catch(captains.error)
    },
  },
};
