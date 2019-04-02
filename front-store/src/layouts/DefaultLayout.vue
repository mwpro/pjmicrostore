<template>
    <div>
        <TopMenu />
        <section class="section">
          <div class="container">
            <slot/>          
          </div>
        </section>
        <footer class="footer has-text-centered">
          pjmicrostore 2019
        </footer>
    </div>
</template>

<script>
import CategoriesList from './../components/CategoriesList.vue';
import TopMenu from './../components/TopMenu.vue';
export default {
  components: { CategoriesList, TopMenu },  
}
</script>

<style>

</style>
