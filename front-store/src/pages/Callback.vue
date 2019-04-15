<template>
    <h1>Trwa przekierowanie...</h1>
</template>

<script>
export default {
    created() {
      this.$auth.authCallback();
    }
}
</script>

<style>
</style>
