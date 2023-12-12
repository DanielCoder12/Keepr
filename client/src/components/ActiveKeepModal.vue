<template>
    <!-- FIXME IF IMAGE HEIGHT IS A LOT IT GETS WONKY, FIX THAT PLS -->
    <!-- FIXME MOBILE VIEW -->
    <div v-if="keep.id" class="modal fade modal-xl" id="activeKeepModal" tabindex="-1" role="dialog"
        aria-labelledby="activeKeepModal" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">

                <div class="modal-body p-0">
                    <div class="container-fluid">
                        <section class="row">
                            <!-- TODO ADD DISAPPEARING X ON MOBILE -->
                            <div class="col-12 col-md-6  p-0">
                                <img class=" rounded-start img-fluid keep-img" :src="keep.img" alt="">
                                <i data-bs-toggle="modal" data-bs-target="#activeKeepModal"
                                    class="mdi d-flex d-md-none fs-3 x-position mdi-close"></i>
                            </div>
                            <div
                                class="col-12 col-md-6 py-md-4 pt-md-4 pb-md-0 bg-cream rounded-end d-flex flex-column justify-content-between">
                                <div class="d-flex justify-content-center font-gray">
                                    <p class="me-2 mb-0 fs-5 pt-2">
                                        <i class="mdi mdi-eye"></i>
                                        {{ keep.views }}

                                    </p>
                                    <p class="ms-2 mb-0 fs-5 pt-2">
                                        <i class="mdi mdi-alpha-k-box-outline"></i>
                                        {{ keep.kept }}
                                    </p>
                                </div>
                                <div>
                                    <p class="text-center mb-0 text-break marko-one fs-1 fw-bold">
                                        {{ keep.name }}
                                    </p>
                                    <p class="font-gray mb-0 text-break">
                                        {{ keep.description }}
                                    </p>

                                </div>
                                <div class="d-flex pb-3 mt-3 mt-md-0 justify-content-between">
                                    <div class="d-flex ">
                                        <!-- FIXME MAKE LOOK LIKE MOCK -->
                                        <div v-if="vault.creatorId == account.id && $route.name == 'Vault'">
                                            <button @click="removeKeepFromVault(keep.vaultKeepId)"
                                                class="text-white btn btn-danger"><i class="mdi mdi-close-circle"></i>
                                                Remove</button>
                                        </div>
                                        <!-- FIXME really ugly -->
                                        <div v-else>

                                            <form v-if="account.id && filteredVaults.length > 0"
                                                @submit.prevent="saveKeep()" class="d-flex ">
                                                <!-- FIXME MAKE IT SO IF KEEP IS ALREADY IN VAULT IT DOESNT SHOW ON THE FORM -->
                                                <select role="button" v-model="selectedVault"
                                                    class="bg-cream dropdown rounded oxygen "
                                                    aria-label="Default select example">
                                                    <option v-for=" vault in filteredVaults" class="oxygen"
                                                        :value="vault.id" :key="vault.id">

                                                        {{ vault.name }}
                                                    </option>

                                                </select>
                                                <!-- FIXME STYLE THIS BUTTON, MODAL AND INPUT -->
                                                <button :disabled="selectedVault == ''" type="submit"
                                                    class="btn btn-secondary text-white">Save</button>
                                            </form>
                                        </div>
                                    </div>
                                    <div class="d-flex align-items-center" role="button" v-if="keep.creatorId == account.id"
                                        @click="redirectToAccountPage()">

                                        <img class="rounded-circle border border-dark profile-img shadow"
                                            :src="keep.creator.picture" :alt="keep.creator.name">
                                        <p class="mb-0 fw-bold text-break ps-2 oxygen ">
                                            {{ keep.creator.name }}
                                        </p>
                                    </div>
                                    <div class="d-flex align-items-center" role="button" v-else
                                        @click="redirectToProfilePage(keep.creator.id)">

                                        <img class="rounded-circle border border-dark  profile-img shadow"
                                            :src="keep.creator.picture" :alt="keep.creator.name">
                                        <p class="mb-0 oxygen text-break ps-2">
                                            {{ keep.creator.name }}
                                        </p>
                                    </div>

                                </div>

                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';
import { useRouter } from 'vue-router';
import { Modal } from 'bootstrap';
export default {
    setup() {
        const router = useRouter()
        const selectedVault = ref('')
        return {
            router,
            selectedVault,
            keep: computed(() => AppState.activeKeep),
            vault: computed(() => AppState.activeVault),
            vaults: computed(() => AppState.accountVaults),
            account: computed(() => AppState.account),
            vaultsWithKeep: computed(() => AppState.vaultsWithActiveKeep),
            filteredVaults: computed(() => AppState.accountVaults.filter(v => AppState.vaultsWithActiveKeep.find(vw => v.id == vw.vaultId) == null)),
            async saveKeep() {
                try {
                    await vaultsService.saveKeepToVault(selectedVault.value, this.keep.id)
                    this.keep.kept++
                    Pop.success("Keep Kept")
                } catch (error) {
                    Pop.error(error)
                }
            },
            redirectToAccountPage() {
                Modal.getOrCreateInstance('#activeKeepModal').hide()
                router.push({ path: '/account' })
            },
            redirectToProfilePage(profileId) {
                Modal.getOrCreateInstance('#activeKeepModal').hide()
                router.push({ name: 'Profile', params: { profileId } })
            },
            async removeKeepFromVault(vaultKeepId) {
                try {
                    const yes = await Pop.confirm('Are you sure you would like to remove this keep from your vault?')
                    if (!yes) {
                        return
                    }
                    await vaultsService.removeKeepFromVault(vaultKeepId)
                    Pop.success('keep removed from vault')
                    Modal.getOrCreateInstance('#activeKeepModal').hide()
                } catch (error) {
                    Pop.error(error)
                }
            }

        }
    }
};
</script>


<style lang="scss" scoped>
.modal-height {
    height: 60vh;
}

.dropdown {
    border-top: none;
    border-left: none;
    border-right: none;
    width: 12rem;
    height: 2rem;
}

@media(max-width: 767px) {
    .dropdown {
        width: 6rem;
    }
}

.x-position {
    position: absolute;
    top: 1%;
    right: 2%;
}

.bg-cream {
    background-color: #FEF6F0;
}

.btn-secondary {
    background-color: #877A8F;
    height: 2rem;
    display: flex;
    align-items: center;
}

.keep-img {
    width: 100%;
    height: 35rem;
    object-fit: cover;
    object-position: center;
}

@media(max-width: 767px) {
    .keep-img {
        height: 50vh;
        width: 100%;
    }
}


.profile-img {
    height: 2.5rem;
    aspect-ratio: 1/1;
    object-fit: cover;
    object-position: center;
}
</style>