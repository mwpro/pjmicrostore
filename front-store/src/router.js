import Vue from 'vue';
import Router from 'vue-router';

import Home from './pages/Home.vue';
import Product from './pages/Product.vue';
import Cart from './pages/Cart.vue';
import AboutUs from './pages/AboutUs.vue';
import OrderPlaced from './pages/OrderPlaced.vue';
import PaymentRedirect from './pages/PaymentRedirect.vue';
import PaymentMock from './pages/PaymentMock.vue';
import Callback from './pages/Callback.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
    },
    {
      path: '/callback',
      name: 'callback',
      component: Callback,
      meta: { layout: 'minimal' }
    },
    {
      path: '/products/:productId',
      name: 'product',
      props: true,
      component: Product,
    },
    {
      path: '/cart',
      name: 'cart',
      component: Cart,
      meta: { layout: 'minimal' }
    },
    {
      path: '/aboutus',
      name: 'aboutus',
      component: AboutUs
    },
    {
      path: '/orderPlaced',
      name: 'orderPlaced',
      component: OrderPlaced,
    },
    {
      path: '/paymentRedirect',
      name: 'paymentRedirect',
      props: true,
      component: PaymentRedirect,
      meta: { layout: 'minimal' }
    },
    {
      path: '/payment-mock/:paymentMockId',
      name: 'paymentMock',
      props: true,
      component: PaymentMock,
      meta: { layout: 'minimal' } // tood or none?
    },
  ],
});
