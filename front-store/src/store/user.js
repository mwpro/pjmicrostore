import axios from 'axios';
import Vue from 'vue';
import Qs from 'qs';

export default {
  namespaced: true,
  state: {
    user: null,
  },
  mutations: {
    getUser(state, user) {
      state.user = user;
    },
  },
  actions: {
    getUserAction({ commit }) {
      return axios
        .get('/api/users/me')
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          const product = response.data;
          commit('getUser', product);
          return product;
        });
      // TODO .catch(captains.error)
    },
  },
};
