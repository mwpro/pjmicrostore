<template>
    <div class="card-body row">
      <div class="col-md-6">
          <h6>Masz już konto?</h6>
          <a class="btn btn-sm btn-outline-secondary" @click="login()">Zaloguj się</a>
      </div>
      <div class="col-md-6">
        <h6>Nie masz konta?</h6>
        <a class="btn btn-sm btn-outline-secondary" @click="login()">Załóż konto</a>
        <br />lub<br />
        <a class="btn btn-sm btn-outline-secondary" @click="login()">Kontynuuj jako gość</a>
      </div>
    </div>
</template>

<script>
export default {
  computed: {
    isAuthenticated() {
      return this.$auth.isAuthenticated;
    },
  },
  methods: {
    login() {
      this.$auth.loginPopup();
    },
  },
};
</script>

<style>

</style>
