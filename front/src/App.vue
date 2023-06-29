<template>
  <!-- <NavigationHeader /> -->
  <q-layout view="hHh Lpr fFf" container style="height: 100vh" class="shadow-2">
    <q-header elevated :class="$q.dark.isActive ? 'bg-dark-02' : 'bg-light-02'">
      <q-toolbar class="glossy pa-0">
        <q-btn
          flat
          @click="drawer = !drawer"
          icon="menu"
          class="pa-15"
          size="16px"
        />

        <q-space />

        <q-toolbar-title class="text-center">
          <q-icon name="img:/logo.svg" size="35px" />
        </q-toolbar-title>

        <q-space />

        <NavigationFab />
      </q-toolbar>
    </q-header>

    <q-footer elevated reveal>
      <q-toolbar
        class="glossy-down"
        :class="$q.dark.isActive ? 'bg-dark-02' : 'bg-light-02'"
      >
        <q-toolbar-title>Footer</q-toolbar-title>
      </q-toolbar>
    </q-footer>

    <q-drawer
      v-model="drawer"
      show-if-above
      :mini="miniState"
      @mouseover="miniState = false"
      @mouseout="miniState = true"
      mini-to-overlay
      :width="200"
      :breakpoint="768"
      :class="$q.dark.isActive ? 'bg-dark-02' : 'bg-light-02'"
      class="glossy-left text-white"
    >
      <q-scroll-area class="fit" :horizontal-thumb-style="{ opacity: '0' }">
        <q-list padding>
          <q-item
            clickable
            v-ripple
            v-for="page in pages"
            :key="page.name"
            :to="page.name"
            :active="page.name === $router.currentRoute.value.name"
          >
            <q-item-section avatar>
              <q-icon :name="page.icon" />
            </q-item-section>

            <q-item-section> {{ $t(`word.${page.name}`) }} </q-item-section>
          </q-item>
        </q-list>
      </q-scroll-area>
    </q-drawer>

    <q-page-container>
      <q-page padding>
        <RouterView />
      </q-page>
    </q-page-container>
  </q-layout>
  <!-- <MainFooter /> -->
</template>

<script setup lang="ts">
import { onMounted, ref, watch, computed } from 'vue';
import NavigationHeader from 'src/components/NavigationHeader.vue';
import MainFooter from 'src/components/MainFooter.vue';
import NavigationFab from 'src/components/NavigationFab.vue';
import { pages } from 'src/router/routes';

const drawer = ref(false);
const miniState = ref(false);
</script>

<style lang="scss">
.q-page {
  max-width: 1440px;
  margin: auto;
}

.q-list {
  .q-item--active {
    color: white;
    font-weight: bold;
    -webkit-text-stroke: 0.5px #000;
  }
}
</style>
