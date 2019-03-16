<template>
  <div>
    <CategoriesListGroup :categories="categories"/>
  </div>
</template>

<script>
import CategoriesListGroup from "./CategoriesListGroup.vue";

export default {
  components: { CategoriesListGroup },
  computed: {
    categories() {
      return this.$store.state.products.categoriesList;
    }
  },
  created() {
    this.$store.dispatch("products/getCategoriesAction");
  }
};
</script>

<style scoped>
    div {
        text-align: left;
    }
</style>
