<template>
  <div class="card-body">
    <div
      v-for="paymentMethod in paymentMethods"
      v-bind:key="paymentMethod.name"
      class="custom-control custom-radio"
    >
      <input
        :id="paymentMethod.name"
        v-model="selectedPaymentMethod"
        :value="paymentMethod"
        type="radio"
        class="custom-control-input"
      />
      <label
        class="custom-control-label"
        :for="paymentMethod.name"
      >{{ paymentMethod.name }} - {{ paymentMethod.fee | currency}}</label>
    </div>
  </div>
</template>

<script>
export default {
  computed: {
    paymentMethods() {
      return this.$store.state.cart.paymentMethods;
    },
    selectedPaymentMethod: {
      get() {
        return this.$store.state.cart.selectedPaymentMethod;
      },
      set(value) {
        this.$store.dispatch('cart/setPaymentMethodAction', value);
      },
    },
  },
};
</script>

<style>
</style>
