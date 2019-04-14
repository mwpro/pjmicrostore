<template>
  <div>
    <div
      class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom"
    >
      <h1 class="h2">Zamówienia</h1>
    </div>

    <b-table hover :items="orders" :fields="fieldsConfig">      
      <template slot="createDateUtc" slot-scope="row">
        {{ row.value | dateTime }}
      </template>
      <template slot="amount" slot-scope="row">
        {{ row.value | currency }}
      </template>      
      <template slot="status" slot-scope="row">
        <OrderStatus :status="row.value" />
      </template>
      <template slot="actions" slot-scope="row">
        <b-button size="sm" :to="'orders/'+row.item.id" class="mr-1">Zobacz</b-button>
      </template>
    </b-table>
  </div>
</template>

<script>
import OrderStatus from '../components/OrderStatus'

export default {
  components: {
    OrderStatus
  },
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
    orders() {
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
