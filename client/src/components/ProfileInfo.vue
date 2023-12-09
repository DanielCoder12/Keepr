<template>
    <!-- FIXME IF NO BACKGROUND IMAGE MAKE LOOK BETTER -->
    <!-- FIXME MAKE LOOK GOOD ON MOBILE -->
    <div class="col-11 ">
        <div class="background-img bg-dark rounded text-center" :style="{ backgroundImage: `url(${profile.coverImg})` }">
            <div class="position">
                <img class="rounded-circle profile-img" :src="profile.picture" alt="profile picture">
                <p class=" mb-0">
                    {{ profile.name }}
                </p>
                <p class=" mb-0">
                    {{ vaults.length }} Vaults | {{ keeps.length }} Keeps
                </p>
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
            profile: computed(() => AppState.activeProfile),
            vaults: computed(() => AppState.vaults),
            keeps: computed(() => AppState.keeps)
        }
    }
};
</script>


<style lang="scss" scoped>
.background-img {
    height: 25rem;
    margin-bottom: 7rem;
}

.position {
    position: relative;
    top: 21rem;
}

.profile-img {
    height: 8rem;
    aspect-ratio: 1/1;
    object-fit: cover;
    object-position: center;
}
</style>