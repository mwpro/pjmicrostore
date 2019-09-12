// based on
// https://getbootstrap.com/docs/4.3/examples/blog/
<template>
  <div>
    <div class="container">
      <TopMenu />
    </div>
    <div class="container">
      <slot />
    </div>

    <!-- /.container -->
    <footer class="blog-footer">
      <p>pjmicrostore</p>
    </footer>
  </div>
  <!--
            <slot/> -->
</template>

<script>
import TopMenu from '../components/TopMenu.vue';

export default {
  components: { TopMenu },
};
</script>

<style>
</style>
