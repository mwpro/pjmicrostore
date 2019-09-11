<template>
    <div>
        <header class="blog-header py-3">
        <div class="row flex-nowrap justify-content-between align-items-center">
          <div class="col-4 pt-1">
            <router-link to="/aboutus" class="text-muted">O nas</router-link>
          </div>
          <div class="col-4 text-center">
            <router-link to="/" class="blog-header-logo text-dark">
                pjmicrostore
            </router-link>
          </div>
          <div class="col-4 d-flex justify-content-end align-items-center">
            <a class="btn btn-sm btn-outline-secondary" @click="login()" v-if="!isAuthenticated">Zaloguj się</a>
            <a class="btn btn-sm btn-outline-secondary" @click="logout()" v-if="isAuthenticated">Wyloguj</a>
            <router-link to="/account" class="btn btn-sm btn-outline-secondary">Konto</router-link>
            <CartInfo />
          </div>
        </div>
      </header>

      <div class="nav-scroller py-1 mb-2">
        <nav class="nav d-flex justify-content-between">
          <router-link
            v-for="category in categories"
            v-bind:key="category.id"
            class="p-2 text-muted"
            :to="'/category/'+category.id"
          >
            {{ category.name }}
          </router-link>
        </nav>
      </div>

    </div>
</template>

<script>
import axios from 'axios';
import CartInfo from './CartInfo.vue';

export default {
  components: { CartInfo },
  created() {
    this.$store.dispatch('cart/getCartAction');
    // already done in categoriesList, would be nice to have some "lock"
    this.$store.dispatch('products/getCategoriesAction');
  },
  computed: {
    cart() {
      return this.$store.state.cart.cart;
    },
    categories() {
      return this.$store.state.products.categoriesList;
    },
    isAuthenticated() {
      return this.$auth.isAuthenticated;
    },
  },
  methods: {
    login() {
      this.$auth.login();
    },
    logout() {
      this.$auth.logout();
    },
  },
};
</script>

<style>
  .router-link-active {
    font-weight: bold;
  }
</style>
