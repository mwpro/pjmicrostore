<template>
  <div class="p-4">
    <div v-for="searchFilter in searchFilters" v-bind:key="searchFilter.attributeName">
        <h5>{{searchFilter.attributeName}}</h5>
        <div class="form-check" v-for="attributeValue in searchFilter.stringValueAttributeValues" v-bind:key="attributeValue.value">
          <input class="form-check-input" type="checkbox" v-model="searchModel" :value="{ attribute: searchFilter.attributeName, value: attributeValue.value }"
           :id="'filter'+searchFilter.attributeName+attributeValue.value">
          <label class="form-check-label" :for="'filter'+searchFilter.attributeName+attributeValue.value">
            {{ attributeValue.value }} ({{ attributeValue.count }})
          </label>
        </div>
    </div>
    {{ searchModel }}
    <button @click="updateFilter" class="btn btn-primary btn-block">Filtruj</button>
  </div>
</template>

<script>
export default {
  data() {
    return {
      searchModel: [],
    };
  },
  computed: {
    searchFilters() {
      return this.$store.state.products.searchAttributes;
    },
  },
  methods: {
    updateFilter() {
      this.$store.dispatch('products/setFiltersAction', this.searchModel)
        .then(() => this.$store.dispatch('products/searchProductsAction'));
    },
  },
};
</script>

<style>
</style>
