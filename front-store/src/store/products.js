import axios from 'axios';
import Vue from 'vue';
import Qs from 'qs';

export default {
  namespaced: true,
  state: {
    category: null,
    filters: [],
    productsList: [

    ],
    categoriesList: [

    ],
    product: {

    },
    searchAttributes: [

    ],
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
    setCategory(state, category) {
      state.category = category;
    },
    setFilters(state, filters) {
      state.filters = filters;
    },
  },
  actions: {
    getProductAction({ commit }, productId) {
      return axios
        .get(`/api/products/${productId}`)
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          const product = response.data;
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
    // todo separate search store
    resetSearchTermsActions({ dispatch }) {
      dispatch('setCateogryAction', null);
      dispatch('setFiltersAction', []);
    },
    setFiltersAction({ commit }, filters) {
      commit('setFilters', filters);
    },
    setCateogryAction({ commit }, category) {
      commit('setCategory', category);
    },
    searchProductsAction({ commit, state }) {
      const searchParams = {};
      if (state.filters != null) {
        searchParams.stringAttr = [];
        state.filters.forEach((term) => {
          if (searchParams.stringAttr[term.attribute] == null) {
            searchParams.stringAttr[term.attribute] = [];
          }
          searchParams.stringAttr[term.attribute].push(term.value);
        });
      }
      if (state.category != null) {
        searchParams.categoryId = state.category;
      }
      return axios
        .get('/api/search', {
          params: searchParams,
          paramsSerializer(params) {
            return Qs.stringify(params);
          },
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
    },
  },
};
