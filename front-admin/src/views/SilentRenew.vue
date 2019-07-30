<template>
    <h1>Trwa przekierowanie...</h1>
</template>

<script>
export default {
  created() {
    this.$auth.silentCallback();
  },
};
</script>

<style>
</style>
