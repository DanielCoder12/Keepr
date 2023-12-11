<template>
  <div class="container-fluid">
    <section class="row d-flex justify-content-center">
      <div class="col-11">
        <section class="row ">
          <div class="col-12 mt-4">
            <div class="masonry-with-columns">

              <div class="" v-for="keep in keeps" :key="keep.id">
                <KeepCard :keep="keep" />

              </div>
            </div>
          </div>
        </section>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import KeepCard from '../components/KeepCard.vue';

export default {
  setup() {
    onMounted(() => {
      getKeeps();
    });
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss"></style>
