<template>
  <!-- FIXME MAKE LOOK RIGHT ON MOBILE -->
  <nav class="navbar border-bottom d-flex justify-content-between navbar-expand-sm navbar-dark px-3">
    <div class="d-flex common-width align-items-center">

      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <button class="btn btn-light oxygen fw-bold py-1 rounded">Home</button>

      </router-link>
      <div v-if="account.id" class="dropdown">
        <button class="btn fw-bold oxygen dropdown-toggle" type="button" id="CreateButtonDropDown"
          data-bs-toggle="dropdown" aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu" aria-labelledby="CreateButtonDropDown">
          <li data-bs-toggle="modal" data-bs-target="#createKeepModal"><a class="dropdown-item">New
              Keep</a></li>
          <li data-bs-toggle="modal" data-bs-target="#createVaultModal"><a class="dropdown-item">New
              Vault</a></li>
        </ul>
      </div>
    </div>

    <!-- LOGIN COMPONENT HERE -->
    <div>
      <!-- FIXME PLACEHOLDER LOGO PLEASE FIND A WAY TO IMPORT THE REAL ONE -->
      <div class="d-flex ps-2 pe-4 flex-column fw-bold logo-border rounded">
        <p class="mb-0 no-select marko-one">the</p>
        <p class="mb-0 no-select marko-one">keepr</p>
        <p class="mb-1 no-select marko-one">co.</p>
      </div>
    </div>
    <div class="common-width">
      <!-- FIXME UGLY -->
      <Login />
    </div>

  </nav>
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { AppState } from '../AppState';
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
      },
      account: computed(() => AppState.account)
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.btn-light {
  background-color: #E9D8D6;
  border: none;
}

.logo-border {
  border: 2px solid black;
}

.no-select {
  user-select: none;
}


.common-width {
  width: 14rem;
}

p {
  height: 1.25rem;
}

.border-bottom {
  background-color: #FEF6F0;
  border-bottom: 3px solid #e6e4e3 !important;
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
