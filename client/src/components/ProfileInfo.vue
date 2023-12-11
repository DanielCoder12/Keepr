<template>
    <!-- FIXME IF NO BACKGROUND IMAGE MAKE LOOK BETTER -->
    <!-- FIXME MAKE LOOK GOOD ON MOBILE -->
    <div class="col-11 ">
        <div class="background-img rounded text-center"
            :style="{ backgroundImage: `url(${profile.coverImg ? profile.coverImg : 'https://www.fuller.edu/wp-content/uploads/2022/11/secondary-banner-placeholder.jpg'})` }">
            <div :class="{ 'accPosition': $route.name == 'Account', 'position': $route.name != 'Account' }">
                <div v-if="$route.name == 'Account'" class="text-end btn-position">
                    <!-- FIXME MAKE THIS OPEN EDIT ACCOUNT MODAL -->
                    <!-- <div class="dropdown"> -->
                    <!-- <button class="btn " type="button" id="accountDropdown" > -->
                    <i role="button" title="account options" class="mdi me-3  fs-4 mdi-dots-horizontal"
                        data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></i>
                    <!-- </button> -->
                    <div class="dropdown-menu" aria-labelledby="accountDropdown">
                        <a data-bs-toggle="modal" data-bs-target="#editAccountModal" class="dropdown-item">Edit
                            Account</a>
                        <!-- </div> -->
                    </div>
                </div>
                <img class="rounded-circle profile-img" :src="profile.picture" alt="profile picture">
                <p class=" mb-0 fs-1 oxygen">
                    {{ profile.name }}
                </p>
                <p class=" mb-0">
                    <span v-if="$route.name != 'Account'">{{ vaults.length }}</span> <span v-else>{{ accVaults.length
                    }}</span> Vault<span
                        v-if="vaults.length != 1 && $route.name == 'Account' || accVaults.length != 1 && $route.name != 'Account'">s</span>
                    | {{ keeps.length }} Keep<span v-if="keeps.length != 1">s</span>
                </p>
            </div>

        </div>
    </div>
</template>


<script>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {

    setup() {
        // route = useRoute()
        return {
            // route,
            // myRoute: computed(() => this.$route.name),
            accVaults: computed(() => AppState.accountVaults),
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
    margin-bottom: 9rem;
    object-fit: cover;
    object-position: center;
    background-position: center;
}

.btn-position {
    position: relative;
    top: 7rem;
}

@media(max-width: 767px) {
    .background-img {
        height: 13rem;
    }

    .btn-position {
        top: 6rem !important;
    }

    .position {
        top: 8rem !important;
    }

    .accPosition {
        top: 7.8rem !important;
    }

    .profile-img {
        height: 6rem !important;
    }
}

.position {
    position: relative;
    top: 20.8rem;
}

.accPosition {
    position: relative;
    top: 18.5rem;
}

.profile-img {
    height: 8rem;
    aspect-ratio: 1/1;
    object-fit: cover;
    object-position: center;
    border: 2px solid white;
    box-shadow: 0px 5px 5px rgba(0, 0, 0, 0.43);

}
</style>