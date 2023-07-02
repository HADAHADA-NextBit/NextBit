<template>
  <div class="notice-wrap">
    <div
      class="notice-desc-container text-white"
      :class="$q.dark.isActive ? 'bg-dark-02' : 'bg-light-02'"
    >
      <div class="fs-40">
        {{ $t('word.notice') }}
      </div>
      <div class="fs-20 mt-30 column">
        <div class="lh-30">{{ $t('notice.description').split('\n')[0] }}</div>
        <div class="lh-30">{{ $t('notice.description').split('\n')[1] }}</div>
      </div>
    </div>

    <div class="notice-info-wrap">
      <div class="notice-image-area column">
        <div class="text-light-01 fs-15 text-weight-bold mb-20">
          {{ $t('word.support') }}
        </div>
        <div class="fs-30 mb-15">
          {{ $t('word.notice') }}
        </div>
        <div class="fs-16 mb-50" style="font-weight: 200">
          {{ $t('notice.need_to_know') }}
        </div>
        <q-img src="/public/icons/notice.png" />
      </div>

      <div class="notice-info-area spacing-y-xl pa-30">
        <div v-for="(test, index) in 5" :key="test">
          <div class="mb-10 fs-20">{{ index + 1 }}. {{ test }}</div>

          <div class="fs-12 pl-20">내용 {{ test }}</div>
        </div>

        <div class="row justify-end">
          <q-pagination
            v-model="pagination.skip"
            :max="pagination.total"
            direction-links
            rounded
            :text-color="$q.dark.isActive ? 'white' : 'black'"
            active-color="none"
            active-text-color="light-02"
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
</script>

<style scoped lang="scss">
.notice-wrap {
  margin-bottom: 50px;
}
.notice-desc-container {
  padding: 70px 80px;
  font-weight: 700;
}

.notice-info-wrap {
  padding: 70px 80px;
  display: flex;

  .notice-image-area {
    width: 35%;
  }

  .notice-info-area {
    width: 60%;
    border: 1px solid #000;
    margin-left: 5%;
    border-radius: 10px;
  }
}

.body--dark {
  .notice-info-area {
    border: 1px solid #fff;
  }
}
</style>
