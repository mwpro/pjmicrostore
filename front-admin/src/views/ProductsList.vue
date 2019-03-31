<template>
  <div>
    <div
      class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom"
    >
      <h1 class="h2">Produkty</h1>
      <b-button-toolbar class="mb-2 mb-md-0">
        <b-button-group size="sm" class="mr-1">
          <b-button to="products/new">Dodaj nowy</b-button>
        </b-button-group>
      </b-button-toolbar>
    </div>

    <b-table hover :items="products" :fields="fieldsConfig">
      <template slot="actions" slot-scope="row">
        <b-button size="sm" @click="row" class="mr-1">Edytuj</b-button>
        <b-button size="sm" @click="row" class="mr-1">Usuń</b-button>
      </template>
    </b-table>
  </div>
</template>

<script>
export default {
  data() {
    return {
      fieldsConfig: [
        {
          key: "id",
          label: "#",
          sortable: false
        },
        {
          key: "name",
          label: "Nazwa",
          sortable: true
        },
        {
          key: "category.name",
          label: "Kategoria",
          sortable: false
        },
        {
          key: "price",
          label: "Cena",
          sortable: true
        },
        {
          key: "actions",
          label: ""
        }
      ]
    };
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

<style>
</style>
