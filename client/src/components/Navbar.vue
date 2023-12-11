<template>
  <!-- FIXME MAKE LOOK RIGHT ON MOBILE -->
  <nav class="navbar border-bottom d-flex justify-content-between navbar-expand-sm navbar-dark px-3">
    <div class="d-flex common-width align-items-center">

      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <button class="btn btn-primary rounded">Home</button>

      </router-link>
      <div class="dropdown">
        <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown"
          aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          <li data-bs-toggle="modal" data-bs-target="#createKeepModal"><a class="dropdown-item">New
              Keep</a></li>
          <li data-bs-toggle="modal" data-bs-target="#createVaultModal"><a class="dropdown-item">New
              Vault</a></li>
        </ul>
      </div>
    </div>

    <!-- LOGIN COMPONENT HERE -->
    <div>
      logo
    </div>
    <div class="common-width">
      <!-- FIXME UGLY -->
      <Login />
    </div>

  </nav>
</template>

<script>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.common-width {
  width: 14rem;
}

.border-bottom {
  /* border: 4rem solid black; */
  background-color: #FEF6F0;
}

.nav-link {
  text-transform: uppercase;
}



/* @media screen and (min-width: 768px) {
  nav {
    height: 64px;
  } */
/* } */
</style>
