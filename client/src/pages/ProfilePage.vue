<template>
    <div class="container-fluid">
        <section class="row d-flex justify-content-center mt-3 mb-5 mb-md-3 mt-md-5">
            <div class="col-12 col-md-8">
                <!-- profile info -->
                <section class="row d-flex justify-content-center">
                    <ProfileInfo />
                </section>
                <p class="fs-1 fw-bold oxygen mt-md-4 mt-0">Vaults</p>
                <!-- profile vaults -->
                <section class="row">
                    <div v-for="vault in vaults" :key="vault.id" class="col-12 col-md-4">
                        <VaultCard :vault="vault" />
                    </div>
                </section>
                <!-- profile keeps -->
                <section class="row ">
                    <div class="col-12">

                        <p class="fs-1 fw-bold oxygen mt-3">keeps</p>

                        <div class="masonry-with-columns">

                            <div v-for="keep in keeps" :key="keep.id" class="col-4">
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
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch, watchEffect } from 'vue';
import Pop from '../utils/Pop';
import { profileService } from '../services/ProfileService'
import { logger } from '../utils/Logger';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService'
import KeepCard from '../components/KeepCard.vue';
import VaultCard from '../components/VaultCard.vue';
import ProfileInfo from '../components/ProfileInfo.vue';

export default {
    setup() {
        const route = useRoute();
        watchEffect(() => {
            route;
            clearAppState()
            getProfile(),
                getProfileKeeps(),
                getProfileVaults();
        });
        function clearAppState() {
            vaultsService.clearAppState()
        }
        async function getProfile() {
            try {
                await profileService.getProfile(route.params.profileId);
            }
            catch (error) {
                Pop.error(error)
            }
        }
        async function getProfileKeeps() {
            try {
                await keepsService.getProfileKeeps(route.params.profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getProfileVaults() {
            try {
                await vaultsService.getProfileVaults(route.params.profileId)
            }
            catch (error) {
                Pop.error(error);
            }
        }
        return {
            profile: computed(() => AppState.activeProfile),
            vaults: computed(() => AppState.vaults),
            keeps: computed(() => AppState.keeps)
        };
    },
    components: { KeepCard, VaultCard, ProfileInfo }
};
</script>


<style lang="scss" scoped></style>