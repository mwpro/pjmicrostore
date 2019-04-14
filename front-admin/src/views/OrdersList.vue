<template>
  <div>
    <div
      class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom"
    >
      <h1 class="h2">Zamówienia</h1>
    </div>

    <b-table hover :items="products" :fields="fieldsConfig">      
      <template slot="createDateUtc" slot-scope="row">
        {{ row.value | dateTime }}
      </template>
      <template slot="amount" slot-scope="row">
        {{ row.value | currency }}
      </template>
      <template slot="actions" slot-scope="row">
        <b-button size="sm" @click="row" class="mr-1">Zobacz</b-button>
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
          key: "createDateUtc",
          label: "Data",
          sortable: false
        },
        {
          key: "customer",
          label: "Klient",
          sortable: false
        },
        {
          key: "amount",
          label: "Kwota",
          sortable: false
        },
        {
          key: "paymentMethod",
          label: "Metoda płatności",
          sortable: false
        },        
        {
          key: "status",
          label: "Status",
          sortable: false
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
      return this.$store.state.orders.ordersList;
    }
  },
  created() {
    this.$store.dispatch("orders/getOrdersAction");
  }
};
</script>

<style>
</style>
