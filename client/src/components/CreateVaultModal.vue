<template>
    <div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="createVaultModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content bg-gray">
                <div class="container-fluid ">
                    <section class="row">
                        <div class="col-12 p-3">
                            <h2 class="oxygen text-color">Add your vault</h2>
                            <div title="close" class="position">
                                <i class="mdi mdi-close  fs-4 px-3"></i>
                            </div>
                        </div>
                        <div class="col-12 p-3">
                            <form @submit.prevent="editOrCreate()">
                                <div class="mb-3">
                                    <label for="name" class="form-label">Title</label>
                                    <input required maxlength="20" v-model="editable.name" placeholder="Title..."
                                        type="text" class="form-control bg-gray" id="vaultNameHelp"
                                        aria-describedby="vaultNameHelp">
                                </div>
                                <div class="mb-5">
                                    <label for="img" class="form-label">Image Url</label>
                                    <input required maxlength="500" v-model="editable.img" placeholder="Image Url..."
                                        type="url" class="form-control bg-gray" id="vaultImgHelp"
                                        aria-describedby="vaultImgHelp">
                                </div>
                                <div>
                                    <textarea required maxlength="500" v-model="editable.description"
                                        placeholder="Vault Description..." class="w-100 mt-5 bg-gray"></textarea>
                                </div>
                                <div class="text-end mt-4">
                                    <div id="emailHelp" class="form-text">Private Vaults can only be seen by you
                                    </div>
                                    <div class="form-check d-flex justify-content-end">

                                        <input v-model="editable.isPrivate" class="form-check-input bg-gray" type="checkbox"
                                            id="flexCheckDefault">
                                        <label class="form-check-label ms-2" for="flexCheckDefault">
                                            Make Vault Private?
                                        </label>
                                    </div>
                                    <button v-if="isEditing == false" type="submit"
                                        class="btn px-5 btn-dark text-white">Create Vault</button>
                                    <button v-else type="submit" class="btn px-5 btn-dark text-white">Save Vault</button>
                                </div>
                            </form>
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref, watchEffect, onUnmounted, watch, VueElement } from 'vue';
import Pop from '../utils/Pop';
// import { logger } from '../utils/Logger';
import { vaultsService } from '../services/VaultsService';
import { useRouter } from 'vue-router';
import { Modal } from 'bootstrap';
import { logger } from '../utils/Logger';
export default {
    setup() {
        const editable = ref({})
        const router = useRouter()
        watchEffect(() => {
            if (AppState.isEditing) {
                editable.value = { ...AppState.activeVault }
            }
            onMounted(() => {
                var myModalEl = document.getElementById('createVaultModal')
                myModalEl.addEventListener('hidden.bs.modal', function (event) {
                    vaultsService.changeEditStatusFalse()
                    editable.value = {}
                })
            })

        })
        return {
            editable,
            router,
            isEditing: computed(() => AppState.isEditing),
            async editOrCreate() {
                if (AppState.isEditing == false) {
                    try {
                        if (!editable.isPrivate) {
                            editable.isPrivate = false
                        }

                        const vault = await vaultsService.createVault(editable.value)
                        Modal.getOrCreateInstance('#createVaultModal').hide()
                        editable.value = {}
                        await router.push({ name: 'Vault', params: { vaultId: vault.id } })
                    } catch (error) {
                        Pop.error(error)
                    }

                }
                if (AppState.isEditing) {

                    try {
                        logger.log(editable.value)
                        await vaultsService.editVault(editable.value, AppState.activeVault.id)
                        Modal.getOrCreateInstance('#createVaultModal').hide()
                    } catch (error) {
                        Pop.error(error)
                    }
                }

            }
        }
    }
};
</script>


<style lang="scss" scoped>
textarea {
    resize: none;
}

.bg-gray {
    background-color: #F9F6FA;
}

.text-color {
    color: #636E72;
}

.position {
    position: absolute;
    top: 1%;
    right: 0%;
}
</style>