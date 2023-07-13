<template>
  <q-dialog
    ref="dialogRef"
    style="height: 100vh"
    persistent
    no-shake
    maximized
    transition-show="slide-left"
    transition-hide="slide-right"
    position="right"
  >
    <q-card style="max-width: 500px; min-width: 320px">
      <q-card-section class="row items-center pa-5">
        <q-btn icon="chevron_right" flat dense v-close-popup size="20px" />
      </q-card-section>

      <q-card-section>
        <div class="mt-30">
          <q-table
            :title="$t('word.transaction_history')"
            :rows="rows"
            :columns="columns"
            row-key="name"
            card-style="border-radius: 10px"
            :card-class="$q.dark.isActive ? 'bg-dark-06' : ''"
            bordered
          >
            <template #header="props">
              <q-tr :props="props">
                <q-th v-for="col in props.cols" :key="col.name" :props="props">
                  {{ $t(col.label) }}
                </q-th>
              </q-tr>
            </template>
          </q-table>
        </div>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import { useDialogPluginComponent } from 'quasar';
import { QTableProps } from 'quasar';

const columns: QTableProps['columns'] = [
  {
    name: 'coin',
    label: 'word.coin',
    field: 'coin',
  },
  {
    name: 'type',
    label: 'word.type',
    field: 'type',
  },
  {
    name: 'conclusionAmount',
    label: 'word.conclusion_amount',
    field: 'conclusionAmount',
  },

  { name: 'quantity', label: 'word.quantity', field: 'quantity' },
  {
    name: 'conclusionPrice',
    label: 'word.conclusion_price',
    field: 'conclusionPrice',
  },
  { name: 'yield', label: 'word.yield', field: 'yield' },
  { name: 'profit', label: 'word.profit', field: 'profit' },
  {
    name: 'fee',
    label: 'word.fee',
    field: 'fee',
  },
  {
    name: 'previousAmount',
    label: 'word.previous_amount',
    field: 'previousAmount',
  },
  {
    name: 'comingAmount',
    label: 'word.coming_amount',
    field: 'comingAmount',
  },
];

const rows: QTableProps['rows'] = [
  {
    coin: 'BTC',
    type: 159,
    conclusionAmount: 6.0,
    quantity: 24,
    conclusionPrice: 4.0,
    yield: '1%',
    profit: 87,
    fee: '14%',
    previousAmount: '1%',
    comingAmount: '1%',
  },
  {
    coin: 'ETH',
    type: 159,
    conclusionAmount: 6.0,
    quantity: 24,
    conclusionPrice: 4.0,
    yield: '1%',
    profit: 87,
    fee: '14%',
    previousAmount: '1%',
    comingAmount: '1%',
  },
];
const { dialogRef, onDialogOK } = useDialogPluginComponent();
</script>

<style scoped lang="scss"></style>
