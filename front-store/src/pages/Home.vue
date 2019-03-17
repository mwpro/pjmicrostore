<template>
  <div class="columns">     
    <div class="column is-one-quarter">
        <CategoriesList />
    </div>
    <div class="column">
      <div class="columns is-multiline">
        <ProductBox v-for="product in products" v-bind:key="product.id" v-bind:product="product" />
      </div>
    </div>
  </div>   
</template>

<script>
import ProductBox from './../components/ProductBox.vue';
import CategoriesList from './../components/CategoriesList.vue';

export default {
  name: 'HelloWorld',
  components: { ProductBox, CategoriesList },
  props: {
    msg: String,
  },  
  computed: {
    products() {
      return this.$store.state.products.productsList;
    },    
  },
  created() {
    this.$store.dispatch(
      'products/getProductsAction'
    );
  },
};
</script>

<style scoped>
</style>
