<template>
    <h1>Trwa przekierowanie...</h1>
</template>

<script>
export default {
  created() {
    console.log(this.$auth);
    this.$auth.silentCallback();
  },
};
</script>

<style>
</style>
