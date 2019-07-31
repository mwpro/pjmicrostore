// based on
// https://getbootstrap.com/docs/4.3/examples/dashboard/
<template>
  <div>
    <TopMenu />
    <div class="container-fluid">
      <div class="row">
        <Sidebar />

        <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-4">
          <slot />
        </main>
      </div>
    </div>
  </div>
</template>

<script>

import Sidebar from '../components/Sidebar.vue';
import TopMenu from '../components/TopMenu.vue';

export default {
  components: { TopMenu, Sidebar },
  created() {
    this.$auth.getUser().then((x) => {
      if (x == null) {
        this.$auth.login();
      }
    });
  },
};
</script>

<style>
</style>
