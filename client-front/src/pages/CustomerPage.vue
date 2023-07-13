<template>
  <div class="customer-wrap">
    <div
      class="default-container text-white"
      :class="$q.dark.isActive ? 'bg-dark-02' : 'bg-light-02'"
    >
      <div class="container-title">
        {{ $t('customer.title') }}
      </div>
      <div class="container-description">
        <div>{{ $t('customer.description').split('\n')[0] }}</div>
        <div>{{ $t('customer.description').split('\n')[1] }}</div>
      </div>
    </div>

    <div class="default-info-wrap">
      <div class="customer-image-area column">
        <div class="text-light-01 fs-15 text-weight-bold mb-20">
          {{ $t('word.support') }}
        </div>

        <div class="fs-30 mb-15">
          {{ $t('word.faq') }}
        </div>

        <div
          class="fs-16 lh-20"
          style="font-weight: 200"
          v-for="index in 3"
          :key="index"
        >
          {{ $t('customer.need_to_know').split('\n')[index] }}
        </div>

        <q-img src="/images/faq.png" style="max-width: 430px" />
      </div>

      <div class="customer-info-area spacing-y-xl pa-20">
        <div v-for="(test, index) in 5" :key="test">
          <div class="mb-10 fs-20">{{ index + 1 }}. {{ test }}</div>

          <div class="fs-12 pl-20">내용 {{ test }}</div>
        </div>

        <div class="row justify-center">
          <q-pagination
            v-model="faqPagination.skip"
            :max="faqPagination.total"
            direction-links
            rounded
            :text-color="$q.dark.isActive ? 'white' : 'black'"
            active-color="none"
            active-text-color="light-02"
            @update:model-value="controlFaqPagination"
          />
        </div>
      </div>
    </div>

    <div
      class="text-center column pa-30 m-auto mt-50 br-10"
      :class="$q.dark.isActive ? 'bg-dark-02' : 'bg-light-02'"
      style="max-width: 1000px; margin-bottom: 100px"
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

    <div style="max-width: 1000px" class="m-auto">
      <div class="fs-30 mb-30">{{ $t('word.inquiry_history') }}</div>
      <div
        class="text-white pa-10 br-10"
        :class="$q.dark.isActive ? 'bg-dark-09' : 'bg-light-02'"
      >
        <q-list>
          <q-expansion-item
            popup
            icon="Q."
            label="Inbox"
            v-for="i in 5"
            :key="i"
            expand-icon-class="text-white"
          >
            <q-separator />
            <q-card class="bg-inherit">
              <q-card-section> Lorem ipsum dolor sit amet </q-card-section>
            </q-card>
          </q-expansion-item>
        </q-list>

        <div class="row justify-center mt-30">
          <q-pagination
            v-model="historyPagination.skip"
            :max="historyPagination.total"
            direction-links
            rounded
            text-color="white"
            active-color="none"
            active-text-color="light-03"
            @update:model-value="controlHistoryPagination"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import InquiryDialog from 'src/components/dialog/InquiryDialog.vue';

const faqPagination = ref({
  total: 5,
  skip: 1,
});

const historyPagination = ref({
  total: 5,
  skip: 1,
});

const controlFaqPagination = (skip: number) => {
  console.log(skip - 1);
};

const controlHistoryPagination = (skip: number) => {
  console.log(skip - 1);
};
</script>

<style scoped lang="scss">
.customer-wrap {
  margin-bottom: 50px;
}

.default-info-wrap {
  margin-bottom: 50px;

  .customer-image-area {
    width: 35%;
  }

  .customer-info-area {
    width: 60%;
    border: 1px solid #000;
    margin-left: 5%;
    border-radius: 10px;
  }
}

.body--dark {
  .customer-info-area {
    border: 1px solid #fff;
  }
}

@media (max-width: 768px) {
  .customer-desc-container {
    padding: 20px;
  }

  .default-info-wrap {
    padding: 20px;
    display: block;

    .customer-image-area,
    .customer-info-area {
      width: 100%;
    }

    .customer-info-area {
      margin-left: 0;
    }
  }
}
</style>
