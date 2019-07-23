<template>
  <div>
    <main role="main" class="container">
      <div class="row">
        <aside class="col-md-3 blog-sidebar">
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
            </b-table>
          </div>
        </div>

        <!-- /.blog-sidebar -->
      </div>
      <!-- /.row -->
    </main>

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
          key: 'id',
          label: '#',
          sortable: false,
        },
        {
          key: 'createDateUtc',
          label: 'Data',
          sortable: false,
        },
        {
          key: 'amount',
          label: 'Kwota',
          sortable: false,
        },
        {
          key: 'status',
          label: 'Status',
          sortable: false,
        },
      ],
    };
  },
  computed: {
    orders() {
      return this.$store.state.orders.ordersList;
    },
  },
  created() {
    this.$store.dispatch('orders/getOrdersAction');
  },

};
</script>

<style>

</style>
