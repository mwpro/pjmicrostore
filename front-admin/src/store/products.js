import axios from 'axios';
import Vue from 'vue';

export default {
  namespaced: true,
  state: {
    productsList: [

    ],
    categoriesList: [

    ],    
    attributesList: [

    ],
    product: {

    }
  },
  mutations: {
    getProducts(state, products) {
      state.productsList = products;
    },    
    getProduct(state, product) {
      state.product = product;
    },
    getCategories(state, categories) {
      state.categoriesList = categories;
    },    
    getAttributes(state, attributes) {
      state.attributesList = attributes;
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
      // TODO .catch(captains.error)
    },
    getProductAction({ commit }, productId) {
      return axios
        .get(`/api/products/${productId}`)
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let product = response.data;
          commit('getProduct', product);
          return product;
        });
      // TODO .catch(captains.error)
    },
    getCategoriesAction({ commit }) {
      return axios
        .get('/api/categories')
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let categories = response.data;
          if (typeof categories !== 'object') {
            categories = [];
          }

          commit('getCategories', categories);
          return categories;
        });
      // TODO .catch(captains.error)
    },
    getAttributesAction({ commit }) {
      return axios
        .get('/api/attributes')
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let attributes = response.data;
          if (typeof attributes !== 'object') {
            attributes = [];
          }

          commit('getAttributes', attributes);
          return attributes;
        });
      // TODO .catch(captains.error)
    },
    saveProductAction({ }, product) {
      return axios
        .post('/api/products', product)
        .then((response) => {
          if (response.status !== 201) throw Error(response.message);

          return response.data;
        });
      // TODO .catch(captains.error)
    },
  },
};
