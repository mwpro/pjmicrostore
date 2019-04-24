<template>
    <ul>
        <li v-for="category in categories" v-bind:key="category.id">
            <router-link :to="'/category/'+category.id">{{ category.name }}</router-link>
            <CategoriesListGroup :categories="category.child" />
        </li>
    </ul>
</template>

<script>
import CategoriesListGroup from './CategoriesListGroup.vue';

export default {
  components: { CategoriesListGroup },
  name: 'CategoriesListGroup',
  props: {
    categories: Array,
  },
};
</script>

<style>

</style>
