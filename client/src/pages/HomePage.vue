<template>
  <div class="container-fluid">
    <section class="row d-flex justify-content-center">
      <div class="col-11">
        <section class="row ">
          <!-- <div class="col-12"> -->
          <!-- <div class="masonry-with-columns"> -->

          <div class="col-4" v-for="keep in keeps" :key="keep.id">
            <KeepCard :keep="keep" />

            <!-- </div> -->
            <!-- </div> -->
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

<style scoped lang="scss">
// .masonry-with-columns {
//   columns: 4 300px;
//   column-gap: 1rem;

//   div {
//     width: 150px;
//     background: #EC985A;
//     color: white;
//     margin: 0 1rem 1rem 0;
//     display: inline-block;
//     width: 100%;
//     // text-align: center;
//     // font-family: system-ui;
//     // font-weight: 900;
//     font-size: 2rem;
//   }

//   @for $i from 1 through 36 {
//     div:nth-child(#{$i}) {
//       $h: (
//         random(400) + 100) + px;
//       height: $h;
//       line-height: $h;
//     }
// }
// }
</style>
