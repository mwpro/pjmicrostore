<template>
    <div class="card-body">
        <div class="mb-3">
          <label for="email">Email</label>
          <input
            type="email"
            class="form-control"
            v-model="email"
            id="email"
            placeholder="you@example.com"
          />
          <div class="invalid-feedback">Wprowadź prawidłowy adres email.</div>
        </div>
        <div class="mb-3">
          <label for="phone">Numer telefonu</label>
          <input
            type="phone"
            class="form-control"
            v-model="phone"
            id="phone"
            placeholder="123-456-789"
          />
          <div class="invalid-feedback">Wprowadź prawidłowy numer telefonu.</div>
        </div>
        <h4 class="mb-3">Dane do wysyłki</h4>
        <form class="needs-validation" novalidate>
          <div class="row">
            <div class="col-md-6 mb-3">
              <label for="shipping-firstName">Imię</label>
              <input
                type="text"
                class="form-control"
                v-model="shippingDetailsFirstName"
                id="shipping-firstName"
                placeholder="Jan"
                value
                required
              />
              <div class="invalid-feedback">Imię jest wymagane.</div>
            </div>
            <div class="col-md-6 mb-3">
              <label for="shipping-lastName">Nazwisko</label>
              <input
                type="text"
                class="form-control"
                id="shipping-lastName"
                v-model="shippingDetailsLastName"
                placeholder="Kowalski"
                value
                required
              />
              <div class="invalid-feedback">Nazwisko jest wymagane.</div>
            </div>
          </div>

          <div class="mb-3">
            <label for="shipping-address">Adres</label>
            <input
              type="text"
              class="form-control"
              id="shipping-address"
              v-model="shippingDetailsAddress"
              placeholder="Przykładowa 123/3"
              required
            />
            <div class="invalid-feedback">Wprowadź adres wysyłki.</div>
          </div>

          <div class="row">
            <div class="col-md-9 mb-9">
              <label for="shipping-city">Miasto</label>
              <input
                type="text"
                class="form-control"
                v-model="shippingDetailsCity"
                id="shipping-city"
                placeholder="Warszawa"
                required
              />
              <div class="invalid-feedback">Wprowadź miasto.</div>
            </div>
            <div class="col-md-3 mb-3">
              <label for="shipping-zip">Kod pocztowy</label>
              <input
                type="text"
                class="form-control"
                v-model="shippingDetailsZip"
                id="shipping-zip"
                placeholder="12-345"
                required
              />
              <div class="invalid-feedback">Wprowadź kod pocztowy.</div>
            </div>
          </div>
          <hr class="mb-4" />
          <div class="custom-control custom-checkbox">
            <input
              type="checkbox"
              class="custom-control-input"
              id="same-address"
              v-model="billingDetailsAreDifferentFromShipping"
            />
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
                v-model="billingDetailsFirstName"
                id="billing-firstName"
                placeholder="Jan"
                value
                required
              />
              <div class="invalid-feedback">Imię jest wymagane.</div>
            </div>
            <div class="col-md-6 mb-3">
              <label for="billing-lastName">Nazwisko</label>
              <input
                type="text"
                class="form-control"
                id="billing-lastName"
                v-model="billingDetailsLastName"
                placeholder="Kowalski"
                value
                required
              />
              <div class="invalid-feedback">Nazwisko jest wymagane.</div>
            </div>
          </div>

          <div class="mb-3" v-if="billingDetailsAreDifferentFromShipping">
            <label for="billing-address">Adres</label>
            <input
              type="text"
              class="form-control"
              id="billing-address"
              v-model="billingDetailsAddress"
              placeholder="Przykładowa 123/3"
              required
            />
            <div class="invalid-feedback">Wprowadź adres wysyłki.</div>
          </div>

          <div class="row" v-if="billingDetailsAreDifferentFromShipping">
            <div class="col-md-9 mb-9">
              <label for="billing-city">Miasto</label>
              <input
                type="text"
                class="form-control"
                v-model="billingDetailsCity"
                id="billing-city"
                placeholder="Warszawa"
                required
              />
              <div class="invalid-feedback">Wprowadź miasto.</div>
            </div>
            <div class="col-md-3 mb-3">
              <label for="billing-zip">Kod pocztowy</label>
              <input
                type="text"
                class="form-control"
                v-model="billingDetailsZip"
                id="billing-zip"
                placeholder="12-345"
                required
              />
              <div class="invalid-feedback">Wprowadź kod pocztowy.</div>
            </div>
          </div>
        </form>
    </div>
