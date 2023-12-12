<template>
    <!-- TODO MAKE LOOK MORE LIKE MOCK -->
    <!-- FIXME IT DOESNT WORK YET -->
    <div class="modal fade " id="createKeepModal" tabindex="-1" aria-labelledby="createKeepModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content bg-gray">
                <div class="container-fluid">
                    <section class="row">
                        <div class="col-12 p-3">
                            <h2 class="oxygen text-color">Add your keep</h2>
                            <div class="position">
                                <i class="mdi mdi-close fs-4 p-3 "></i>
                            </div>
                        </div>
                        <div class="col-12 p-3">
                            <form @submit.prevent="createKeep()">
                                <div class="mb-3">
                                    <label for="name" class="form-label">Title</label>
                                    <input required maxlength="30" v-model="editable.name" placeholder="Title..."
                                        type="text" class="form-control bg-gray" id="keepNameHelp"
                                        aria-describedby="keepNameHelp">
                                </div>
                                <div class="mb-5">
                                    <label for="img" class="form-label">Image Url</label>
                                    <input required maxlength="500" v-model="editable.img" placeholder="Image Url..."
                                        type="url" class="form-control bg-gray" id="keepImgHelp"
                                        aria-describedby="keepImgHelp">
                                </div>
                                <div>
                                    <textarea required maxlength="500" v-model="editable.description"
                                        placeholder="Keep Description..." class="w-100 bg-gray mt-5"></textarea>
                                </div>
                                <div class="text-end mt-4">
                                    <button type="submit" class="btn btn-dark text-white">Create</button>
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
import { useRoute } from 'vue-router';
import { keepsService } from '../services/KeepsService';
import { Modal } from 'bootstrap';
export default {
    setup() {
        const editable = ref({})
        const route = useRoute()
        return {
            editable,
            route,
            // keeps should only be added if on the home page or account page
            async createKeep() {
                try {
                    logger.log(editable.value)
                    logger.log(route.fullPath)
                    await keepsService.createKeep(editable.value, route.fullPath)
                    Modal.getOrCreateInstance('#createKeepModal').hide()
                    editable.value = {}
                    Pop.success("keep Created")
                } catch (error) {
                    Pop.error(error)
                }
            }

        }
    }
};
</script>


<style lang="scss" scoped>
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



textarea {
    resize: none;
}
</style>