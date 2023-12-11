<template>
  <!-- FIXME THIS ENTIRE PAGE JUST BROKE 😢 -->
  <div class="container-fluid">
    <section class="row d-flex justify-content-center mt-5">
      <div class="col-8">
        <!-- profile info -->
        <section class="row d-flex justify-content-center">
          <ProfileInfo />
        </section>
        <p>Vaults</p>
        <!-- profile vaults -->
        <section class="row">
          <div v-for="vault in vaults" :key="vault.id" class="col-4">
            <VaultCard :vault="vault" />
          </div>
        </section>
        <!-- profile keeps -->
        <section class="row">
          <p>keeps</p>
          <div v-for="keep in keeps" :key="keep.id" class="col-4">
            <KeepCard :keep="keep" />
          </div>
        </section>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue';
import { AppState } from '../AppState';
import ProfileInfo from '../components/ProfileInfo.vue';
import VaultCard from '../components/VaultCard.vue';
import KeepCard from '../components/KeepCard.vue';
import { accountService } from '../services/AccountService';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';
export default {
  setup() {
    watchEffect(() => {
      AppState.account.id
      clearAppState()
      setAccountInAppState(),
        // getAccountVaults(),
        getActiveKeeps()
    })
    function clearAppState() {
      vaultsService.clearAppState()
    }
    function setAccountInAppState() {
      accountService.setAccountInAppState()
    }
    async function getAccountVaults() {
      try {
        await vaultsService.getAccountVaults()
      } catch (error) {
        Pop.error()
      }
    }
    async function getActiveKeeps() {
      try {

        await keepsService.getProfileKeeps(AppState.account.id)
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.accountVaults),
    };
  },
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
