<template>
    <button @click="addToCart()">Dodaj do koszyka</button>
</template>

<script>
export default {
    props: {
        product: Object,
    }, 
    methods: {
        addToCart() {
            console.log(`${this.product.name} added to cart`);
            this.$store.dispatch("cart/addProductToCartAction", {
                productId: this.product.id,
                quantity: 1
            });
        }
    }
}
</script>

<style>

</style>
