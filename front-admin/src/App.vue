// https://getbootstrap.com/docs/4.3/examples/dashboard/
<template>
  <div>
    <TopMenu />
    <div class="container-fluid">
      <div class="row">
        <Sidebar />

        <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-4">
          <router-view />
        </main>
      </div>
    </div>
  </div>
</template>

<script>
import Sidebar from "./components/Sidebar.vue";
import TopMenu from "./components/TopMenu.vue";
export default {
  components: {
    Sidebar,
    TopMenu
  }
};
</script>


<style>
</style>
