<template>
    <div>
        <h2>{{ product.name }}</h2>
        <img v-if="photos[0]" :src="photos[0].originalUrl" alt="Product name" style="width: 500px; height: auto" />
        <big>{{ product.price | currency }}</big>
        <AddToCart :product="product" />
        <p>
            {{product.description}}
        </p>
        <h3>Specyfikacja</h3>
        <table>
            <tr v-for="attribute in product.attributes" v-bind:key="attribute.attributeId">
                <td>{{ attribute.name }}</td>
                <td>{{ attribute.value }}</td>
            </tr>
        </table>
    </div>
</template>

<script>
import AddToCart from '../components/AddToCart.vue';

export default {
  components: { AddToCart },
  props: {
    productId: String, // TODO number
  },
  computed: {
    product() {
      return this.$store.state.products.product;
    },
    photos() {
      return this.$store.state.products.photos;
    },
  },
  created() {
    this.$store.dispatch('products/getProductAction', this.productId);
    this.$store.dispatch('products/getPhotosAction', this.productId);
  },
};
</script>

<style>

</style>
