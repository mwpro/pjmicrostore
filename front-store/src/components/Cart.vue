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
                        <td>{{ item.price | currency }}</td>                        
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
            <button @click="placeOrder()">Złóż zamówienie</button>
        </div>
    </div>
</template>

<script>
export default {
  computed: {
    cart() {
      return this.$store.state.cart.cart;
    },    
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
      this.$store.dispatch("cart/placeOrder")
        .then(order => {
            this.$router.push({ name: 'orderPlaced', params: { order: order } });
        });      
    }
  }
}
</script>

<style>

</style>
