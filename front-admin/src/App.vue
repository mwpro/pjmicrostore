// https://getbootstrap.com/docs/4.3/examples/dashboard/
<template>
  <div id="app">
    <component :is="layout">
      <router-view/>
    </component>
  </div>
</template>

<script>
import DefaultLayout from './layouts/DefaultLayout.vue';
import MinimalLayout from './layouts/MinimalLayout.vue';

export default {
  components: {
    DefaultLayout,
    MinimalLayout,
  },
  computed: {
    layout() {
      return `${(this.$route.meta.layout || 'Default')}Layout`;
    },
  },
};
</script>


<style>
</style>
