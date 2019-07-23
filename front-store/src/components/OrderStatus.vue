<template>
  <span class="badge badge-pill" :class="'badge-'+color">{{ translation }}</span>
</template>

<script>
export default {
  props: {
    status: String,
  },
  computed: {
    translation() {
      switch (this.status) {
        case 'New':
          return 'Nowe';
        case 'WaitingForPayment':
          return 'Oczekuje na płatność';
        case 'WaitingForShipment':
          return 'Oczekuje na wysyłkę';
        case 'Completed':
          return 'Zakończone';
        case 'Cancelled':
          return 'Anulowane';
        default:
          return this.status;
      }
    },
    color() {
      switch (this.status) {
        case 'New':
        case 'WaitingForPayment':
          return 'primary';
        case 'WaitingForShipment':
          return 'warning';
        case 'Completed':
          return 'success';
        case 'Cancelled':
          return 'danger';
        default:
          return 'primary';
      }
    },
  },
};
</script>

<style>
</style>
