<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <div class="products">
      <ProductBox v-for="product in products" v-bind:key="product.id" v-bind:product="product" />
    </div>
  </div>
</template>

<script>
import ProductBox from './ProductBox.vue';

export default {
  name: 'HelloWorld',
  components: { ProductBox },
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

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
.products {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}
</style>
