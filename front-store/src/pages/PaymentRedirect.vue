<template>
    <div>
        <h1>Trwa przekierowanie do płatności</h1>
        {{ attemptNumber }}
    </div>
</template>

<script>
export default {
  data() {
    return {
      attemptNumber: 0,
    };
  },
  props: {
    order: Object,
  },
  created() {
    this.tryGetPayment();
  },
  methods: {
    tryGetPayment() {
      this.$store.dispatch('cart/getPaymentDetailsAction', this.order.paymentCheckUrl).then((result) => {
        if (result == '') {
          new Promise(resolve => setTimeout(resolve, 1000))
            .then(() => this.tryGetPayment());
          this.attemptNumber++;
        } else {
          console.log(result);
          window.location.href = result;
        }
      });

    },
  },
};
</script>

<style>

</style>
