<template>
  <q-card class="info-wrap" bordered>
    <div class="column">
      <span> {{ $t('word.investment') }} </span>
      <span> 10,000,000,000 \ </span>
    </div>

    <div class="column">
      <span> {{ $t('word.total_bid') }} </span>
      <span> 34,735,000 \ </span>
    </div>

    <div class="column">
      <span> {{ $t('word.available_bid') }} </span>
      <span> 999,999,999 \ </span>
    </div>

    <div class="column">
      <span> {{ $t('word.total_yield') }} </span>
      <span class="text-positive"> -1% </span>
    </div>

    <div class="column">
      <span> {{ $t('word.total_evaluation') }} </span>
      <span class="text-negative"> -300,000 \ </span>
    </div>
  </q-card>

  <div class="mt-30">
    <q-table
      :title="$t('word.recent_transaction_history')"
      :rows="rows"
      :columns="columns"
      row-key="name"
      card-style="border-radius: 10px"
      :card-class="$q.dark.isActive ? 'bg-dark-09' : ''"
      bordered
      hide-pagination
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

  <div class="mt-30">
    <span class="fs-18">{{ $t('word.holding_coin') }}</span>

    <q-card
      v-ripple
      class="info-wrap cursor-pointer mt-20"
      bordered
      @click="$q.dialog({ component: WalletDialog })"
    >
      <div class="column">
        <span> 비트코인 </span>
        <span> BTC </span>
      </div>

      <div class="column">
        <span> {{ $t('word.holding_quantity') }} </span>
        <span> 1 </span>
      </div>

      <div class="column">
        <span> {{ $t('word.total_bid') }} </span>
        <span> </span>
      </div>

      <div class="column">
        <span> {{ $t('word.average') }} </span>
        <span> </span>
      </div>

      <div class="column">
        <span> {{ $t('word.evaluation') }} </span>
        <span> </span>
      </div>

      <div class="column">
        <span> {{ $t('word.yield') }} </span>
        <span> </span>
      </div>

      <div class="row justify-end">
        <q-icon name="chevron_right" size="25px" />
      </div>
    </q-card>
  </div>
</template>

<script setup lang="ts">
import { QTableProps } from 'quasar';
import WalletDialog from 'src/components/dialog/WalletDialog.vue';

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
</script>

<style scoped lang="scss"></style>
