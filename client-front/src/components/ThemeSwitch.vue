<template>
  <q-toggle
    class="theme-switch"
    v-model="darkMode"
    :icon="darkMode ? 'dark_mode' : 'bedtime_off'"
    color="blue-grey-4"
    keep-color
  />
</template>

<script setup lang="ts">
import { useQuasar, LocalStorage } from 'quasar';
import { ref, watch, onBeforeMount } from 'vue';
import { useCommonStore } from 'src/stores/common-store';

const $q = useQuasar();
const darkMode = ref(false);

watch(darkMode, (darkMode: boolean) => {
  $q.dark.set(darkMode);
  if (!darkMode) {
    LocalStorage.remove('dm');
  } else {
    LocalStorage.set('dm', String(darkMode));
  }
});

onBeforeMount(() => {
  LocalStorage.getItem('dm') === 'true'
    ? ((darkMode.value = true), $q.dark.set(true))
    : (darkMode.value = false);
});
</script>
