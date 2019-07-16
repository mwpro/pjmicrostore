import axios from 'axios';
import Vue from 'vue';
import Qs from 'qs';

export default {
  namespaced: true,
  state: {
    cart: {
      cartItems: [],
      cartAccessToken: localStorage.getItem('cart_token'),
      value: 0,
      numberOfItems: 0,
    },
    paymentMethods: [],
  },
  mutations: {
    updateCart(state, cart) {
      state.cart = cart;
      localStorage.setItem('cart_token', cart.cartAccessToken);
    },
    updatePaymentMethods(state, paymentMethods) {
      state.paymentMethods = paymentMethods;
    },
  },
  actions: {
    getCartAction({ commit, state }) {
      return axios
        .get('/api/cart', {
          params: { cartToken: state.cart.cartAccessToken },
          paramsSerializer(params) {
            return Qs.stringify(params);
          },
        }) // cartAccessToken
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
    addProductToCartAction({ commit, state }, productInfo) {
      return axios({
        method: 'post',
        url: `/api/cart/products/${productInfo.productId}`,
        data: { quantity: productInfo.quantity },
        params: { cartToken: state.cart.cartAccessToken },
        paramsSerializer(params) {
          return Qs.stringify(params);
        },
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
    removeFromCartAction({ commit, state }, productId) {
      return axios({
        method: 'delete',
        url: `/api/cart/products/${productId}`,
        params: { cartToken: state.cart.cartAccessToken },
        paramsSerializer(params) {
          return Qs.stringify(params);
        },
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
    updateItemAction({ commit, state }, productInfo) {
      return axios({
        method: 'put',
        url: `/api/cart/products/${productInfo.productId}`,
        data: { quantity: productInfo.quantity },
        params: { cartToken: state.cart.cartAccessToken },
        paramsSerializer(params) {
          return Qs.stringify(params);
        },
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
    placeOrder({ commit, state }, checkoutDetails) {
      checkoutDetails.cartAccessToken = state.cart.cartAccessToken;
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
            cartAccessToken: '',
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
