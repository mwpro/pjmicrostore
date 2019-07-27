import axios from 'axios';
import Vue from 'vue';

export default {
  namespaced: true,
  state: {
    ordersList: [

    ],
    orderDetails: {
    },
  },
  mutations: {
    getOrders(state, orders) {
      state.ordersList = orders;
    },
    getOrder(state, order) {
      state.orderDetails = order;
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

    getOrderAction({ commit }, orderId) {
      return axios
        .get(`/api/orders/${orderId}`)
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          const order = response.data;
          /* hangs on Mac?
          if (typeof order !== 'object') {
            order = [];
          } */

          commit('getOrder', order);
          return order;
        });
      // TODO .catch(captains.error)
    },
  },
};
