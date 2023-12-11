<template>
    <div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="createVaultModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content bg-gray">
                <div class="container-fluid ">
                    <section class="row">
                        <div class="col-12 p-3">
                            <h2>Add your vault</h2>
                            <div class="position">
                                <i class="mdi mdi-close  fs-4 px-3"></i>
                            </div>
                        </div>
                        <div class="col-12 p-3">
                            <form @submit.prevent="createVault()">
                                <div class="mb-3">
                                    <label for="name" class="form-label">Title</label>
                                    <input required maxlength="50" v-model="editable.name" placeholder="Title..."
                                        type="text" class="form-control" id="name" aria-describedby="nameHelp">
                                </div>
                                <div class="mb-5">
                                    <label for="img" class="form-label">Image Url</label>
                                    <input required maxlength="500" v-model="editable.img" placeholder="Image Url..."
                                        type="url" class="form-control" id="img" aria-describedby="imgHelp">
                                </div>
                                <div>
                                    <textarea required maxlength="500" v-model="editable.description"
                                        placeholder="Vault Description..." class="w-100 mt-5"></textarea>
                                </div>
                                <div class="text-end mt-4">
                                    <div id="emailHelp" class="form-text">Private Vaults can only be seen by you
                                    </div>
                                    <div class="form-check d-flex justify-content-end">

                                        <input v-model="editable.isPrivate" class="form-check-input" type="checkbox"
                                            value="" id="flexCheckDefault">
                                        <label class="form-check-label ms-2" for="flexCheckDefault">
                                            Make Vault Private?
                                        </label>
                                    </div>
                                    <button type="submit" class="btn px-5 btn-dark text-white">Create Vault</button>
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
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { vaultsService } from '../services/VaultsService';
import { useRouter } from 'vue-router';
import { Modal } from 'bootstrap';
export default {
    setup() {
        const editable = ref({})
        const router = useRouter()
        return {
            editable,
            router,
            async createVault() {
                try {
                    logger.log(editable.value)
                    const vault = await vaultsService.createVault(editable.value)
                    Modal.getOrCreateInstance('#createVaultModal').hide()
                    editable.value = {}
                    await router.push({ name: 'Vault', params: { vaultId: vault.id } })
                } catch (error) {
                    Pop.error(error)
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

.position {
    position: absolute;
    top: 1%;
    right: 0%;
}
</style>