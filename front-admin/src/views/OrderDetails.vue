<template>
  <div>
    <div
      class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom"
    >
      <h1 class="h2">Zamówienie #{{ order.id }}</h1>
    </div>
    <div class="row">
      <div class="col-sm-6">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title">Zamówienie</h5>
            <p class="card-text">
              Identyfikator: {{ order.id }}<br>
              Data złożenia: {{ order.createDate | dateTime }}<br>
              Status: <OrderStatus :status="order.status" />
            </p>
          </div>
        </div>
      </div>
      <div class="col-sm-6">
        <div class="card">
          <div class="card-body">
            <h5
              class="card-title"
            >Klient - {{ order.customer.firstName }} {{ order.customer.lastName }}</h5>
            <p class="card-text">
              Identyfikator: {{ order.customer.customerId }}
              <br>
              Email: {{ order.customer.email }}
              <br>
              Telefon: {{ order.customer.phone }}
              <br>
            </p>
          </div>
        </div>
      </div>
      <div class="col-sm-12">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title">Produkty</h5>
            <b-table hover :items="order.orderLines" :fields="fieldsConfig">
              <template slot="productPrice" slot-scope="row">{{ row.value | currency }}</template>
              <template slot="value" slot-scope="row">{{ row.value | currency }}</template>
              <template slot="actions" slot-scope="row">
                <b-button size="sm" @click="row" class="mr-1">Edytuj</b-button>
                <b-button size="sm" @click="row" class="mr-1">Usuń</b-button>
              </template>
            </b-table>
            <p class="text-right font-weight-bold">Razem: {{ order.total | currency }}</p>
          </div>
        </div>
      </div>
    </div>
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
          key: "productId",
          label: "#",
          sortable: false
        },
        {
          key: "productName",
          label: "Nazwa",
          sortable: false
        },
        {
          key: "productPrice",
          label: "Cena",
          sortable: false
        },
        {
          key: "quantity",
          label: "Ilość",
          sortable: false
        },
        {
          key: "value",
          label: "Wartość",
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
    order() {
      return this.$store.state.orders.orderDetails;
    }
  },
  props: {
    orderId: String // todo number
  },
  created() {
    this.$store.dispatch("orders/getOrderAction", this.orderId);
  }
};
</script>

<style>
</style>
