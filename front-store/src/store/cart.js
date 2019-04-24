import axios from 'axios';
import Vue from 'vue';

export default {
  namespaced: true,
  state: {
    cart: {
      cartItems: [],
      value: 0,
      numberOfItems: 0,
    },
    paymentMethods: [],
  },
  mutations: {
    updateCart(state, cart) {
      state.cart = cart;
    },
    updatePaymentMethods(state, paymentMethods) {
      state.paymentMethods = paymentMethods;
    },
  },
  actions: {
    getCartAction({ commit }) {
      return axios
        .get('/api/cart')
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let cart = response.data;
          if (typeof cart !== 'object') {
            cart = {}; // todo ???
          }

          commit('updateCart', cart);
          return cart;
        });
      // TODO .catch(captains.error)
    },
    addProductToCartAction({ commit }, productInfo) {
      return axios
        .post(`/api/cart/products/${productInfo.productId}`, {
          quantity: productInfo.quantity,
        })
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let cart = response.data;
          if (typeof cart !== 'object') {
            cart = {}; // todo ???
          }

          commit('updateCart', cart);
          return cart;
        });
      // TODO .catch(captains.error)
    },
    removeFromCartAction({ commit }, productId) {
      return axios
        .delete(`/api/cart/products/${productId}`)
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let cart = response.data;
          if (typeof cart !== 'object') {
            cart = {}; // todo ???
          }

          commit('updateCart', cart);
          return cart;
        });
      // TODO .catch(captains.error)
    },
    updateItemAction({ commit }, productInfo) {
      return axios
        .put(`/api/cart/products/${productInfo.productId}`, {
          quantity: productInfo.quantity,
        })
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let cart = response.data;
          if (typeof cart !== 'object') {
            cart = {}; // todo ???
          }

          commit('updateCart', cart);
          return cart;
        });
      // TODO .catch(captains.error)
    },
    placeOrder({ commit }, checkoutDetails) {
      return axios
        .post('/api/orders', checkoutDetails)
        .then((response) => {
          if (response.status !== 201) throw Error(response.message);
          let order = response.data;
          if (typeof order !== 'object') {
            order = {}; // todo ???
          }

          commit('updateCart', {
            cartItems: [],
            value: 0,
            numberOfItems: 0,
          }); // todo actual cart cleaning?
          return order;
        });
      // TODO .catch(captains.error)
    },
    getPaymentMethodsAction({ commit }) {
      return axios
        .get('/api/payments/methods')
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let paymentMethods = response.data;
          if (typeof paymentMethods !== 'object') {
            paymentMethods = []; // todo ???
          }

          commit('updatePaymentMethods', paymentMethods);
          return paymentMethods;
        });
      // TODO .catch(captains.error)
    },
    getPaymentDetailsAction({ commit }, paymentCheckUrl) {
      return axios
        .get(paymentCheckUrl) // doesnt look safe
        .then((response) => {
          if (response.status !== 200) return ''; // todo handle other statuses
          let payment = response.data;
          if (typeof payment !== 'object') {
            payment = {}; // todo ???
          }
          return payment.paymentUrl;
        });
      // TODO .catch(captains.error)
    },
  },
};
