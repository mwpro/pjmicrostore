<template>
    <div>
        <TopMenu />
        <section class="section">
          <div class="container">
            <slot/>          
          </div>
        </section>
        <footer class="footer has-text-centered">
          <div class="container">
             <div class="columns">
              <div class="column is-8-desktop is-offset-2-desktop">
                <p>
                  <strong class="has-text-weight-semibold">
                    <a href="https://www.npmjs.com/package/bulma-start">bulma-start@0.0.3</a>
                  </strong>
                </p>
                <p>
                  <small>
                    Source code licensed <a href="http://opensource.org/licenses/mit-license.php">MIT</a>
                  </small>
                </p>
                <p style="margin-top: 1rem;">
                  <a href="http://bulma.io">
                    <img src="made-with-bulma.png" alt="Made with Bulma" width="128" height="24">
                  </a>
                </p>
              </div>
            </div>
          </div>
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
