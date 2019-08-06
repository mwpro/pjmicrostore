<template>
  <div class="col-md-4 order-md-2 mb-4">
    <h4 class="d-flex justify-content-between align-items-center mb-3">
      <span class="text-muted">Twój koszyk</span>
      <span class="badge badge-secondary badge-pill">{{ cart.numberOfItems }}</span>
    </h4>
    <ul class="list-group mb-3">
      <li
        v-for="item in cart.cartItems"
        v-bind:key="item.productId"
        class="list-group-item d-flex justify-content-between lh-condensed"
      >
        <div>
          <h6 class="my-0">{{ item.productName }}</h6>
          <small class="text-muted">
            <button @click="updateItem(item, item.quantity - 1)" :disabled="item.quantity == 1">-</button>
            {{ item.quantity }}
            <!-- TODO input for editing -->
            <button @click="updateItem(item, item.quantity + 1)">+</button>
            x {{ item.productPrice | currency }}
          </small>
        </div>
        <span class="text-muted">{{ item.value | currency }}</span>
      </li>
      <li
        class="list-group-item d-flex justify-content-between bg-light"
        v-if="selectedDeliveryMethod"
      >
        <div class="text-muted">
          <h6 class="my-0">Dostawa: {{ selectedDeliveryMethod.name }}</h6>
        </div>
        <span class="text-muted">{{ selectedDeliveryMethod.price | currency }}</span>
      </li>
      <li
        class="list-group-item d-flex justify-content-between bg-light"
        v-if="selectedPaymentMethod"
      >
        <div class="text-muted">
          <h6 class="my-0">Płatność: {{ selectedPaymentMethod.name }}</h6>
        </div>
        <span class="text-muted">{{ selectedPaymentMethod.fee | currency }}</span>
      </li>
      <li class="list-group-item d-flex justify-content-between">
        <span>Razem</span>
        <strong>{{ cartTotal | currency }}</strong>
      </li>
    </ul>

    <!-- <form class="card p-2">
          <div class="input-group">
            <input type="text" class="form-control" placeholder="Promo code">
            <div class="input-group-append">
              <button type="submit" class="btn btn-secondary">Redeem</button>
            </div>
          </div>
    </form>-->
  </div>
</template>

<script>
export default {
  computed: {
    cart() {
      return this.$store.state.cart.cart;
    },
    cartTotal() {
      return this.$store.getters['cart/cartTotal'];
    },
    selectedDeliveryMethod() {
      return this.$store.state.cart.selectedDeliveryMethod;
    },
    selectedPaymentMethod() {
      return this.$store.state.cart.selectedPaymentMethod;
    },
  },
  created() {
  },
  methods: {
    removeItem(item) {
      this.$store.dispatch('cart/removeFromCartAction', item.productId);
    },
    updateItem(item, updatedQuantity) {
      this.$store.dispatch('cart/updateItemAction', {
        productId: item.productId,
        quantity: updatedQuantity,
      });
    },
  },
};
</script>

<style>
</style>
