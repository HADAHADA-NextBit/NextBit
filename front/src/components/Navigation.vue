<template>
  <div class="nav">
    <q-toolbar class="bg-primary text-white shadow-2">
      <q-btn-group outline>
        <div class="q-gutter-lg">
          <q-btn to="/" stretch flat label="HOME" padding="13px 10px" />
          <q-btn
            to="/exchange"
            stretch
            flat
            label="거래소"
            padding="13px 10px"
          />
        </div>
      </q-btn-group>

      <q-space />

      <q-spinner-radio color="red" size="2rem" />
      <q-separator dark vertical />
      <Translate />
      <q-separator dark vertical />
      <q-toggle v-model="darkMode" icon="alarm" />
      <q-separator dark vertical />
      <q-btn
        dense
        flat
        icon="list"
        style="margin-left: 10px"
        @click="useCommonStore().setExchangeNavShow()"
      />
    </q-toolbar>
  </div>
</template>

<script setup lang="ts">
import { useQuasar } from 'quasar';
import { ref, watch } from 'vue';
import { useCommonStore } from 'src/stores/common';
import Translate from './Translate.vue';

const $q = useQuasar();
const darkMode = ref(false);

watch(darkMode, (darkMode: boolean) => {
  $q.dark.set(darkMode);
  useCommonStore().setDarkMode(darkMode);
});
</script>

<style scoped lang="scss">
.nav {
  position: fixed;
  top: 0;
  left: 0;
  z-index: 10;
  width: 100%;
}
</style>
