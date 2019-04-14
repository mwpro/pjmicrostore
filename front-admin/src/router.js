import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import ProductsList from './views/ProductsList.vue';
import ProductsEdit from './views/ProductsEdit.vue';
import OrdersList from './views/OrdersList.vue';

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
      component: ProductsList
    },
    {
      path: '/products/new',
      name: 'newProduct',      
      component: ProductsEdit
    },    
    {
      path: '/orders',
      name: 'orders',      
      component: OrdersList
    },
  ],
});
