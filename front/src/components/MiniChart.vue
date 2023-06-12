<template>
  <div
    class="mini-chart-wrap"
    :class="{ 'bg-dark': useCommonStore().darkMode }"
  >
    <nav class="nav-top">
      <div class="nav-btn-wrap">
        <q-btn
          type="button"
          v-for="(data, i) in navBtn"
          :key="i"
          :class="{ selected: navSelect === i }"
          @click="navSelect = i"
        >
          {{ data.text }}
        </q-btn>
      </div>
    </nav>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useCommonStore } from 'src/stores/common';

const navBtn = [
  {
    text: '마켓뎁스',
  },
  {
    text: '미니차트',
  },
];
const navSelect = ref(0);
</script>

<style scoped lang="scss">
.mini-chart-wrap {
  height: 37rem;
  background-color: #fff;
}

.nav-top {
  height: 4rem;
  background-color: #fff;
  border-bottom: 1px solid #eee;
  z-index: 1;
  display: flex;
  justify-content: space-between;

  .nav-btn-wrap {
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 100%;
    height: 100%;

    > button {
      width: 100%;
      height: 100%;
      font-weight: 700;

      &.selected {
        color: #0062df;
        border-bottom: 3px solid #0062df;
      }
    }
  }
}

.bg-dark {
  border: 1px solid $dark-component-border;

  .nav-top {
    background-color: $dark-component;
    border-bottom-color: $dark-component-border;
  }

  .mini-chart-wrap {
    background-color: $dark-component !important;
  }
}

@media (max-width: 992px) {
  .mini-chart-wrap {
    display: none;
  }
}
</style>
