<template>
    <nav class="navbar" role="navigation" aria-label="main navigation">
        <div class="navbar-brand">

            <a role="button" class="navbar-burger burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            </a>
        </div>

        <div id="navbarBasicExample" class="navbar-menu">
            <div class="navbar-start">
            <router-link to="/" class="navbar-item">
                Home
            </router-link>

            <a class="navbar-item">
                Documentation
            </a>

            <div class="navbar-item has-dropdown is-hoverable">
                <a class="navbar-link">
                More
                </a>

                <div class="navbar-dropdown">
                <a class="navbar-item">
                    About
                </a>
                <a class="navbar-item">
                    Jobs
                </a>
                <a class="navbar-item">
                    Contact
                </a>
                <hr class="navbar-divider">
                <a class="navbar-item">
                    Report an issue
                </a>
                </div>
            </div>
            </div>

            <div class="navbar-end">
            <div class="navbar-item">
                <div class="buttons">
                <CartInfo />
                <a class="button is-light">
                    <strong>Sign up</strong>
                </a>
                </div>
            </div>
            </div>
        </div>
    </nav>
</template>

<script>
import CartInfo from './CartInfo.vue';
export default {
  components: { CartInfo },
  created() {
    this.$store.dispatch("cart/getCartAction");
  },
  computed: {
    cart() {
      return this.$store.state.cart.cart;
    }
  },
}
</script>

<style>

</style>
