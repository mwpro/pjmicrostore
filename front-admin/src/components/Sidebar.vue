<template>
  <nav class="col-md-2 d-none d-md-block bg-light sidebar">
    <div class="sidebar-sticky">
      <ul class="nav flex-column">
        <li class="nav-item">
          <router-link to="/" class="nav-link">
            <span data-feather="home"></span>
            Strona główna
            <span class="sr-only">(current)</span>
          </router-link>
        </li>
        <li class="nav-item">
          <router-link to="/orders" class="nav-link" href="#">
            <span data-feather="file"></span>
            Zamówienia
          </router-link>
        </li>
        <li class="nav-item">
          <router-link to="/products" class="nav-link" href="#">
            <span data-feather="shopping-cart"></span>
            Produkty
          </router-link>
        </li>        
      </ul>
    </div>
  </nav>
</template>

<script>
export default {};
</script>

<style>
</style>
