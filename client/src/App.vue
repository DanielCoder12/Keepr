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
  <EditAccountModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import ActiveKeepModal from './components/ActiveKeepModal.vue'
import CreateVaultModal from './components/CreateVaultModal.vue'
import CreateKeepModal from './components/CreateKeepModal.vue'
import EditAccountModal from './components/EditAccountModal.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, ActiveKeepModal, CreateVaultModal, CreateKeepModal, EditAccountModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

.marko-one {
  font-family: 'Bebas Neue', sans-serif;
  font-family: 'Marko One', serif;
  font-family: 'Sahitya', serif;
}

.oxygen {
  font-family: 'Bebas Neue', sans-serif;
  font-family: 'Marko One', serif;
  font-family: 'Oxygen', sans-serif;
  font-family: 'Sahitya', serif;
}

.quando {
  font-family: 'Bebas Neue', sans-serif;
  font-family: 'Marko One', serif;
  font-family: 'Oxygen', sans-serif;
  font-family: 'Quando', serif;
  font-family: 'Sahitya', serif;
}

.font-shadow {
  text-shadow: 1px 1px 8px black;
}

.card-shadow {
  box-shadow: 0px 5px 3px rgba(0, 0, 0, 0.188);
}

.font-gray {
  color: #636E72;
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}

// FIXME FIGURE OUT MASONRY ON MOBILE SIDE BY SIDE STACKING
.masonry-with-columns {
  columns: 4 300px;
  column-gap: 1rem;

  div {
    width: 150px;
    // background: #EC985A;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    // text-align: center;
    // font-family: system-ui;
    // font-weight: 900;
    // font-size: 2rem;
  }

  @for $i from 1 through 36 {
    div:nth-child(#{$i}) {
      $h: (
        random(400) + 100) + px;
      // height: $h;
      // line-height: $h;
    }
  }
}

a {
  user-select: none;
}

main {
  background-color: #FEF6F0;
}
</style>
