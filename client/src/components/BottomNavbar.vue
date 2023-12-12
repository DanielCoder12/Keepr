<template>
    <div class="d-flex justify-content-between">
        <div class="p-2">
            <!-- FIXME MAKE LOGO SMALLER SOMEHOW -->
            <router-link class="navbar-brand d-flex common-width " :to="{ name: 'Home' }">
                <div class="d-flex ps-2 pe-4 flex-column fw-bold  logo-border rounded">
                    <p class="mb-0 no-select marko-one">the</p>
                    <p class="mb-0 no-select marko-one">keepr</p>
                    <p class="mb-1 no-select marko-one">co.</p>
                </div>
            </router-link>
        </div>
        <div v-if="account.id" class="dropdown d-flex align-items-center justify-content-center common-width">
            <!-- FIXME BUTTON COLOR FAILS POSTMAN 😢 -->
            <button class="btn btn-secondary shadow fw-bold btn-purple" type="button" id="CreateButtonDropDownMobile"
                data-bs-toggle="dropdown" aria-expanded="false">
                Create
            </button>
            <ul class="dropdown-menu" aria-labelledby="CreateButtonDropDownMobile">
                <li data-bs-toggle="modal" data-bs-target="#createKeepModal"><a class="dropdown-item">New
                        Keep</a></li>
                <li data-bs-toggle="modal" data-bs-target="#createVaultModal"><a class="dropdown-item">New
                        Vault</a></li>
            </ul>
        </div>
        <div class="common-width d-flex align-items-center justify-content-end pe-2">
            <Login />
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Login from './Login.vue';
export default {
    setup() {
        return {
            account: computed(() => AppState.account)
        };
    },
    components: { Login }
};
</script>


<style lang="scss" scoped>
top-border {
    background: #FEF6F0;
}

.logo-border {
    border: 2px solid black;
}

p {
    height: 1.25rem;
}

.btn-secondary {
    background-color: #877A8F;
    border: none;
}

.common-width {
    width: 7rem;
}
</style>