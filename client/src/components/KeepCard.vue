<template>
    <div role="button" @click="setActiveKeep(keep.id)" class="rounded" :style="{ backgroundImage: `url(${keep.img})` }">
        <div class="d-flex justify-content-between">
            <p>{{ keep.name }}</p>
            <img role="button" title="see account" @click.stop="redirectToAccountPage()" v-if="keep.creatorId == account.id"
                class="img-fluid rounded-circle" :src="keep.creator.picture" :alt="keep.creator.name">
            <img v-else role="button" title="see profile" @click.stop="redirectToProfilePage(keep.creatorId)"
                class="img-fluid rounded-circle" :src="keep.creator.picture" :alt="keep.creator.name">
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import { useRouter } from 'vue-router';
import { router } from '../router';
export default {
    props: {
        keep: { type: Keep }
    },
    setup() {
        const router = useRouter()

        return {
            router,
            account: computed(() => AppState.account),
            async setActiveKeep(keepId) {
                try {
                    logger.log(keepId)
                    await keepsService.setActiveKeep(keepId)
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
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.div-img {}
</style>