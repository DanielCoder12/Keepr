<template>
    <!-- FIXME IF IMAGE HEIGHT IS A LOT IT GETS WONKY, FIX THAT PLS -->
    <div v-if="keep.id" class="modal fade modal-xl" id="activeKeepModal" tabindex="-1" role="dialog"
        aria-labelledby="activeKeepModal" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">

                <div class="modal-body p-0">
                    <div class="container-fluid">
                        <section class="row">
                            <!-- TODO ADD DISAPPEARING X ON MOBILE -->
                            <div class="col-12 col-md-6  p-0"> <img class=" rounded-start img-fluid" :src="keep.img" alt="">
                            </div>
                            <div class="col-12 col-md-6  d-flex flex-column justify-content-between">
                                <div>
                                    {{ keep.views }}
                                    {{ keep.kept }}
                                </div>
                                <div>
                                    {{ keep.name }}
                                    {{ keep.description }}

                                </div>
                                <div class="d-flex justify-content-between">
                                    <div class="d-flex ">
                                        <form @submit.prevent="saveKeep()" action="">
                                            <!-- FIXME MAKE IT SO IF KEEP IS ALREADY IN VAULT IT DOESNT SHOW ON THE FORM -->
                                            <select v-model="selectedVault" class="form-select"
                                                aria-label="Default select example">
                                                <option v-for=" vault in vaults" :value="vault.id" :key="vault.id">{{
                                                    vault.name }}</option>

                                            </select>
                                            <!-- FIXME STYLE THIS BUTTON, MODAL AND INPUT -->
                                            <button type="submit" class="btn btn-info">Save</button>
                                        </form>
                                    </div>
                                    <div>

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
export default {
    setup() {
        const selectedVault = ref('')
        return {
            selectedVault,
            keep: computed(() => AppState.activeKeep),
            vaults: computed(() => AppState.accountVaults),
            async saveKeep() {
                try {
                    await vaultsService.saveKeepToVault(selectedVault.value, this.keep.id)
                    Pop.success("Keep Kept")
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



.profile-img {
    height: 3rem;
    aspect-ratio: 1/1;
    object-fit: cover;
    object-position: center;
}
</style>