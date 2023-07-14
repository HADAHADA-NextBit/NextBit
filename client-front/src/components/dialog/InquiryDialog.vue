<template>
  <q-dialog ref="dialogRef" persistent no-shake>
    <q-card
      class="default-dialog text-center"
      :class="$q.dark.isActive ? 'bg-dark-03' : 'bg-light-02'"
      dark
    >
      <q-card-section class="row pa-10">
        <q-space />
        <q-btn icon="close" flat dense v-close-popup size="16px" />
      </q-card-section>

      <q-card-section style="padding: 10%">
        <q-form @submit="sendInquiry" class="q-gutter-md">
          <i class="fa-solid fa-paper-plane" style="font-size: 60px"></i>

          <div class="mt-20 fs-25">{{ $t('word.inquiry') }}</div>

          <div class="my-40 fs-16">
            <div class="lh-24">
              {{ $t('inquiry.dialog.answer_24').split('\n')[0] }}
            </div>

            <div class="lh-24">
              {{ $t('inquiry.dialog.answer_24').split('\n')[1] }}
            </div>
          </div>

          <q-input
            v-model="inquiryState.title"
            :label="$t('word.title')"
            standout="white"
            dense
            dark
            color="black"
            bg-color="white"
            label-color="black"
            input-style="color: black"
          />

          <q-input
            v-model="inquiryState.contents"
            :label="$t('word.contents')"
            type="textarea"
            standout="white"
            dense
            dark
            color="black"
            bg-color="white"
            label-color="black"
            input-style="color: black"
          />
        </q-form>

        <q-btn
          class="mt-50"
          :label="$t('word.inquiry')"
          color="white"
          text-color="black"
          push
        />
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
