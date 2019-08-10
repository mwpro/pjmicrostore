<template>
  <div>
    <div class="py-5 text-center">
      <h2>Złóż zamówienie</h2>
    </div>

    <div v-if="isCartEmpty">Koszyk jest pusty</div>
    <div class="row" v-else>
      <CartSummary />

      <div class="col-md-8 order-md-1">
        <div>
          <div class="card">
            <div class="card-header">
              <h5 class="mb-0">1. Logowanie</h5>
            </div>
            <div class="collapse" :class="{ show: showLoginStep }">
              <LoginStep v-if="showLoginStep" />
            </div>
          </div>
          <div class="card">
            <div class="card-header">
              <h5 class="mb-0">2. Dostawa</h5>
            </div>
            <div class="collapse" :class="{ show: showShippingStep }">
              <ShippingStep v-if="showShippingStep" />
            </div>
          </div>
          <div class="card">
            <div class="card-header">
              <h5 class="mb-0">3. Płatność</h5>
            </div>
            <div class="collapse" :class="{ show: showPaymentStep }">
              <PaymentStep v-if="showPaymentStep" />
            </div>
          </div>
          <div class="card">
            <div class="card-header">
              <h5 class="mb-0">4. Adres</h5>
            </div>
            <div class="collapse" :class="{ show: showAddressStep }">
              <AddressStep v-if="showAddressStep" />
            </div>
          </div>
        </div>
        <hr class="mb-4" />

        <button
          :disabled="!orderButtonEnabled"
          @click="placeOrder()"
          class="btn btn-primary btn-lg btn-block"
        >Złóż zamówienie</button>
      </div>
    </div>
  </div>
</template>

<script>
import LoginStep from '../components/Checkout/LoginStep.vue';
import ShippingStep from '../components/Checkout/ShippingStep.vue';
import PaymentStep from '../components/Checkout/PaymentStep.vue';
import AddressStep from '../components/Checkout/AddressStep.vue';
import CartSummary from '../components/Checkout/CartSummary.vue';

export default {
  components: {
    LoginStep,
    ShippingStep,
    PaymentStep,
    AddressStep,
    CartSummary,
  },
  data() {
    return {
      orderButtonClicked: false,
    };
  },
  computed: {
    isCartEmpty() {
      return this.$store.getters['cart/isCartEmpty'];
    },
    showLoginStep() {
      return !this.$auth.isAuthenticated && !this.$store.state.cart.checkoutAsGuest;
    },
    showShippingStep() {
      return this.$store.state.cart.deliveryMethods && !this.showLoginStep;
    },
    showPaymentStep() {
      return (
        this.$store.state.cart.selectedDeliveryMethod
        && this.$store.state.cart.paymentMethods
      );
    },
    showAddressStep() {
      return this.$store.state.cart.selectedPaymentMethod;
    },
    orderButtonEnabled() {
      return (
        this.$store.state.cart.selectedPaymentMethod !== null && !this.$data.orderButtonClicked
      );
    },
  },
  created() {
    this.$store.dispatch('cart/getCartAction');
    this.$store.dispatch('cart/getDeliveryMethodsAction');
  },
  methods: {
    placeOrder() {
      this.$data.orderButtonClicked = true;
      this.$store
        .dispatch('cart/placeOrder')
        .then((order) => {
          if (order.paymentCheckUrl !== undefined) {
            this.$router.push({
              name: 'paymentRedirect',
              params: { order },
            });
          } else {
            this.$router.push({ name: 'orderPlaced' });
          }
        });
    },
  },
};
</script>

<style>
</style>
