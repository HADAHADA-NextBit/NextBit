<template>
  <nav class="main-bg px-10">
    <div class="main-layout">
      <q-btn
        :to="page.name"
        v-for="page in pages"
        :key="page.name"
        unelevated
        no-caps
        class="full-height"
        :class="{
          'router-active': page.name === $router.currentRoute.value.name,
        }"
      >
        <q-icon
          name="img:/logo.svg"
          class="mr-5"
          size="35px"
          v-if="page.name === 'home'"
        />
        <span>{{ $t(`word.${page.name}`) }}</span>
      </q-btn>

      <q-space />

      <NavigationFab />
    </div>
  </nav>
</template>

<script setup lang="ts">
import NavigationFab from 'src/components/NavigationFab.vue';
import { pages } from 'src/router/routes';
</script>

<style scoped lang="scss">
nav {
  height: 60px;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  z-index: 10;
  display: flex;
  align-items: center;

  div {
    a {
      color: rgb(155, 155, 155);
      margin-right: 10px;
    }

    .router-active {
      color: rgb(255, 255, 255);
    }
  }
}

@media (max-width: 768px) {
}
</style>
