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

      <template slot="price" slot-scope="row">
        {{ row.value | currency }}
      </template>  
      <template slot="actions" slot-scope="row">
        <b-button :to="'products/'+row.item.id" size="sm" @click="row" class="mr-1">Edytuj</b-button>
        <b-button size="sm" @click="row" class="mr-1">Usuń</b-button>
      </template>
    </b-table>

    <b-pagination 
      v-model="currentPage"
      :total-rows="productsCount"
      :per-page="perPage"
    >
    </b-pagination>
  </div>
</template>

<script>
export default {
  data() {
    return {
      currentPage: 1,
      perPage: 10,
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
  watch: {
    currentPage: function () {
      this.getProducts();
    },
  },
  methods: {
    getProducts() {
      this.$store.dispatch("products/getProductsAction", {
        page: this.currentPage,
      });
    },
  },
  computed: {
    products() {
      return this.$store.state.products.productsList;
    },
    productsCount() {
      return this.$store.state.products.productsCount;
    },
  },
  created() {
    this.getProducts();
  },
};
</script>

<style>
</style>
