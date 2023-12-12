<template>
    <!-- <div role="button" @click="setActiveKeep(keep.id)" class="rounded" :style="{ backgroundImage: `url(${keep.img})` }">
        <div class="d-flex justify-content-between">
            <p>{{ keep.name }}</p>
            <img role="button" title="see account" @click.stop="redirectToAccountPage()" v-if="keep.creatorId == account.id"
                class="img-fluid rounded-circle" :src="keep.creator.picture" :alt="keep.creator.name">
            <img v-else role="button" title="see profile" @click.stop="redirectToProfilePage(keep.creatorId)"
                class="img-fluid rounded-circle" :src="keep.creator.picture" :alt="keep.creator.name">
        </div>
    </div> -->
    <!-- FIXME FIX BROKEN IMAGES -->
    <div role="button" @click.stop="setActiveKeep(keep.id)" class="image ">
        <img v-if="didLoad" @error="loadFailed(keep.id)" class="w-100 rounded card-shadow" :src="keep.img" alt="">
        <img v-else class="w-100 rounded card-shadow" src="https://www.drupal.org/files/project-images/broken-image.jpg"
            alt="">
        <div class="d-flex px-3 w-100 position justify-content-between">

            <p class="align-items-center text-break d-flex mb-0 marko-one font-shadow fs-3">{{ keep.name }}</p>
            <span v-if="keep.creator.id == account.id" :title="keep.creator.name"
                class="text-end d-none d-md-flex align-items-center"><img role="button"
                    @click.stop="redirectToAccountPage()" class="creator-img border shadow border-dark rounded-circle"
                    :src="keep.creator.picture" :alt="keep.creator.picture"></span>
            <span v-else role="button" :title="keep.creator.name" @click.stop="redirectToProfilePage(keep.creatorId)"
                class="text-end d-none d-md-flex align-items-center"><img role="button"
                    class="creator-img border border-dark shadow rounded-circle" :src="keep.creator.picture"
                    :alt="keep.creator.name"></span>
        </div>
        <i @click.stop="deleteKeep(keep.id)" v-if="keep.creator.id == account.id" role="button" title="delete keep"
            class="mdi close-position text-shadow mdi-close-circle fs-4 text-danger"></i>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { Keep } from '../models/Keep';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService';
// import { logger } from '../utils/Logger';
import { useRouter } from 'vue-router';
import { router } from '../router';
import { vaultsService } from '../services/VaultsService';
export default {
    props: {
        keep: { type: Keep }
    },
    setup() {
        const router = useRouter()
        let didLoad = ref(true)

        return {
            didLoad,
            router,
            account: computed(() => AppState.account),
            async setActiveKeep(keepId) {
                try {
                    // logger.log(keepId)
                    await keepsService.setActiveKeep(keepId)
                    await vaultsService.getVaultIdsKeepsAreIn(keepId)
                    Modal.getOrCreateInstance('#activeKeepModal').show()
                } catch (error) {
                    Pop.error(error)
                }
            },
            redirectToAccountPage() {
                router.push({ path: '/account' })
            },
            redirectToProfilePage(profileId) {
                router.push({ name: 'Profile', params: { profileId } })
            },
            async deleteKeep(keepId) {
                try {
                    const yes = await Pop.confirm('Are you sure you would like to delete this keep?')
                    if (!yes) {
                        return
                    }
                    await keepsService.deleteKeep(keepId)
                } catch (error) {
                    Pop.error(error)
                }
            },
            loadFailed() {
                didLoad.value = false
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.creator-img {
    height: 3rem;
    aspect-ratio: 1/1;
}

img:hover {
    transform: scale(1.03);
}

img {
    transition: .25s;
}

.close-position {
    position: absolute;
    top: -1rem;
    right: -.5rem;
    text-shadow: 0px 0px 3px rgb(0, 0, 0);
}


.image {
    position: relative;
    text-align: center;
}

.position {
    position: absolute;
    bottom: 0rem;
}
</style>