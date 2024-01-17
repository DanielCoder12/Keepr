<template>
    <div class="modal fade" id="editAccountModal" tabindex="-1" role="dialog" aria-labelledby="editAccountModalLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <form @submit.prevent="editAccount()">

                    <div class="modal-header">
                        <h5 class="modal-title">Edit Account</h5>
                        <button type="button" class="btn-close" title="close" data-bs-dismiss="modal"
                            aria-label="Close"></button>
                    </div>
                    <!-- FIXME MAKE MAX LENGTHS NOT INSANELY LONG -->
                    <div class="modal-body">
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input required maxlength="225" v-model="editable.name" type="text" class="form-control"
                                id="name" aria-describedby="nameHelp">
                        </div>
                        <div class="mb-3">
                            <label for="picture" class="form-label">Profile Picture</label>
                            <input required maxlength="225" v-model="editable.picture" type="url" class="form-control"
                                id="picture" aria-describedby="pictureHelp">
                        </div>
                        <div class="mb-3">
                            <label for="coverImg" class="form-label">Cover Image</label>
                            <input maxlength="500" v-model="editable.coverImg" type="url" class="form-control" id="coverImg"
                                aria-describedby="coverImgHelp">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-dark">Save</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref, watchEffect } from 'vue';
import { logger } from '../utils/Logger';
import { accountService } from '../services/AccountService';
import { Modal } from 'bootstrap';
import Pop from '../utils/Pop';
export default {
    setup() {
        const editable = ref({})
        watchEffect(() => editable.value = { ...AppState.account })
        return {
            editable,
            async editAccount() {
                try {

                    await accountService.editAccount(editable.value)
                    Modal.getOrCreateInstance('#editAccountModal').hide()
                    Pop.success('Account Edited')
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>