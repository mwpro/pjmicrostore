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
    checkoutAsGuest: false,
    paymentMethods: null,
    deliveryMethods: null,
    selectedDeliveryMethod: null,
    selectedPaymentMethod: null,
    email: null,
    phone: null,
    shippingDetailsAddress: null,
    shippingDetailsCity: null,
    shippingDetailsFirstName: null,
    shippingDetailsLastName: null,
    shippingDetailsStreet: null,
    shippingDetailsZip: null,
    billingDetailsAreDifferentFromShipping: false,
    billingDetailsAddress: null,
    billingDetailsCity: null,
    billingDetailsFirstName: null,
    billingDetailsLastName: null,
    billingDetailsStreet: null,
    billingDetailsZip: null,
  },
  mutations: {
    updateCart(state, cart) {
      state.cart = cart;
      localStorage.setItem('cart_token', cart.cartAccessToken);
    },
    setDeliveryMethod(state, deliveryMethod) {
      state.selectedDeliveryMethod = deliveryMethod;
    },
    setCheckoutAsGuest(state) {
      state.checkoutAsGuest = true;
    },
    setPaymentMethod(state, paymentMethod) {
      state.selectedPaymentMethod = paymentMethod;
    },
    updatePaymentMethods(state, paymentMethods) {
      state.paymentMethods = paymentMethods;
    },
    updateDeliveryMethods(state, deliveryMethods) {
      state.deliveryMethods = deliveryMethods;
    },
    updateEmail(state, email) {
      state.email = email;
    },
    updatePhone(state, phone) {
      state.phone = phone;
    },
    updateShippingDetailsAddress(state, shippingDetailsAddress) {
      state.shippingDetailsAddress = shippingDetailsAddress;
    },
    updateShippingDetailsCity(state, shippingDetailsCity) {
      state.shippingDetailsCity = shippingDetailsCity;
    },
    updateShippingDetailsFirstName(state, shippingDetailsFirstName) {
      state.shippingDetailsFirstName = shippingDetailsFirstName;
    },
    updateShippingDetailsLastName(state, shippingDetailsLastName) {
      state.shippingDetailsLastName = shippingDetailsLastName;
    },
    updateShippingDetailsStreet(state, shippingDetailsStreet) {
      state.shippingDetailsStreet = shippingDetailsStreet;
    },
    updateShippingDetailsZip(state, shippingDetailsZip) {
      state.shippingDetailsZip = shippingDetailsZip;
    },
    updateBillingDetailsAreDifferentFromShipping(state, billingDetailsAreDifferentFromShipping) {
      state.billingDetailsAreDifferentFromShipping = billingDetailsAreDifferentFromShipping;
    },
    updateBillingDetailsAddress(state, billingDetailsAddress) {
      state.billingDetailsAddress = billingDetailsAddress;
    },
    updateBillingDetailsCity(state, billingDetailsCity) {
      state.billingDetailsCity = billingDetailsCity;
    },
    updateBillingDetailsFirstName(state, billingDetailsFirstName) {
      state.billingDetailsFirstName = billingDetailsFirstName;
    },
    updateBillingDetailsLastName(state, billingDetailsLastName) {
      state.billingDetailsLastName = billingDetailsLastName;
    },
    updateBillingDetailsStreet(state, billingDetailsStreet) {
      state.billingDetailsStreet = billingDetailsStreet;
    },
    updateBillingDetailsZip(state, billingDetailsZip) {
      state.billingDetailsZip = billingDetailsZip;
    },
    resetCheckout(state) {
      state.checkoutAsGuest = false;
      state.selectedDeliveryMethod = null;
      state.selectedPaymentMethod = null;
      state.paymentMethods = null;
      state.paymentMethods = null;

      state.email = null;
      state.phone = null;
      state.shippingDetailsAddress = null;
      state.shippingDetailsCity = null;
      state.shippingDetailsFirstName = null;
      state.shippingDetailsLastName = null;
      state.shippingDetailsStreet = null;
      state.shippingDetailsZip = null;
      state.billingDetailsAreDifferentFromShipping = false;
      state.billingDetailsAddress = null;
      state.billingDetailsCity = null;
      state.billingDetailsFirstName = null;
      state.billingDetailsLastName = null;
      state.billingDetailsStreet = null;
      state.billingDetailsZip = null;
    },
  },
  getters: {
    isCartEmpty: state => state.cart.cartItems.length === 0,
    cartTotal: state => state.cart.total
      + ((state.selectedDeliveryMethod) ? state.selectedDeliveryMethod.price : 0)
      + ((state.selectedPaymentMethod) ? state.selectedPaymentMethod.fee : 0),
    placeOrderModel: state => ({
      cartAccessToken: state.cart.cartAccessToken,
      email: state.email,
      phone: state.phone,
      paymentMethod: ((state.selectedPaymentMethod) ? state.selectedPaymentMethod.name : ''),
      shippingMethod: ((state.selectedDeliveryMethod) ? state.selectedDeliveryMethod.name : ''),
      shippingDetails: {
        firstName: state.shippingDetailsFirstName,
        lastName: state.shippingDetailsLastName,
        address: state.shippingDetailsAddress,
        city: state.shippingDetailsCity,
        zip: state.shippingDetailsZip,
      },
      billingDetails: state.billingDetailsAreDifferentFromShipping
        ? {
          firstName: state.billingDetailsFirstName,
          lastName: state.billingDetailsLastName,
          address: state.billingDetailsAddress,
          city: state.billingDetailsCity,
          zip: state.billingDetailsZip,
        } : {
          firstName: state.shippingDetailsFirstName,
          lastName: state.shippingDetailsLastName,
          address: state.shippingDetailsAddress,
          city: state.shippingDetailsCity,
          zip: state.shippingDetailsZip,
        },
    }),
  },
  actions: {
    setDeliveryMethodAction({ commit, dispatch }, deliveryMethod) {
      dispatch('getPaymentMethodsAction', deliveryMethod.name);
      commit('setPaymentMethod', null);
      commit('setDeliveryMethod', deliveryMethod);
    },
    setPaymentMethodAction({ commit }, paymentMethod) {
      commit('setPaymentMethod', paymentMethod);
    },
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
    placeOrder({ commit, getters }) {
      return axios
        .post('/api/orders', getters.placeOrderModel)
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
          commit('resetCheckout');
          return order;
        });
      // TODO .catch(captains.error)
    },
    getDeliveryMethodsAction({ commit }) {
      return axios
        .get('/api/shipping')
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let deliveryMethods = response.data;
          if (typeof deliveryMethods !== 'object') {
            deliveryMethods = []; // todo ???
          }

          commit('updateDeliveryMethods', deliveryMethods);
          return deliveryMethods;
        });
      // TODO .catch(captains.error)
    },
    getPaymentMethodsAction({ commit }, deliveryMethod) {
      return axios({
        method: 'get',
        url: '/api/payments/methods',
        params: { deliveryMethod },
        paramsSerializer(params) {
          return Qs.stringify(params);
        },
      }).then((response) => {
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
