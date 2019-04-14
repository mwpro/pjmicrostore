<template>
  <div>
    <div class="py-5 text-center">
      <img
        class="d-block mx-auto mb-4"
        src="https://getbootstrap.com/docs/4.3/assets/brand/bootstrap-solid.svg"
        alt
        width="72"
        height="72"
      >
      <h2>Złóż zamówienie</h2>
    </div>

    <div v-if="cart.cartItems.length === 0">Koszyk jest pusty</div>
    <div class="row" v-else>
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
                <button
                  @click="updateItem(item, item.quantity - 1)"
                  :disabled="item.quantity == 1"
                >-</button>
                {{ item.quantity }}
                <!-- TODO input for editing -->
                <button @click="updateItem(item, item.quantity + 1)">+</button>
                x {{ item.productPrice | currency }}
              </small>
            </div>
            <span class="text-muted">{{ item.value | currency }}</span>
          </li>
          <!-- <li class="list-group-item d-flex justify-content-between bg-light">
            <div class="text-success">
              <h6 class="my-0">Promo code</h6>
              <small>EXAMPLECODE</small>
            </div>
            <span class="text-success">-$5</span>
          </li>-->
          <li class="list-group-item d-flex justify-content-between">
            <span>Razem</span>
            <strong>{{ cart.total | currency }}</strong>
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
      <div class="col-md-8 order-md-1">
        <div class="mb-3">
          <label for="email">Email</label>
          <input type="email" class="form-control" v-model="email"  id="email" placeholder="you@example.com">
          <div class="invalid-feedback">Wprowadź prawidłowy adres email.</div>
        </div>
        <h4 class="mb-3">Dane do wysyłki</h4>
        <form class="needs-validation" novalidate>
          <div class="row">
            <div class="col-md-6 mb-3">
              <label for="shipping-firstName">Imię</label>
              <input
                type="text"
                class="form-control"
                v-model="shippingDetails.firstName" 
                id="shipping-firstName"
                placeholder="Jan"
                value
                required
              >
              <div class="invalid-feedback">Imię jest wymagane.</div>
            </div>
            <div class="col-md-6 mb-3">
              <label for="shipping-lastName">Nazwisko</label>
              <input
                type="text"
                class="form-control"
                id="shipping-lastName"
                v-model="shippingDetails.lastName" 
                placeholder="Kowalski"
                value
                required
              >
              <div class="invalid-feedback">Nazwisko jest wymagane.</div>
            </div>
          </div>

          <div class="mb-3">
            <label for="shipping-address">Adres</label>
            <input
              type="text"
              class="form-control"
              id="shipping-address"
              v-model="shippingDetails.address" 
              placeholder="Przykładowa 123/3"
              required
            >
            <div class="invalid-feedback">Wprowadź adres wysyłki.</div>
          </div>

          <div class="row">
            <div class="col-md-9 mb-9">
              <label for="shipping-city">Miasto</label>
              <input type="text" class="form-control" v-model="shippingDetails.city" id="shipping-city" placeholder="Warszawa" required>
              <div class="invalid-feedback">Wprowadź miasto.</div>
            </div>
            <div class="col-md-3 mb-3">
              <label for="shipping-zip">Kod pocztowy</label>
              <input type="text" class="form-control" v-model="shippingDetails.zip" id="shipping-zip" placeholder="12-345" required>
              <div class="invalid-feedback">Wprowadź kod pocztowy.</div>
            </div>
          </div>
          <hr class="mb-4">
          <div class="custom-control custom-checkbox">
            <input type="checkbox" class="custom-control-input" id="same-address" v-model="billingDetailsAreDifferentFromShipping">
            <label
              class="custom-control-label"
              for="same-address"
            >Adres dostawy jest inny niż adres rozliczeniowy</label>
          </div>
          <div class="row" v-if="billingDetailsAreDifferentFromShipping">
            <div class="col-md-6 mb-3">
              <label for="billing-firstName">Imię</label>
              <input
                type="text"
                class="form-control"
                v-model="billingDetails.firstName" 
                id="billing-firstName"
                placeholder="Jan"
                value
                required
              >
              <div class="invalid-feedback">Imię jest wymagane.</div>
            </div>
            <div class="col-md-6 mb-3">
              <label for="billing-lastName">Nazwisko</label>
              <input
                type="text"
                class="form-control"
                id="billing-lastName"
                v-model="billingDetails.lastName" 
                placeholder="Kowalski"
                value
                required
              >
              <div class="invalid-feedback">Nazwisko jest wymagane.</div>
            </div>
          </div>

          <div class="mb-3" v-if="billingDetailsAreDifferentFromShipping">
            <label for="billing-address">Adres</label>
            <input
              type="text"
              class="form-control"
              id="billing-address"
              v-model="billingDetails.address" 
              placeholder="Przykładowa 123/3"
              required
            >
            <div class="invalid-feedback">Wprowadź adres wysyłki.</div>
          </div>

          <div class="row" v-if="billingDetailsAreDifferentFromShipping">
            <div class="col-md-9 mb-9">
              <label for="billing-city">Miasto</label>
              <input type="text" class="form-control" v-model="billingDetails.city" id="billing-city" placeholder="Warszawa" required>
              <div class="invalid-feedback">Wprowadź miasto.</div>
            </div>
            <div class="col-md-3 mb-3">
              <label for="billing-zip">Kod pocztowy</label>
              <input type="text" class="form-control" v-model="billingDetails.zip" id="billing-zip" placeholder="12-345" required>
              <div class="invalid-feedback">Wprowadź kod pocztowy.</div>
            </div>
          </div>
          
          <!-- 
          <div class="custom-control custom-checkbox">
            <input type="checkbox" class="custom-control-input" id="save-info">
            <label class="custom-control-label" for="save-info">Save this information for next time</label>
          </div>-->
          <hr class="mb-4">

          <h4 class="mb-3">Płatność</h4>

          <div class="d-block my-3">
            <div
              v-for="paymentMethod in paymentMethods"
              v-bind:key="paymentMethod"
              class="custom-control custom-radio"
            >
              <!-- <input type="radio" v-model="selectedPaymentMethod" :value="paymentMethod"> -->
              <input
                :id="paymentMethod"
                v-model="selectedPaymentMethod"
                :value="paymentMethod"
                type="radio"
                class="custom-control-input"
              >
              <label class="custom-control-label" :for="paymentMethod">{{ paymentMethod }}</label>
            </div>
          </div>
          <!-- <div class="row">
            <div class="col-md-6 mb-3">
              <label for="cc-name">Name on card</label>
              <input type="text" class="form-control" id="cc-name" placeholder required>
              <small class="text-muted">Full name as displayed on card</small>
              <div class="invalid-feedback">Name on card is required</div>
            </div>
            <div class="col-md-6 mb-3">
              <label for="cc-number">Credit card number</label>
              <input type="text" class="form-control" id="cc-number" placeholder required>
              <div class="invalid-feedback">Credit card number is required</div>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3 mb-3">
              <label for="cc-expiration">Expiration</label>
              <input type="text" class="form-control" id="cc-expiration" placeholder required>
              <div class="invalid-feedback">Expiration date required</div>
            </div>
            <div class="col-md-3 mb-3">
              <label for="cc-cvv">CVV</label>
              <input type="text" class="form-control" id="cc-cvv" placeholder required>
              <div class="invalid-feedback">Security code required</div>
            </div>
          </div>-->
          <hr class="mb-4">

          <button
            :disabled="!orderButtonEnabled"
            @click="placeOrder()"
            class="btn btn-primary btn-lg btn-block"
          >Złóż zamówienie</button>
        </form>
      </div>
    </div>
    {{ placeOrderModel }}
  </div>
