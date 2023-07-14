<template>
  <!-- <NavigationHeader /> -->
  <q-layout view="hHh Lpr fFf" class="shadow-2">
    <q-header elevated :class="$q.dark.isActive ? 'bg-dark-02' : 'bg-light-02'">
      <q-toolbar class="glossy pa-0" style="height: 60px">
        <q-fab
          flat
          icon="chevron_right"
          active-icon="chevron_left"
          v-model="drawer"
          size="16px"
        />

        <q-space />

        <q-toolbar-title class="text-center full-height">
          <q-btn :to="{ name: 'home' }" unelevated class="full-height">
            <q-icon name="img:/logo.svg" size="35px" />
          </q-btn>
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
        <q-space />

        <span>© Team HadaHada</span>
      </q-toolbar>
    </q-footer>

    <q-drawer
      v-model="drawer"
      show-if-above
      :mini="miniState"
      @mouseover="miniState = false"
      @mouseout="miniState = true"
      bordered
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
      <q-page style="padding: 20px">
        <RouterView />
      </q-page>
    </q-page-container>
  </q-layout>
  <!-- <MainFooter /> -->
</template>

<script setup lang="ts">
import { onBeforeMount, ref, watch, computed } from 'vue';
// import NavigationHeader from 'src/components/NavigationHeader.vue';
// import MainFooter from 'src/components/MainFooter.vue';
import NavigationFab from 'src/components/NavigationFab.vue';
import { pages } from 'src/router/routes';
import axios from 'axios';
import { SessionStorage } from 'quasar';

const drawer = ref(false);
const miniState = ref(true);

onBeforeMount(async () => {
  const platform = SessionStorage.getItem('platform');
  if (!platform) return;
  const request = axios.create();

  const code = new URL(location.href).searchParams.get('code');

  try {
    if (platform === 'kakao') {
      const res = await request.post(
        `https://kauth.kakao.com/oauth/token?grant_type=authorization_code&client_id=${process.env.KAKAO_REST_KEY}&redirect_uri=${process.env.REDIRECT}&code=${code}`
      );

      const info = await request.get(
        'https://kapi.kakao.com/v1/user/access_token_info',
        {
          headers: { Authorization: `Bearer ${res.data.access_token}` },
        }
      );
    } else if (platform === 'naver') {
      const token = new URLSearchParams(location.href.split('#')[1]).get(
        'access_token'
      );
    }
  } finally {
    setTimeout(() => {
      history.replaceState({}, '', location.pathname);
      SessionStorage.remove('platform');
    }, 100);
  }
});
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
