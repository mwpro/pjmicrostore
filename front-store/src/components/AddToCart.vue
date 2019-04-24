<template>
    <button class="btn btn-secondary btn-block" @click="addToCart()">Dodaj do koszyka</button>
</template>

<script>
export default {
  props: {
    product: Object,
  },
  methods: {
    addToCart() {
      this.$store.dispatch('cart/addProductToCartAction', {
        productId: this.product.id,
        quantity: 1,
      });
    },
  },
};
</script>

<style>

</style>
