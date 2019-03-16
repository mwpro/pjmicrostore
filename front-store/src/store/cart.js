import axios from 'axios';
import Vue from 'vue';

export default {
  namespaced: true,
  state: {
    cart: {
      cartItems: [],
      value: 0
    },
  },
  mutations: {
    updateCart(state, cart) {
      state.cart = cart;
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
          quantity: productInfo.quantity
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
          quantity: productInfo.quantity
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
  },
};
