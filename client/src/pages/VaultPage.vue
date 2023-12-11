<template>
    <div class="container-fluid">
        <section class="row d-flex justify-content-center">
            <div class="col-3 mt-4">
                <div class="vault-img d-flex flex-column justify-content-end rounded text-white text-center"
                    :style="{ backgroundImage: `url(${vault.img})` }">
                    <h1>{{ vault.name }}</h1>
                    <p>by {{ vault.creator.name }}</p>
                </div>
                <div class="text-end">
                    <i class="mdi mdi-dots-horizontal"></i>
                </div>
                <div class="text-center">
                    {{ keeps.length }} Keeps
                </div>
            </div>
            <div>
                <div class="col-8">
                    <section class="row">
                        <div v-for="keep in keeps" :key="keep.id" class="col-4">
                            <KeepCard />
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </div>
</template>


<script>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect } from 'vue';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import KeepCard from '../components/KeepCard.vue';
export default {
    setup() {
        const route = useRoute();
        watchEffect(() => {
            route,
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
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.keeps)
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