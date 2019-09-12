<template>
  <div class="bg-light">
    <div class="container bg-light">
      <slot/>


      <footer class="my-5 pt-5 text-muted text-center text-small">
        <p class="mb-1">pjmicrostore</p>
      </footer>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style>
.container {
  max-width: 960px;
}

.lh-condensed {
  line-height: 1.25;
}
.bd-placeholder-img {
  font-size: 1.125rem;
  text-anchor: middle;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}

@media (min-width: 768px) {
  .bd-placeholder-img-lg {
    font-size: 3.5rem;
  }
}
</style>