</template>

<script>
export default {
  computed: {
    email: {
      get() {
        return this.$store.state.cart.email;
      },
      set(value) {
        this.$store.commit('cart/updateEmail', value);
      },
    },
    phone: {
      get() {
        return this.$store.state.cart.phone;
      },
      set(value) {
        this.$store.commit('cart/updatePhone', value);
      },
    },
    shippingDetailsAddress: {
      get() {
        return this.$store.state.cart.shippingDetailsAddress;
      },
      set(value) {
        this.$store.commit('cart/updateShippingDetailsAddress', value);
      },
    },
    shippingDetailsCity: {
      get() {
        return this.$store.state.cart.shippingDetailsCity;
      },
      set(value) {
        this.$store.commit('cart/updateShippingDetailsCity', value);
      },
    },
    shippingDetailsFirstName: {
      get() {
        return this.$store.state.cart.shippingDetailsFirstName;
      },
      set(value) {
        this.$store.commit('cart/updateShippingDetailsFirstName', value);
      },
    },
    shippingDetailsLastName: {
      get() {
        return this.$store.state.cart.shippingDetailsLastName;
      },
      set(value) {
        this.$store.commit('cart/updateShippingDetailsLastName', value);
      },
    },
    shippingDetailsStreet: {
      get() {
        return this.$store.state.cart.shippingDetailsStreet;
      },
      set(value) {
        this.$store.commit('cart/updateShippingDetailsStreet', value);
      },
    },
    shippingDetailsZip: {
      get() {
        return this.$store.state.cart.shippingDetailsZip;
      },
      set(value) {
        this.$store.commit('cart/updateShippingDetailsZip', value);
      },
    },
    billingDetailsAreDifferentFromShipping: {
      get() {
        return this.$store.state.cart.billingDetailsAreDifferentFromShipping;
      },
      set(value) {
        this.$store.commit('cart/updateBillingDetailsAreDifferentFromShipping', value);
      },
    },
    billingDetailsAddress: {
      get() {
        return this.$store.state.cart.billingDetailsAddress;
      },
      set(value) {
        this.$store.commit('cart/updateBillingDetailsAddress', value);
      },
    },
    billingDetailsCity: {
      get() {
        return this.$store.state.cart.billingDetailsCity;
      },
      set(value) {
        this.$store.commit('cart/updateBillingDetailsCity', value);
      },
    },
    billingDetailsFirstName: {
      get() {
        return this.$store.state.cart.billingDetailsFirstName;
      },
      set(value) {
        this.$store.commit('cart/updateBillingDetailsFirstName', value);
      },
    },
    billingDetailsLastName: {
      get() {
        return this.$store.state.cart.billingDetailsLastName;
      },
      set(value) {
        this.$store.commit('cart/updateBillingDetailsLastName', value);
      },
    },
    billingDetailsStreet: {
      get() {
        return this.$store.state.cart.billingDetailsStreet;
      },
      set(value) {
        this.$store.commit('cart/updateBillingDetailsStreet', value);
      },
    },
    billingDetailsZip: {
      get() {
        return this.$store.state.cart.billingDetailsZip;
      },
      set(value) {
        this.$store.commit('cart/updateBillingDetailsZip', value);
      },
    },

  },
  created() {
    this.$store.dispatch('user/getUserAction').then((user) => {
      this.email = user.email;
      this.phone = user.phone;
      this.shippingDetailsAddress = user.shippingDetails.street;
      this.shippingDetailsCity = user.shippingDetails.city;
      this.shippingDetailsFirstName = user.shippingDetails.firstName;
      this.shippingDetailsLastName = user.shippingDetails.lastName;
      this.shippingDetailsZip = user.shippingDetails.zip;
      if (user.billingDetails.street) {
        this.billingDetailsAreDifferentFromShipping = true;
        this.billingDetailsAddress = user.billingDetails.street;
        this.billingDetailsCity = user.billingDetails.city;
        this.billingDetailsFirstName = user.billingDetails.firstName;
        this.billingDetailsLastName = user.billingDetails.lastName;
        this.billingDetailsZip = user.billingDetails.zip;
      } else {
        this.billingDetailsAreDifferentFromShipping = false;
      }
    });
  },
};
</script>

<style>

</style>
