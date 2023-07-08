<template>
  <div class="coin-info">
    <div>{{ chart.tickerData }}</div>
    {{ chart.tickerData.korean_name }}
    {{ chart.tickerData.tp }}
    {{ chart.tickerData.pcp }}
    {{ chart.tickerData.hp }}
    {{ chart.tickerData.lp }}
    {{ chart.tickerData.atv24h }}
    {{ chart.tickerData.atp24h }}
    {{ chart.tickerData.h52wp }}
    {{ chart.tickerData.l52wp }}
  </div>
</template>

<script setup lang="ts">
import { useChartStore } from 'src/stores/chart-store';

const chart = useChartStore();
</script>

<style scoped lang="scss">
.coin-info {
  height: 80px;
  background-color: $light-02;
  color: white;
  border: $dark-chart-page-border;
  border-radius: 10px;
}

.body--dark {
  .coin-info {
    background-color: $dark-02;
  }
}
</style>
