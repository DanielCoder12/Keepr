<template>
    <div role="button" @click="RedirectToVaultPage(vault.id)" class="vault-img rounded mb-4 d-flex align-items-end"
        :style="{ backgroundImage: `url(${vault.img})` }">
        <div class="w-100">
            <div v-if="account.id == vault.creatorId" class=" d-flex">
                <i class="mdi mdi-close-circle-outline fs-5 text-danger delete-position"></i>
            </div>
            <div class="d-flex justify-content-between">
                <p class="m-2 text-white">
                    {{ vault.name }}
                </p>
                <i v-if="vault.isPrivate" class="mdi mdi-lock m-2"></i>
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
}
</style>