<template>
  <div>
    <div class="jumbotron p-4 p-md-5 text-white rounded bg-dark">
      <div class="col-md-8 px-0">
        <h1 class="display-4 font-italic">PJMicroStore</h1>
        <p
          class="lead my-3"
        >Sklep internetowy oparty o mikroserwisy na platformie Kubernetes</p>
      </div>
    </div>

    <main role="main" class="container">
      <div class="row">
        <div class="col blog-main">
          <div class="row">
            <ProductBox v-for="product in products" v-bind:key="product.id" v-bind:product="product"/>
          </div>
        </div>

        <!-- /.blog-sidebar -->
      </div>
      <!-- /.row -->
    </main>

  </div>
</template>

<script>
import ProductBox from '../components/ProductBox.vue';
import CategoriesList from '../components/CategoriesList.vue';
import SearchFilters from '../components/SearchFilters.vue';


export default {
  name: 'Home',
  components: { ProductBox, CategoriesList, SearchFilters },
  computed: {
    products() {
      return this.$store.state.products.productsList;
    },
  },
  created() {
    this.$store.dispatch('products/resetSearchTermsActions')
      .then(() => this.$store.dispatch('products/searchProductsAction'));
  },
};
</script>

<style scoped>
</style>
