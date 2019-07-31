<template>
  <div class="col-12 col-md-4">
    <div class="card">
      <img class="card-img-top" :src="product.searchThumbnailUri" alt="Card image cap">
      <div class="card-body">
        <strong class="d-inline-block mb-2 text-success">Design</strong>
        <h4 class="card-title">
          <router-link
            :to="{ name: 'product', params: { productId: product.id }}"
          >{{ product.name }}</router-link>
        </h4>
        <div class="row">
          <div class="col-md-12">
            <p class="btn btn-block">{{ product.price | currency }}</p>
          </div>
          <div class="col">
            <AddToCart :product="product"/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import AddToCart from './AddToCart.vue';

export default {
  components: { AddToCart },
  props: {
    product: Object,
  },
};
</script>

<style scoped>
</style>
