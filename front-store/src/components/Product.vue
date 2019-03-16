<template>
    <div>
        <h2>{{ product.name }}</h2>
        <img src="https://picsum.photos/300/300?image=0" alt="Product name" />
        <p>{{ product.price | currency }}</p>
        <AddToCart :product="product" />
        <p>
            {{product.description}}
        </p>
        <table>
            <tr v-for="attribute in product.attributes" v-bind:key="attribute.attributeId">
                <td>{{ attribute.attribute.name }}</td>
                <td>{{ attribute.value }}</td>
            </tr>
        </table>
    </div>
</template>

<script>
import AddToCart from './AddToCart.vue';

export default {
  components: { AddToCart },
  props: {
    productId: String, // TODO number
  }, 
  computed: {
    product() {
      return this.$store.state.products.product;
    }
  },
  created() {
    this.$store.dispatch("products/getProductAction", this.productId);
  }
}
</script>

<style>

</style>
