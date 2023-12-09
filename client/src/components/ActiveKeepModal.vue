<template>
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
                                <div>
                                    <img class="rounded-circle shadow" :src="keep.creator.picture" :alt="keep.creator.name">
                                    {{ keep.creator.name }}
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
import { computed, reactive, onMounted } from 'vue';
export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep)
        }
    }
};
</script>


<style lang="scss" scoped>
.modal-height {
    height: 60vh;
}
</style>