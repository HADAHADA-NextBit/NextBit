<template>
  <q-card class="info-wrap">
    <div class="column">
      <span>
        {{ chart.coinFullName[i18n.global.locale.value.split('-')[0]] }}
      </span>
      <span>
        {{ chart.selectCoin }}
      </span>
    </div>

    <div class="column">
      <span> {{ $t('word.current_price') }} </span>
      <span
        :class="
          chart.tickerData.tp > chart.tickerData.pcp
            ? 'text-positive'
            : chart.tickerData.tp === chart.tickerData.pcp
            ? ''
            : 'text-negative'
        "
      >
        {{ chart.tickerData.tp?.toLocaleString() }}
      </span>
    </div>

    <div class="column">
      <span> {{ $t('word.previous_close') }} </span>
      <span>
        {{ chart.tickerData.pcp?.toLocaleString() }}
      </span>
    </div>

    <div class="column">
      <span> {{ $t('word.day_high') }} </span>
      <span class="text-positive">
        {{ chart.tickerData.hp?.toLocaleString() }}
      </span>
    </div>

    <div class="column">
      <span> {{ $t('word.day_low') }} </span>
      <span class="text-negative">
        {{ chart.tickerData.lp?.toLocaleString() }}
      </span>
    </div>

    <div class="column">
      <span>
        {{ $t('word.volume_type', [chart.selectCoin.split('-')[1]]) }}
      </span>
      <span>
        {{ Math.round(chart.tickerData.atv24h).toLocaleString() }}
        {{ chart.selectCoin.split('-')[1] }}
      </span>
    </div>

    <div class="column">
      <span> {{ $t('word.volume_type', ['&#8361;']) }} </span>
      <span>
        {{
          chart.tickerData.atp24h &&
          Number(chart.tickerData.atp24h.toFixed().slice(0, -6))
            .toLocaleString()
            .concat(` ${$t('word.short_million')}`)
        }}
      </span>
    </div>

    <div class="column">
      <span> {{ $t('word.52w_highest') }} </span>
      <span class="text-positive">
        {{ chart.tickerData.h52wp?.toLocaleString() }}
      </span>
    </div>

    <div class="column">
      <span> {{ $t('word.52w_lowest') }} </span>
      <span class="text-negative">
        {{ chart.tickerData.l52wp?.toLocaleString() }}
      </span>
    </div>
  </q-card>
</template>

<script setup lang="ts">
import { useChartStore } from 'src/stores/chart-store';
import { i18n } from 'src/boot/i18n';

const chart = useChartStore();
</script>

<style scoped lang="scss"></style>
