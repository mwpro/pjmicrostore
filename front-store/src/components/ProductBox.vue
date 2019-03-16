<template>
    <div class="product-miniature">
        <h4><router-link :to="{ name: 'product', params: { productId: product.id }}">{{ product.name }}</router-link></h4>
        <img src="https://picsum.photos/300/300?image=0" alt="Product name" />
        <p>{{ product.price }}</p>
        <AddToCart :product="product" />
    </div>
</template>

<script>
import AddToCart from './AddToCart.vue';

export default {
  components: { AddToCart },
  props: {
    product: Object,
  }, 
}
</script>

<style scoped>
    .product-miniature {
        padding: 10px;
    }
</style>
