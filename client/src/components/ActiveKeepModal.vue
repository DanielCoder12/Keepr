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
                            </div>
                            <div class="col-12 col-md-6 py-md-4 px-md-5  d-flex flex-column justify-content-between">
                                <div class="d-flex justify-content-center font-gray">
                                    <p class="me-2 fs-5">
                                        <i class="mdi mdi-eye"></i>
                                        {{ keep.views }}

                                    </p>
                                    <p class="ms-2 fs-5">
                                        <i class="mdi mdi-alpha-k-box-outline"></i>
                                        {{ keep.kept }}
                                    </p>
                                </div>
                                <div>
                                    <p class="text-center marko-one fs-1 fw-bold">
                                        {{ keep.name }}
                                    </p>
                                    <p class="font-gray">
                                        {{ keep.description }}
                                    </p>

                                </div>
                                <div class="d-flex justify-content-between">
                                    <div class="d-flex ">
                                        <!-- FIXME really ugly -->
                                        <form v-if="account.id" @submit.prevent="saveKeep()" class="d-flex">
                                            <!-- FIXME MAKE IT SO IF KEEP IS ALREADY IN VAULT IT DOESNT SHOW ON THE FORM -->
                                            <select v-model="selectedVault" class="form-select oxygen "
                                                aria-label="Default select example">
                                                <option v-for=" vault in vaults" class="oxygen" :value="vault.id"
                                                    :key="vault.id">{{
                                                        vault.name }}</option>

                                            </select>
                                            <!-- FIXME STYLE THIS BUTTON, MODAL AND INPUT -->
                                            <button type="submit" class="btn btn-purple text-white">Save</button>
                                        </form>
                                    </div>
                                    <div class="d-flex align-items-center" role="button" v-if="keep.creatorId == account.id"
                                        @click="redirectToAccountPage()">

                                        <img class="rounded-circle profile-img shadow" :src="keep.creator.picture"
                                            :alt="keep.creator.name">
                                        <p class="mb-0 oxygen ">
                                            {{ keep.creator.name }}
                                        </p>
                                    </div>
                                    <div class="d-flex align-items-center" role="button" v-else
                                        @click="redirectToProfilePage()">

                                        <img class="rounded-circle profile-img shadow" :src="keep.creator.picture"
                                            :alt="keep.creator.name">
                                        {{ keep.creator.name }}
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
            vaults: computed(() => AppState.accountVaults),
            account: computed(() => AppState.account),
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
            }

        }
    }
};
</script>


<style lang="scss" scoped>
.modal-height {
    height: 60vh;
}

.btn-purple {
    background-color: #A277D9;
}

.keep-img {
    width: 100%;
    height: 35rem;
    object-fit: cover;
    object-position: center;
}



.profile-img {
    height: 3rem;
    aspect-ratio: 1/1;
    object-fit: cover;
    object-position: center;
}
</style>