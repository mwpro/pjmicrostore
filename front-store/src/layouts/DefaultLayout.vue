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
      <p>Blog template built for
        <a href="https://getbootstrap.com/">Bootstrap</a> by
        <a href="https://twitter.com/mdo">@mdo</a>.
      </p>
      <p>
        <a href="#">Back to top</a>
      </p>
    </footer>
  </div>
  <!-- 
            <slot/> -->
</template>

<script>
import CategoriesList from "./../components/CategoriesList.vue";
import TopMenu from "./../components/TopMenu.vue";

export default {
  components: { CategoriesList, TopMenu }
};
</script>

<style>
</style>
