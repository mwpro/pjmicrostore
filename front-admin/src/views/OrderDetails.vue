<template>
  <div>
    <div
      class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom"
    >
      <h1 class="h2">Zamówienie #{{ order.id }}</h1>
      <b-button-toolbar class="mb-2 mb-md-0">
        <b-button-group class="mr-1" v-if="this.order.status == 'WaitingForShipment'">
          <b-button @click="complete()" variant="success">Wysłano</b-button>
        </b-button-group>
        <b-button-group class="mr-1" v-if="this.order.status != 'Completed'">
          <b-button @click="cancel()" variant="danger">Anuluj</b-button>
        </b-button-group>
      </b-button-toolbar>
    </div>
    <div class="row">
      <div class="col-sm-6">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title">Zamówienie</h5>
            <p class="card-text">
              Identyfikator: {{ order.id }}
              <br>
              Data złożenia: {{ order.createDate | dateTime }}
              <br>Status:
              <OrderStatus :status="order.status"/>
            </p>
          </div>
        </div>
      </div>
      <div class="col-sm-6">
        <div class="card">
          <div class="card-body">
            <h5
              class="card-title"
            >Klient</h5>
            <p class="card-text">
              Identyfikator: {{ order.customer.customerId }}
              <br>
              Email: {{ order.customer.email }}
              <br>
              Telefon: {{ order.customer.phone }}
              <br>
              Dane rozliczeniowe:<br >
              {{ order.billingAddress.firstName }} {{ order.billingAddress.lastName }}<br>
              {{ order.billingAddress.address }}<br>
              {{ order.billingAddress.city }}, {{ order.billingAddress.zip }}
            </p>
          </div>
        </div>
      </div>
      <div class="col-sm-6">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title">Płatność</h5>
            <p>
              Wartość towarów: {{ order.productsTotal | currency }}
              <br>Przesyłka: {{ order.shipping.name | dictionaryValue('deliveryMethod') }} - {{ order.shipping.fee | currency }}
              <br>Płatność:  {{ order.payment.name | dictionaryValue('paymentMethods') }} - {{ order.payment.fee | currency }}
              <br>
              Razem: {{ order.total | currency }}
              <br>
            </p>
          </div>
        </div>
      </div>
      <div class="col-sm-6">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title">Przesyłka</h5>
            Sposób wysyłki: {{ order.shipping.name | dictionaryValue('deliveryMethod') }}<br >
            Adres do wysyłki:<br >
              {{ order.shippingAddress.firstName }} {{ order.shippingAddress.lastName }}<br>
              {{ order.shippingAddress.address }}<br>
              {{ order.shippingAddress.city }}, {{ order.shippingAddress.zip }}
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
            </b-table>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import OrderStatus from '../components/OrderStatus';

export default {
  components: {
    OrderStatus,
  },
  data() {
    return {
      fieldsConfig: [
        {
          key: 'productId',
          label: '#',
          sortable: false,
        },
        {
          key: 'productName',
          label: 'Nazwa',
          sortable: false,
        },
        {
          key: 'productPrice',
          label: 'Cena',
          sortable: false,
        },
        {
          key: 'quantity',
          label: 'Ilość',
          sortable: false,
        },
        {
          key: 'value',
          label: 'Wartość',
          sortable: false,
        },
      ],
    };
  },
  computed: {
    order() {
      return this.$store.state.orders.orderDetails;
    },
  },
  props: {
    orderId: String, // todo number
  },
  methods: {
    cancel() {
      this.$store.dispatch('orders/cancelOrderAction', this.orderId);
    },
    complete() {
      this.$store.dispatch('orders/completeOrderAction', this.orderId);
    },
  },
  created() {
    this.$store.dispatch('orders/getOrderAction', this.orderId);
  },
};
</script>

<style>
</style>
