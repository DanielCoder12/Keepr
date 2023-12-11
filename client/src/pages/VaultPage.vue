<template>
    <!-- FIXME THIS PAGE BROKEN TOO 😢 -->
    <div class="container-fluid">
        <section class="row d-flex justify-content-center">
            <div class="col-3 mt-4">
                <div class="vault-img font-shadow d-flex flex-column justify-content-end rounded text-white text-center"
                    :style="{ backgroundImage: `url(${vault.img})` }">
                    <div>

                        <i v-if="vault.creatorId == account.id" role="button" title="vault options"
                            class="mdi mdi-dots-horizontal position" data-bs-toggle="dropdown" aria-haspopup="true"
                            aria-expanded="false"></i>
                        <div class="dropdown-menu dropdown-position" aria-labelledby="dropdownMenuButton">
                            <a @click="deleteVault(vault.id)" class="dropdown-item text-danger">Delete Vault</a>
                        </div>
                    </div>
                    <div class="quando">
                        <h1>{{ vault.name }}</h1>
                        <p>by {{ vault.creator?.name }}</p>
                    </div>

                </div>

                <div v-if="vault.creatorId == account.id" class="text-end">
                    <!-- FIXME MAKE THIS WHERE YOU DELETE VAULT -->
                    <!-- <div class="dropdown"> -->
                    <!-- <button class="btn" type="button" id="dropdownMenuButton"> -->
                    <!-- </button> -->
                    <!-- </div> -->
                </div>
                <div class="text-center oxygen d-flex justify-content-center">
                    <p class="px-3 mt-3 rounded-pill justify-content-center d-flex fs-4 bg-purple">
                        {{ keeps.length }} Keep<span v-if="keeps.length != 1">s</span>
                    </p>
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
.position {
    color: black;
    position: relative;
    top: 7.5rem;
    left: 10rem;
    text-shadow: none;
}

.dropdown-position {
    position: relative;
    top: 10rem;
    left: 10rem;
}

.vault-img {
    height: 11rem;
    object-fit: cover;
    object-position: center;
    background-position: center;
    background-repeat: none;
    width: auto;
}

.bg-purple {
    background-color: #DED6E9;
}
</style>