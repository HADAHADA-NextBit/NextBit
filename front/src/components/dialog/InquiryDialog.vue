<template>
  <q-dialog presistent ref="dialogRef">
    <q-card class="default-dialog pa-50 text-center">
      <q-card-section>
        <q-form @submit="sendInquiry" class="q-gutter-md">
          <i class="fa-solid fa-paper-plane" style="font-size: 60px"></i>
          <div class="mt-20 fs-25">{{ $t('word.inquiry') }}</div>
          <div class="mt-40 fs-16">
            <div class="lh-24">
              {{ $t('inquiry.dialog.answer_24').split('\n')[0] }}
            </div>
            <div class="lh-24">
              {{ $t('inquiry.dialog.answer_24').split('\n')[1] }}
            </div>
          </div>
          <q-input
            v-model="inquiryState.title"
            color="white"
            standout="white"
            dense
          >
          </q-input>
          <q-input
            v-model="inquiryState.contents"
            color="white"
            type="textarea"
            standout="white"
            dense
          >
          </q-input>
        </q-form>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import { useDialogPluginComponent } from 'quasar';
import { ref } from 'vue';

const { dialogRef } = useDialogPluginComponent();

const inquiryState = ref({
  title: '',
  contents: '',
});

const sendInquiry = () => {
  //
};
</script>