</template>

<script>
export default {
  data() {
    return {
      email: null,
      shippingDetails: {
        firstName: null,
        lastName: null,
        address: null,
        city: null,
        zip: null
      },
      billingDetailsAreDifferentFromShipping: false,
      billingDetails: {
        firstName: null,
        lastName: null,
        address: null,
        city: null,
        zip: null
      },
      selectedPaymentMethod: null,
      orderButtonClicked: false
    };
  },
  computed: {
    cart() {
      return this.$store.state.cart.cart;
    },
    paymentMethods() {
      return this.$store.state.cart.paymentMethods;
    },
    orderButtonEnabled() {
      return (
        this.$data.selectedPaymentMethod !== null &&
        !this.$data.orderButtonClicked
      );
    },
    placeOrderModel() {
      return {        
        email: this.$data.email,
        paymentMethod: this.$data.selectedPaymentMethod,
        shippingDetails: this.$data.shippingDetails,
        billingDetails: (this.$data.billingDetailsAreDifferentFromShipping ? this.$data.billingDetails : this.$data.shippingDetails)
      };
    }
  },
  created() {
    this.$store.dispatch("cart/getPaymentMethodsAction");
    this.$store.dispatch("cart/getCartAction");
  },
  methods: {
    removeItem(item) {
      this.$store.dispatch("cart/removeFromCartAction", item.productId);
    },
    updateItem(item, updatedQuantity) {
      this.$store.dispatch("cart/updateItemAction", {
        productId: item.productId,
        quantity: updatedQuantity
      });
    },
    placeOrder() {
      this.$data.orderButtonClicked = true;
      this.$store
        .dispatch("cart/placeOrder", this.placeOrderModel)
        .then(order => {
          if (order.paymentCheckUrl !== undefined) {
            this.$router.push({
              name: "paymentRedirect",
              params: { order: order }
            });
          } else {
            this.$router.push({ name: "orderPlaced" });
          }
        });
    }
  }
};
</script>

<style>
</style>
