<template>
  <div class="notice-wrap">
    <div
      class="default-container text-white"
      :class="$q.dark.isActive ? 'bg-dark-02' : 'bg-light-02'"
    >
      <div class="container-title">
        {{ $t('word.notice') }}
      </div>
      <div class="container-description">
        <div class="lh-30">{{ $t('notice.description').split('\n')[0] }}</div>
        <div class="lh-30">{{ $t('notice.description').split('\n')[1] }}</div>
      </div>
    </div>

    <div class="default-info-wrap">
      <div class="left column">
        <div class="text-light-01 fs-15 text-weight-bold mb-20">
          {{ $t('word.support') }}
        </div>
        <div class="fs-30 mb-15">
          {{ $t('word.notice') }}
        </div>
        <div class="fs-16 mb-50" style="font-weight: 200">
          {{ $t('notice.need_to_know') }}
        </div>
        <q-img src="/icons/notice.png" />
      </div>

      <div class="right spacing-y-xl pa-30">
        <div v-for="(test, index) in 5" :key="test">
          <div class="mb-10 fs-20">{{ index + 1 }}. {{ test }}</div>

          <div class="fs-12 pl-20">내용 {{ test }}</div>
        </div>

        <div class="row justify-center">
          <q-pagination
            v-model="pagination.skip"
            :max="pagination.total"
            direction-links
            rounded
            :text-color="$q.dark.isActive ? 'white' : 'black'"
            active-color="none"
            active-text-color="light-02"
            @update:model-value="controlPagination"
            class="mt-30"
          />
        </div>
      </div>
    </div>

    <div
      class="mt-50 text-center column pa-30 m-auto"
      :class="$q.dark.isActive ? 'bg-dark-02' : 'bg-light-02'"
      style="max-width: 1000px"
    >
      <div class="fs-40 mb-30 text-white">{{ $t('notice.inquiry') }}</div>

      <div class="mb-35 text-white">{{ $t('inquiry.help') }}</div>

      <div>
        <q-btn
          @click="$q.dialog({ component: InquiryDialog })"
          :label="$t('word.inquiry')"
          color="white"
          text-color="black"
          push
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import InquiryDialog from 'src/components/dialog/InquiryDialog.vue';

const pagination = ref({
  total: 5,
  skip: 1,
});

const controlPagination = (skip: number) => {
  console.log(skip - 1);
};
</script>

<style scoped lang="scss">
.notice-wrap {
  margin-bottom: 50px;
}

.default-info-wrap {
  .right {
    border: 1px solid #000;
    border-radius: 10px;
  }
}

.body--dark {
  .right {
    border: 1px solid #fff;
  }
}
</style>
