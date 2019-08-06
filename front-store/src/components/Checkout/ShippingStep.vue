<template>
  <div class="card-body">
    <div
      v-for="deliveryMethod in deliveryMethods"
      v-bind:key="deliveryMethod.name"
      class="custom-control custom-radio"
    >
      <!-- <input type="radio" v-model="selectedPaymentMethod" :value="paymentMethod"> -->
      <input
        :id="deliveryMethod.name"
        v-model="selectedDeliveryMethod"
        :value="deliveryMethod"
        type="radio"
        class="custom-control-input"
      />
      <label
        class="custom-control-label"
        :for="deliveryMethod.name"
      >{{ deliveryMethod.name }} - {{ deliveryMethod.price | currency }}</label>
    </div>
  </div>
</template>

<script>
export default {
  computed: {
    deliveryMethods() {
      return this.$store.state.cart.deliveryMethods;
    },
    selectedDeliveryMethod: {
      get() {
        return this.$store.state.cart.selectedDeliveryMethod;
      },
      set(value) {
        this.$store.dispatch('cart/setDeliveryMethodAction', value);
      },
    },
  },
};
</script>

<style>
</style>
