<template>
    <nav class="navbar navbar-dark fixed-top bg-dark flex-md-nowrap p-0 shadow">
      <a class="navbar-brand col-sm-3 col-md-2 mr-0" href="#">PJMicroStore - Zarządzanie</a>
      
      <ul class="navbar-nav px-3">
        <li class="nav-item text-nowrap">
          <a class="nav-link" @click="logout()" v-if="isAuthenticated">Wyloguj</a>
        </li>
      </ul>
    </nav>
</template>

<script>
export default {
  computed: {
    isAuthenticated() {
      return this.$auth.isAuthenticated;
    },
  },
  methods: {
    logout() {
      this.$auth.logout();
    },
  },
};
</script>

<style>

</style>
