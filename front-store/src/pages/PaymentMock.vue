<template>
    <div class="bankBox">
        <h1>Witaj w Twoim banku</h1>
        <div>Czy akceptujesz poniższą płatność?</div>
        <div>Kwota: <strong>{{ paymentMockDetails.amount | currency }}</strong></div>
        <div>Tytułem: {{ paymentMockDetails.paymentDescription }}</div>
        <div>
            <button class="accept" @click="solve(true)" >Tak</button> 
            <button class="reject" @click="solve(false)">Nie</button>
        </div>
        <div><small>Id transakcji: {{ paymentMockDetails.providerReference }}</small></div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      paymentMockDetails: 0,
    }
  },
  props: {
    paymentMockId: String
  },
  created() {
    axios
        .get(`/api/payments/mock/${this.paymentMockId}`)
        .then((response) => {
          if (response.status !== 200) throw Error(response.message);
          let paymentMockDetails = response.data;
          if (typeof paymentMockDetails !== 'object') {
            paymentMockDetails = []; // todo ???
          }
          this.$data.paymentMockDetails = paymentMockDetails;
          return paymentMockDetails;
        });
  },   
  methods: {
      solve(success) {
          axios.post(`/api/payments/mock/${this.paymentMockId}/${success}`)
          .then((response) => {
            if (response.status !== 200) throw Error(response.message);
            let paymentResult = response.data;
            if (typeof paymentResult !== 'object') {
                paymentResult = []; // todo ???
            }
            window.location.href = paymentResult.returnUrl;
            return paymentMockDetails;
        });
      }
  }
}
</script>

<style scoped>
    html {
        height: 100%;
    }
    body {
        background-color: blue;
        height: 100%;
    }
    div.bankBox {
        margin: 0 auto;
        width: 500px;
        background-color: white;
        margin-top: 100px;
        padding: 20px;
    }
    h1 {
        font-size: 24px;
    }
    .accept {
        background-color: green;
    }
    .reject {
        background-color: red;
    }

</style>
