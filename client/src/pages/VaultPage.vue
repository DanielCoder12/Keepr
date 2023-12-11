<template>
    <!-- FIXME THIS PAGE BROKEN TOO 😢 -->
    <div class="container-fluid">
        <section class="row d-flex justify-content-center">
            <div class="col-3 mt-4">
                <div class="vault-img d-flex flex-column justify-content-end rounded text-white text-center"
                    :style="{ backgroundImage: `url(${vault.img})` }">
                    <h1>{{ vault.name }}</h1>
                    <p>by {{ vault.creator?.name }}</p>
                </div>

                <div v-if="vault.creatorId == account.id" class="text-end">
                    <!-- FIXME MAKE THIS WHERE YOU DELETE VAULT -->
                    <div class="dropdown">
                        <button class="btn" type="button" id="dropdownMenuButton" data-bs-toggle="dropdown"
                            aria-haspopup="true" aria-expanded="false">
                            <i class="mdi mdi-dots-horizontal"></i>
                        </button>
                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                            <a @click="deleteVault(vault.id)" class="dropdown-item text-danger">Delete Vault</a>

                        </div>
                    </div>
                </div>
                <div class="text-center">
                    {{ keeps.length }} Keep<span v-if="keeps.length != 1">s</span>
                </div>
            </div>
        </section>
        <section class="row d-flex justify-content-center">
            <!-- FIXME MASONRY ONLY GOES TO HALF OF ITS CONTAINER?? -->
            <div class="col-8">
                <section class="row ">
                    <div class="col-12 mt-4">
                        <div class="masonry-with-columns">

                            <div class="" v-for="keep in keeps" :key="keep.id">
                                <KeepCard :keep="keep" />

                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </section>
    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect } from 'vue';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import KeepCard from '../components/KeepCard.vue';
import { router } from '../router';
import { logger } from '../utils/Logger';
export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        onMounted(() => {
            // route,
            clearAppState();
            getVaultById();
            getKeepsByVaultId();
        });
        function clearAppState() {
            vaultsService.clearAppState();
        }
        async function getVaultById() {
            try {
                await vaultsService.getVaultById(route.params.vaultId);
            }
            catch (error) {
                Pop.error(error);
                // logger.log(error.response.data)
                if (error.response.data.includes('invalid id') || error.response.data == 'Private Vault') {
                    router.push({ path: '/' })
                }
            }
        }
        async function getKeepsByVaultId() {
            try {

                await vaultsService.getKeepsByVaultId(route.params.vaultId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        return {
            router,
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.keeps),
            account: computed(() => AppState.account),
            async deleteVault(vaultId) {
                try {
                    const yes = await Pop.confirm("Are you sure you would like to delete this Vault?")
                    if (!yes) {
                        return
                    }
                    await vaultsService.deleteVault(vaultId)
                    router.push({ path: '/' })
                    Pop.success('vault Deleted')
                } catch (error) {
                    Pop.error(error)
                }
            }
        };
    },
    components: { KeepCard }
};
</script>


<style lang="scss" scoped>
.vault-img {
    height: 10rem;
    object-fit: cover;
    object-position: center;
    background-repeat: none;
    width: auto;
}
</style>