<template>
    <div>
        <p>Dziękujemy za złożenie zamówienia!</p>

        <p>Numer zamówienia: {{ order.id }}</p>
    </div>
</template>

<script>
export default {
  props: {
    order: Object
  }, 
}
</script>

<style>

</style>
