<template>
    <div role="button" @click="RedirectToVaultPage(vault.id)"
        class="vault-img card-shadow rounded mb-4 d-flex align-items-end" :style="{ backgroundImage: `url(${vault.img})` }">
        <div class="w-100">

            <div class="d-flex justify-content-between">
                <p class="m-2 text-white text-break quando font-shadow fs-2">
                    {{ vault.name }}
                </p>
                <i v-if="vault.isPrivate"
                    class="mdi d-flex align-items-end text-white font-shadow fs-2 mdi-lock-outline m-2"></i>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault';
import { useRouter } from 'vue-router';
export default {
    props: {
        vault: { type: Vault }
    },
    setup() {
        const router = useRouter()
        return {
            router,
            account: (() => AppState.account),
            activeProfile: (() => AppState.activeProfile),
            RedirectToVaultPage(vaultId) {
                router.push({ name: 'Vault', params: { vaultId } })
            }
        }
    }
};
</script>


<style lang="scss" scoped>
// FIXME MAKE THIS STAY IN THE SAME POSITION NOT DEPENDANT ON SCREEN WIDTH ??MAYBE USE POSITION ABSOLUTE 
.delete-position {
    position: relative;
    top: -6.5rem;
    right: -18.9rem;
}

.vault-img {
    height: 10rem;
    width: auto;
    object-fit: cover;
    object-position: center;
    transition: .25s;
    background-position: center;
}

.vault-img:hover {
    transform: scale(1.03);
}
</style>