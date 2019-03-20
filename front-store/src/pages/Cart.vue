<template>
    <div>
        <div v-if="cart.cartItems.length === 0">Koszyk jest pusty</div>
        <div v-else>
            <table>
                <thead>
                    <tr>
                        <th>Produkt</th>
                        <th>Cena</th>
                        <th>Ilość</th>
                        <th>Wartość</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in cart.cartItems" v-bind:key="item.productId">
                        <td>{{ item.productName }}</td>
                        <td>{{ item.productPrice | currency }}</td>                        
                        <td>                            
                            <button @click="updateItem(item, item.quantity - 1)" :disabled="item.quantity == 1">-</button>
                            {{ item.quantity }} <!-- TODO input for editing -->
                            <button @click="updateItem(item, item.quantity + 1)">+</button>
                        </td>
                        <td>{{ item.value | currency }}</td>
                        <td><button @click="removeItem(item)">x</button></td>
                    </tr>
                </tbody>
            </table>
            <p>
                Razem: {{ cart.total | currency }}
            </p>
            <div>
                <div v-for="paymentMethod in paymentMethods" v-bind:key="paymentMethod">
                    <input type="radio" v-model="selectedPaymentMethod" :value="paymentMethod" />
                    <label>{{ paymentMethod }}</label>
                </div>
            </div>
            <button :disabled="!orderButtonEnabled" @click="placeOrder()">Złóż zamówienie</button>
        </div>
    </div>
</template>

<script>
export default {
  data() {
    return {
      selectedPaymentMethod: null,
      orderButtonClicked: false
    }
  },
  computed: {
    cart() {
      return this.$store.state.cart.cart;
    },       
    paymentMethods() {
      return this.$store.state.cart.paymentMethods;
    },           
    orderButtonEnabled() {
      return this.$data.selectedPaymentMethod !== null && !this.$data.orderButtonClicked;
    }, 
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
      this.$store.dispatch("cart/placeOrder", {
           paymentMethod: this.$data.selectedPaymentMethod
        })
        .then(order => {
            if (order.PaymentCheckUrl !== null) {
                this.$router.push({ name: 'paymentRedirect', params: { order: order } });
            } else {
                this.$router.push({ name: 'orderPlaced' });
            }
        });      
    }
  }
}
</script>

<style>

</style>
