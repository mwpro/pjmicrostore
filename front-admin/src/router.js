import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import ProductsList from './views/ProductsList.vue';
import ProductsEdit from './views/ProductsEdit.vue';
import OrdersList from './views/OrdersList.vue';
import OrderDetails from './views/OrderDetails.vue';
import Callback from './views/Callback.vue';
import SilentRenew from './views/SilentRenew.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  linkActiveClass: 'active',
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
      // meta: { layout: 'minimal' },
    },
    {
      path: '/silentRenew',
      name: 'silentRenew',
      component: SilentRenew,
      // meta: { layout: 'minimal' },
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/About.vue'),
    },
    {
      path: '/products',
      name: 'products',
      component: ProductsList,
    },
    {
      path: '/products/new',
      name: 'newProduct',
      component: ProductsEdit,
    },
    {
      path: '/products/:productId',
      name: 'editProduct',
      component: ProductsEdit,
      props: true,
    },
    {
      path: '/orders',
      name: 'orders',
      component: OrdersList,
    },
    {
      path: '/orders/:orderId',
      name: 'orderDetails',
      component: OrderDetails,
      props: true,
    },
  ],
});
