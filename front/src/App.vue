<template>
  <Navigation />
  <router-view />
  <!-- <q-ajax-bar
    ref="bar"
    position="top"
    color="blue"
    size="10px"
    skip-hijack
    style="top: 5rem"
  /> -->
  <q-ajax-bar
    ref="bar2"
    position="bottom"
    color="accent"
    size="10px"
    :hijack-filter="filterFn"
  />
</template>

<script setup lang="ts">
import Navigation from 'src/components/Navigation.vue';
import { useBinanceStore } from 'src/stores/binance';
import { useCommonStore } from './stores/common';
import { onMounted, ref, watch, computed } from 'vue';

const bar = ref();
const bar2 = ref();
const commonStore = useCommonStore();
const ajaxBar = computed(() => commonStore.ajaxBar);
const binance = useBinanceStore();

// watch(ajaxBar, () => {
//   if (ajaxBar.value) {
//     bar.value.start();
//   } else {
//     bar.value.stop();
//   }
// });

const filterFn = (url: string) => {
  console.log(url);
  console.log(!/^https:\/\/api.upbit.com\/v1\/candles\/minutes/.test(url));
  return !/^https:\/\/api.upbit.com\/v1\/candles\/minutes/.test(url);
};

onMounted(() => {
  binance.socketBinance();
  console.log('here');
  binance.getSocketBinance();
});
</script>
