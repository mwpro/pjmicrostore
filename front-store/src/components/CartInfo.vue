<template>
    <div>
        <p v-if="cart.numberOfItems === 0"><router-link :to="{ name: 'cart' }">Cart is empty</router-link></p>
        <p v-else>
            <router-link :to="{ name: 'cart' }">Koszyk:</router-link> 
            {{ cart.numberOfItems }} produktów o wartości {{ cart.total | currency }}
        </p>
    </div>
</template>

<script>
export default {
  computed: {
    cart() {
      return this.$store.state.cart.cart;
    },    
  },
}
</script>

<style>

</style>
