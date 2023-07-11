<template>
  <q-select
    v-model="$i18n.locale"
    :options="langOptions"
    dense
    emit-value
    map-options
    borderless
    options-dense
    class="translate-lang"
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

<style lang="scss">
.translate-lang {
  .q-field__native,
  .q-field__append {
    color: white;
  }
}
</style>
