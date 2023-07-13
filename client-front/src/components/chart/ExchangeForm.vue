<template>
  <q-card class="chart-border" :class="$q.dark.isActive ? 'bg-dark-09' : ''">
    <q-card-section class="pa-10">
      <q-btn-toggle
        v-model="isBid"
        @update:model-value="
          (formState.orderBalance = '0'), (formState.tradePrice = '0')
        "
        glossy
        :toggle-color="isBid ? 'positive' : 'negative'"
        :options="[
          { label: $t('word.bid'), value: true },
          { label: $t('word.ask'), value: false },
        ]"
        spread
        no-caps
      />
    </q-card-section>

    <q-card-section class="pa-10 row justify-center">
      <q-radio
        v-for="data in [
          { val: 'market', label: 'word.market_price' },
          { val: 'designation', label: 'word.designation_price' },
        ]"
        :key="data.val"
        v-model="tradeType"
        checked-icon="task_alt"
        unchecked-icon="panorama_fish_eye"
        :color="isBid ? 'positive' : 'negative'"
        :val="data.val"
        :label="$t(data.label)"
      />
    </q-card-section>

    <q-card-section class="pa-10 spacing-y-xl">
      <q-input
        :model-value="
          isBid
            ? formState.holdingBalance.toLocaleString()
            : formState.holdingCoin.toLocaleString()
        "
        rounded
        standout
        autocomplete="off"
        class="no-pointer-events"
        input-class="text-right"
        dense
        inputmode="decimal"
      >
        <template #prepend>
          <span class="fs-14">
            {{ isBid ? $t('word.holdings') : $t('word.holding_coin') }}
          </span>
        </template>
        <template #append>
          <span class="fs-14"> &#8361; </span>
        </template>
      </q-input>

      <template v-if="tradeType === 'market'">
        <q-input
          :model-value="chart.tradeData.tp?.toLocaleString()"
          rounded
          standout
          autocomplete="off"
          class="no-pointer-events"
          input-class="text-right"
          dense
          inputmode="decimal"
        >
          <template #prepend>
            <span class="fs-14"> {{ $t('word.estimated') }} </span>
          </template>
          <template #append>
            <span class="fs-14"> &#8361;</span>
          </template>
        </q-input>
      </template>

      <template v-else>
        <q-input
          v-model="formState.tradePrice"
          @update:model-value="
            (val) => (formState.tradePrice = convertNumber(val))
          "
          :debounce="1000"
          rounded
          standout
          autocomplete="off"
          input-class="text-right"
          dense
          inputmode="decimal"
        >
          <template #prepend>
            <span class="fs-14">
              {{ isBid ? $t('word.bid_price') : $t('word.ask_price') }}
            </span>
          </template>
          <template #append>
            <span class="fs-14"> &#8361; </span>
          </template>
        </q-input>
      </template>

      <q-input
        v-model="formState.orderBalance"
        @update:model-value="
          (val) => (formState.orderBalance = convertNumber(val))
        "
        :debounce="1000"
        rounded
        standout
        autocomplete="off"
        input-class="text-right"
        dense
        inputmode="decimal"
      >
        <template #prepend>
          <span class="fs-14">{{ $t('word.order_quantity') }}</span>
        </template>
        <template #append>
          <span class="fs-14">{{ chart.selectCoin.split('-')[1] }} </span>
        </template>
      </q-input>

      <q-input
        :model-value="remaining"
        rounded
        standout
        autocomplete="off"
        class="no-pointer-events"
        input-class="text-right"
        dense
        inputmode="decimal"
      >
        <template #prepend>
          <span class="fs-14">{{ $t('word.order_total') }}</span>
        </template>
        <template #append>
          <span class="fs-14"> &#8361; </span>
        </template>
      </q-input>
    </q-card-section>

    <q-card-section class="pa-10">
      <q-btn
        :label="isBid ? $t('word.bid') : $t('word.ask')"
        class="full-width"
        :color="isBid ? 'positive' : 'negative'"
        rounded
        push
      />
    </q-card-section>
  </q-card>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue';
import { useChartStore } from 'src/stores/chart-store';
import { comma, removeOthers } from 'src/utils/rule';

const chart = useChartStore();
const isBid = ref(true);
const tradeType = ref('market');
const formState = ref({
  holdingBalance: 1000000,
  holdingCoin: 12.123,
  tradePrice: '0',
  orderBalance: '0',
});

const convertNumber = (num: any) => {
  return comma(num);
};

const remaining = computed(() => {
  if (tradeType.value === 'market') {
    return (
      chart.tradeData.tp &&
      comma(chart.tradeData.tp * removeOthers(formState.value.orderBalance))
    );
  } else {
    return comma(
      removeOthers(formState.value.tradePrice) *
        removeOthers(formState.value.orderBalance)
    );
  }
});
</script>

<style scoped lang="scss"></style>
