<template>
  <div>
    <div class="jumbotron p-4 p-md-5 text-white rounded bg-dark">
      <div class="col-md-6 px-0">
        <h1 class="display-4 font-italic">Title of a longer featured blog post</h1>
        <p
          class="lead my-3"
        >Multiple lines of text that form the lede, informing new readers quickly and efficiently about what’s most interesting in this post’s contents.</p>
        <p class="lead mb-0">
          <a href="#" class="text-white font-weight-bold">Continue reading...</a>
        </p>
      </div>
    </div>

    <main role="main" class="container">
      <div class="row">
        <aside class="col-md-3 blog-sidebar">
          <CategoriesList/>
          <div class="p-4 mb-3 bg-light rounded">
            <h4 class="font-italic">About</h4>
            <p class="mb-0">
              Etiam porta
              <em>sem malesuada magna</em> mollis euismod. Cras mattis consectetur purus sit amet fermentum. Aenean lacinia bibendum nulla sed consectetur.
            </p>
          </div>

          <div class="p-4">
            <h4 class="font-italic">Archives</h4>
            <ol class="list-unstyled mb-0">
              <li>
                <a href="#">March 2014</a>
              </li>
              <li>
                <a href="#">February 2014</a>
              </li>
              <li>
                <a href="#">January 2014</a>
              </li>
              <li>
                <a href="#">December 2013</a>
              </li>
              <li>
                <a href="#">November 2013</a>
              </li>
              <li>
                <a href="#">October 2013</a>
              </li>
              <li>
                <a href="#">September 2013</a>
              </li>
              <li>
                <a href="#">August 2013</a>
              </li>
              <li>
                <a href="#">July 2013</a>
              </li>
              <li>
                <a href="#">June 2013</a>
              </li>
              <li>
                <a href="#">May 2013</a>
              </li>
              <li>
                <a href="#">April 2013</a>
              </li>
            </ol>
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
import ProductBox from "./../components/ProductBox.vue";
import CategoriesList from "./../components/CategoriesList.vue";

export default {
  name: "HelloWorld",
  components: { ProductBox, CategoriesList },
  props: {
    msg: String
  },
  computed: {
    products() {
      return this.$store.state.products.productsList;
    }
  },
  created() {
    this.$store.dispatch("products/getProductsAction");
  }
};
</script>

<style scoped>
</style>
