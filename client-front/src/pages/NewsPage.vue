<template>
  <div class="news-wrap">
    <div
      class="default-container text-white"
      :class="$q.dark.isActive ? 'bg-dark-02' : 'bg-light-02'"
    >
      <div class="container-title">
        {{ $t('word.news') }}
      </div>
      <div class="container-description">
        <div class="lh-30">{{ $t('news.description').split('\n')[0] }}</div>
        <div class="lh-30">{{ $t('news.description').split('\n')[1] }}</div>
      </div>
    </div>

    <div
      class="default-info-wrap row justify-between no-wrap"
      style="margin: 0"
    >
      <div class="column">
        <div class="text-light-01 fs-15 text-weight-bold mb-20">
          {{ $t('word.support') }}
        </div>
        <div class="fs-30 mb-15">
          {{ $t('word.news') }}
        </div>
        <div class="fs-16 lh-20" style="font-weight: 200">
          {{ $t('news.need_to_know').split('\n')[0] }}
        </div>
        <div class="fs-16 lh-20" style="font-weight: 200">
          {{ $t('news.need_to_know').split('\n')[1] }}
        </div>
      </div>
      <q-img src="/images/news.png" style="max-width: 500px" />
    </div>

    <div style="max-width: 1000px" class="m-auto">
      <div class="fs-24 mb-30">Top 10</div>
      <q-list
        :class="$q.dark.isActive ? 'bg-dark-09' : 'bg-light-05'"
        class="text-white rounded-borders"
        bordered
      >
        <q-expansion-item
          label="Inbox"
          v-for="i in 10"
          :key="i"
          expand-icon="COINNESS"
          expand-icon-class="text-white"
          href="https://naver.com"
          class="fs-24"
          target="_blank"
        >
        </q-expansion-item>
      </q-list>
    </div>

    <div style="max-width: 1000px; margin-top: 100px" class="m-auto">
      <div class="fs-24 mb-30">{{ $t('word.all') }}</div>
      <q-list
        :class="$q.dark.isActive ? 'bg-dark-09' : 'bg-light-05'"
        class="text-white rounded-borders"
        bordered
      >
        <q-expansion-item
          label="Inbox"
          v-for="i in 10"
          :key="i"
          expand-icon="COINNESS"
          expand-icon-class="text-white"
          href="https://naver.com"
          class="fs-24"
          target="_blank"
        >
        </q-expansion-item>
      </q-list>

      <div class="row justify-center mt-30">
        <q-pagination
          v-model="pagination.skip"
          :max="pagination.total"
          direction-links
          rounded
          :text-color="$q.dark.isActive ? 'white' : 'black'"
          active-color="none"
          active-text-color="light-03"
          @update:model-value="controlPagination"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const pagination = ref({
  total: 5,
  skip: 1,
});

const controlPagination = (skip: number) => {
  console.log(skip - 1);
};
</script>

<style scoped lang="scss">
.news-wrap {
  margin-bottom: 50px;
}
</style>
