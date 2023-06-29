<template>
  <q-dialog>
    <q-card style="min-width: 320px; max-width: 500px; width: 100%">
      <q-card-section class="row items-center q-pb-none">
        <q-space />
        <q-btn icon="close" flat round dense v-close-popup />
      </q-card-section>

      <q-card-section class="q-pa-none">
        <div class="text-h6 text-center">
          <q-icon name="img:/logo.svg" size="35px" />
        </div>
      </q-card-section>

      <q-card-actions align="right">
        <q-btn flat label="OK" color="primary" v-close-popup />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts"></script>
