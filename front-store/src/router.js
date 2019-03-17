import Vue from 'vue';
import Router from 'vue-router';

import Home from './pages/Home.vue';
import Product from './pages/Product.vue';
import Cart from './pages/Cart.vue';
import OrderPlaced from './pages/OrderPlaced.vue';

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
      path: '/orderPlaced',
      name: 'orderPlaced',
      props: true,
      component: OrderPlaced,
    },
  ],
});
