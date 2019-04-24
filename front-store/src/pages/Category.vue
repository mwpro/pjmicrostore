<template>
  <div>
    <main role="main" class="container">
      <div class="row">
        <aside class="col-md-3 blog-sidebar">
          <CategoriesList />
          <SearchFilters />
          <div class="p-4 mb-3 bg-light rounded">
            <h4 class="font-italic">About</h4>
            <p class="mb-0">
              Etiam porta
              <em>sem malesuada magna</em> mollis euismod. Cras mattis consectetur purus sit amet fermentum. Aenean lacinia bibendum nulla sed consectetur.
            </p>
          </div>

          <div class="p-4">
            <h4 class="font-italic">Elsewhere</h4>
            <ol class="list-unstyled">
              <li>
                <a href="#">GitHub</a>
              </li>
              <li>
                <a href="#">Twitter</a>
              </li>
              <li>
                <a href="#">Facebook</a>
              </li>
            </ol>
          </div>
        </aside>
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
  name: 'Category',
  components: { ProductBox, CategoriesList, SearchFilters },
  props: {
    categoryId: String,
  },
  computed: {
    products() {
      return this.$store.state.products.productsList;
    },
  },
  methods: {
    getProducts() {
      this.$store.dispatch('products/resetSearchTermsActions')
        .then(() => this.$store.dispatch('products/setCateogryAction', this.categoryId))
        .then(() => this.$store.dispatch('products/searchProductsAction'));
    },
  },
  watch: {
    categoryId() {
      this.getProducts();
    },
  },
  created() {
    this.getProducts();
  },
};
</script>

<style scoped>
</style>
