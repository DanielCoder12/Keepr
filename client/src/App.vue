<template>
  <header>
    <div>
      <Navbar />
    </div>
  </header>
  <main>
    <router-view />
  </main>
  <footer>
  </footer>
  <ActiveKeepModal />
  <CreateVaultModal />
  <CreateKeepModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import ActiveKeepModal from './components/ActiveKeepModal.vue'
import CreateVaultModal from './components/CreateVaultModal.vue'
import CreateKeepModal from './components/CreateKeepModal.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, ActiveKeepModal, CreateVaultModal, CreateKeepModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
