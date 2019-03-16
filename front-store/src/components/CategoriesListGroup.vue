<template>    
    <ul>
        <li v-for="category in categories" v-bind:key="category.id">
            {{ category.name }}
            <CategoriesListGroup :categories="category.child" />
        </li>
    </ul>
</template>

<script>
import CategoriesListGroup from "./CategoriesListGroup.vue";

export default {
  components: { CategoriesListGroup },
  name: "CategoriesListGroup",
  props: {
    categories: Array,
  }, 
}
</script>

<style>   

</style>
