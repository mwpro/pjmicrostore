<template>
    <div>
        <p v-if="cart.cartItems.length === 0">Cart is empty</p>
        <p v-else>
            Koszyk: {{ cart.cartItems.length }} produktów o wartości {{ cart.total }}
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
//   created() {
//     this.$store.dispatch(
//       'products/getProductsAction'
//     );
//   },
}
</script>

<style>

</style>
