import axios from 'axios';
import Vue from 'vue';
import Qs from 'qs';

export default {
  namespaced: true,
  state: {
    productsList: [

    ],
    categoriesList: [

    ],
    product: {

    },
    searchAttributes: [

    ]
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
    getSearchAttributes(state, searchAttributes) {
      state.searchAttributes = searchAttributes;
    },
  },
  actions: {
    getProductsAction({ commit }) {
      return axios
        .get('/api/search')
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let products = response.data;
          if (typeof products !== 'object') {
            products = [];
          }

          commit('getProducts', products.documents);
          commit('getSearchAttributes', products.stringAttributes);
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
    searchProductsAction({ commit }, searchTerms) {
      var urlReadySearchTerms = [];
      searchTerms.forEach(term => {
        if (urlReadySearchTerms[term.attribute] == null) {
          urlReadySearchTerms[term.attribute] = [];
        }
        urlReadySearchTerms[term.attribute].push(term.value);
        
      });
      console.log(urlReadySearchTerms);
      console.log(Qs.stringify(urlReadySearchTerms));
      return axios
        .get('/api/search', {
          params: {
            stringAttr: urlReadySearchTerms
          },
          paramsSerializer: function (params) {
            return Qs.stringify(params)
          }
        })
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let products = response.data;
          if (typeof products !== 'object') {
            products = [];
          }

          commit('getProducts', products.documents);
          commit('getSearchAttributes', products.stringAttributes);
          return products;
        });
      // TODO .catch(captains.error)
    }
  },
};
