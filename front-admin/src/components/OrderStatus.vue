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
            case "Paid":
                return "Zapłacone";                    
            default:
                return this.status;
        }
    },
    color() {
      switch (this.status) {
            case "New":
                return "primary";
            case "Paid":
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
