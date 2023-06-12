<template>
  <q-select
    v-model="$i18n.locale"
    :options="langOptions"
    dense
    borderless
    emit-value
    map-options
    options-dense
    style="min-width: 80px; padding: 0.5rem"
  />
</template>

<script setup lang="ts">
import languages from 'quasar/lang/index.json';

const appLanguages = languages.filter((lang) =>
  ['en-US', 'ko-KR'].includes(lang.isoName)
);

const langOptions = appLanguages.map((lang) => ({
  label: lang.nativeName,
  value: lang.isoName,
}));
</script>
