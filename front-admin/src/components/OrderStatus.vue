<template>
  <span class="badge badge-pill" :class="'badge-'+color">{{ translation }}</span>
</template>

<script>
export default {
  props: {
    status: String
  },
  computed: {
    translation() {
        switch (this.status) {
            case "New":
                return "Nowe";
            case "WaitingForPayment":
                return "Oczekuje na płatność";
            case "WaitingForShipment":
                return "Oczekuje na wysyłkę";                    
            default:
                return this.status;
        }
    },
    color() {
      switch (this.status) {
            case "New":
            case "WaitingForPayment":
                return "primary";
            case "WaitingForShipment":
                return "warning";                    
            default:
                return "primary";
        }
    }
  }
};
</script>

<style>
</style>
