<template>
  <div class="chart-wrap" :class="{ 'bg-dark': useCommonStore().darkMode }">
    <Echarts
      :bindingOptions="bindingOptions"
      style="width: 100%; height: 530px"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, watch, onUnmounted, computed } from 'vue';
import { useSocketUpbitStore } from 'src/stores/socket-upbit';
import { useCommonStore } from 'src/stores/common';
import Echarts from 'src/components/Echarts.vue';

const upbit = useSocketUpbitStore();

const selectCoinFullName = computed(() => upbit.selectCoinFullName);
const selectCoin = computed(() => upbit.selectCoin);

const bindingOptions = ref({
  title: {
    text: selectCoinFullName.value,
    subtext:
      selectCoin.value.split('-')[1] + '/' + selectCoin.value.split('-')[0],
    left: 'center',
    top: 40,
    textStyle: {
      fontSize: 25,
    },
  },
  tooltip: {
    trigger: 'axis',
  },
  xAxis: [
    {
      type: 'category',
      data: [''],
      boundaryGap: true,
      axisLabel: { show: false },
      axisPointer: {
        show: true,
      },

      axisLine: {
        show: false,
      },
      axisTick: {
        show: false,
      },
    },
    {
      type: 'category',
      gridIndex: 1,
      data: [''],
      boundaryGap: true,
      axisLine: { lineStyle: { color: '#777' } },
      axisPointer: {
        type: 'shadow',
        triggerTooltip: true,
      },
      name: computed(() => upbit.chartTime + 'minute candle'),
      nameLocation: 'middle',
      nameTextStyle: {
        fontStyle: 'italic',
        lineHeight: 40,
      },
    },
  ],
  yAxis: [
    {
      scale: true,
      splitNumber: 2,
      axisLine: { lineStyle: { color: '#777' } },
      axisTick: { show: false },
      axisLabel: {
        inside: false,
        formatter: '{value}\n',
      },
      position: 'right',
      splitArea: {
        show: true,
      },
    },
    {
      scale: true,
      gridIndex: 1,
      splitNumber: 2,
      axisLabel: { inside: false, formatter: '{value}\n', fontSize: 10 },
      axisTick: { show: false },
      splitLine: { show: true },
      position: 'right',
      nameTextStyle: {
        fontSize: 5,
      },
      splitArea: {
        show: true,
      },
    },
  ],
  grid: [
    {
      left: 20,
      right: 80,
      top: 110,
      height: 200,
    },
    {
      left: 20,
      right: 80,
      height: 80,
      top: 340,
    },
  ],
  dataZoom: [
    {
      show: true,
      type: 'inside',
      filterMode: 'filter',
      xAxisIndex: [0, 1],
    },
  ],
  series: [
    {
      name: 'Volume',
      type: 'bar',
      xAxisIndex: 1,
      yAxisIndex: 1,
      showBackground: true,
      data: [{ value: 0, itemStyle: { color: '' } }],
      barWidth: '30%',
      colorBy: 'data',
    },
    {
      type: 'candlestick',
      data: [[20]],
      itemStyle: {
        color: '#1261C4',
        color0: '#D24F45',
        borderColor: '#1261C4',
        borderColor0: '#D24F45',
      },
    },
  ],
  toolbox: {
    show: true,
    feature: {
      myTool1: {
        show: true,
        title: '1minute Candle of Chart',
        icon: 'path://M16,10V22h0V10m1-1H12v2h3V21H12v2h8V21H17V9Z',
        onclick: async () => {
          upbit.chartTime = '1';
        },
      },
      myTool2: {
        show: true,
        title: '3minute Candle of Chart',
        icon: 'path://M18,9H12v2h6v4H14v2h4v4H12v2h6a2,2,0,0,0,2-2V11A2,2,0,0,0,18,9Z',
        onclick: async () => {
          upbit.chartTime = '3';
        },
      },
      myTool3: {
        show: true,
        title: '5minute Candle of Chart',
        icon: 'path://M18,23H12V21h6V17H12V9h8v2H14v4h4a2,2,0,0,1,2,2v4A2,2,0,0,1,18,23Z',
        onclick: async () => {
          upbit.chartTime = '5';
        },
      },
      myTool4: {
        show: true,
        title: '10minute Candle of Chart',
        icon: 'image://https://www.printablee.com/postpic/2014/12/large-numbers_341807.jpg',
        onclick: async () => {
          upbit.chartTime = '10';
        },
      },
      myTool5: {
        show: true,
        title: '15minute Candle of Chart',
        icon: 'image://https://e7.pngegg.com/pngimages/708/761/png-clipart-number-data-number-15.png',
        onclick: async () => {
          upbit.chartTime = '15';
        },
      },
      dataZoom: {
        yAxisIndex: 'none',
      },
      saveAsImage: {},
    },
  },
});

const getCandleAPI = async () => {
  await upbit.getCandleAPI(upbit.selectCoin);

  bindingOptions.value.title.text = upbit.selectCoinFullName;
  bindingOptions.value.title.subtext = upbit.selectCoin;
  bindingOptions.value.xAxis[0].data = upbit.candleDate;
  bindingOptions.value.xAxis[1].data = upbit.candleDate;
  bindingOptions.value.series[0].data = upbit.candleVolume;
  bindingOptions.value.series[1].data = upbit.candleData;
};

let candleInterval: NodeJS.Timeout;

const startCandleInterval = () => {
  candleInterval = setInterval(() => {
    getCandleAPI();
  }, 1000);
};

watch(selectCoin, () => {
  clearInterval(candleInterval);
  startCandleInterval();
});

onMounted(async () => {
  upbit.setCandle = getCandleAPI;
  await upbit.setCandle();
  startCandleInterval();
});

onUnmounted(() => {
  clearInterval(candleInterval);
});
</script>

<style scoped lang="scss">
.chart-wrap {
  background-color: #fff;
  margin-bottom: 1rem;
}

.bg-dark {
  background-color: $dark-component !important;
  border: 1px solid $dark-component-border;
}
</style>
