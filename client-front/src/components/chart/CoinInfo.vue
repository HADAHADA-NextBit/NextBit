<template>
  <q-card class="info-wrap">
    <div class="column">
      <span> {{ chart.coinFullName.ko }} </span>
      <span>
        {{ chart.selectCoin }}
      </span>
    </div>

    <div class="column">
      <span> 현재가 </span>
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
      <span> 전일종가 </span>
      <span>
        {{ chart.tickerData.pcp?.toLocaleString() }}
      </span>
    </div>

    <div class="column">
      <span> 당일고가 </span>
      <span class="text-positive">
        {{ chart.tickerData.hp?.toLocaleString() }}
      </span>
    </div>

    <div class="column">
      <span> 당일저가 </span>
      <span class="text-negative">
        {{ chart.tickerData.lp?.toLocaleString() }}
      </span>
    </div>

    <div class="column">
      <span> 거래량 </span>
      <span>
        {{ Math.round(chart.tickerData.atv24h).toLocaleString() }}
        {{ chart.selectCoin.split('-')[1] }}
      </span>
    </div>

    <div class="column">
      <span> 거래대금 </span>
      <span>
        {{
          chart.tickerData.atp24h &&
          Number(chart.tickerData.atp24h.toFixed().slice(0, -6))
            .toLocaleString()
            .concat(' 백만')
        }}
      </span>
    </div>

    <div class="column">
      <span> 52주 최고 </span>
      <span class="text-positive">
        {{ chart.tickerData.h52wp?.toLocaleString() }}
      </span>
    </div>

    <div class="column">
      <span> 52주 최저 </span>
      <span class="text-negative">
        {{ chart.tickerData.l52wp?.toLocaleString() }}
      </span>
    </div>
  </q-card>
</template>

<script setup lang="ts">
import { useChartStore } from 'src/stores/chart-store';

const chart = useChartStore();
</script>

<style scoped lang="scss">
.info-wrap {
  height: 80px;
  padding: 10px;
  border-radius: 10px;
  display: flex;
  align-items: center;
  overflow-x: auto;

  div {
    flex: 1;
    text-align: center;
    white-space: nowrap;

    &:not(:first-of-type) {
      margin-left: 15px;
    }

    span {
      &:first-child {
        margin-bottom: 5px;
      }
    }
  }
}

.body--dark {
  .info-wrap {
    background-color: $dark-09;
  }
}
</style>
