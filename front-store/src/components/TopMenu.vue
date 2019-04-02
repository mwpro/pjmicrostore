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
            <a class="text-muted" href="#">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="20"
                height="20"
                fill="none"
                stroke="currentColor"
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                class="mx-3"
                role="img"
                viewBox="0 0 24 24"
                focusable="false"
              >
                <title>Search</title>
                <circle cx="10.5" cy="10.5" r="7.5"></circle>
                <path d="M21 21l-5.2-5.2"></path>
              </svg>
            </a>
            <a class="btn btn-sm btn-outline-secondary" href="#">Sign up</a>            
            <CartInfo />
          </div>
        </div>
      </header>

      <div class="nav-scroller py-1 mb-2">
        <nav class="nav d-flex justify-content-between">
          <a v-for="category in categories" v-bind:key="category.id" class="p-2 text-muted" href="#" >
            {{ category.name }}
          </a>
          <a class="p-2 text-muted" href="#">World</a>
          <a class="p-2 text-muted" href="#">U.S.</a>
          <a class="p-2 text-muted" href="#">Technology</a>
          <a class="p-2 text-muted" href="#">Design</a>
        </nav>
      </div>

    </div>
</template>

<script>
import CartInfo from './CartInfo.vue';
export default {
  components: { CartInfo },
  created() {
    this.$store.dispatch("cart/getCartAction");
    // already done in categoriesList, would be nice to have some "lock" this.$store.dispatch("products/getCategoriesAction");
  },
  computed: {
    cart() {
      return this.$store.state.cart.cart;
    },    
    categories() {
      return this.$store.state.products.categoriesList;
    }
  },
}
</script>

<style>

</style>
