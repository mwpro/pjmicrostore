import axios from 'axios';
import Vue from 'vue';
import Qs from 'qs';

export default {
  namespaced: true,
  state: {
    productsList: [

    ],
    productsCount: 0,
    categoriesList: [

    ],
    attributesList: [

    ],
    product: {

    },
    photos: [],
  },
  mutations: {
    getProducts(state, products) {
      state.productsList = products;
    },
    getProductsCount(state, productsCount) {
      state.productsCount = productsCount;
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
    getPhotos(state, photos) {
      state.photos = photos;
    },
  },
  actions: {
    getProductsAction({ commit }, query) {
      return axios
        .get('/api/products', {
          params: query,
          paramsSerializer(params) {
            return Qs.stringify(params);
          },
        })
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          const products = response.data;
          /* hangs on Mac?
          if (typeof products !== 'object') {
            products = [];
          } */

          commit('getProducts', products.products);
          commit('getProductsCount', products.productsCount);
          return products;
        });
      // TODO .catch(captains.error)
    },
    getPhotosAction({ commit }, productId) {
      return axios
        .get(`/api/products/${productId}/photos`)
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          const photos = response.data;
          commit('getPhotos', photos);
          return photos;
        });
      // TODO .catch(captains.error)
    },
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
          const categories = response.data;
          /* hangs on Mac?
          if (typeof categories !== 'object') {
            categories = [];
          } */

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
          const attributes = response.data;
          /* hangs on Mac?
          if (typeof attributes !== 'object') {
            attributes = [];
          } */

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
    updateProductAction({ }, product) {
      return axios
        .put(`/api/products/${product.productId}`, product)
        .then((response) => {
          if (response.status !== 201) throw Error(response.message);

          return response.data;
        });
      // TODO .catch(captains.error)
    },
  },
};
