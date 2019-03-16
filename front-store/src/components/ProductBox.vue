<template>
    <div class="product-miniature">
        <h4>{{ product.name }}</h4>
        <img src="https://picsum.photos/300/300?image=0" alt="Product name" />
        <p>{{ product.price }}</p>
        <button>Dodaj do koszyka</button>
    </div>
</template>

<script>
export default {
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
