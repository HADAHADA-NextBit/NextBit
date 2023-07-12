<template>
  <EchartsDefault :options="options" style="width: 100%; height: 400px" />
</template>

<script setup lang="ts">
import dayjs from 'dayjs';
import axios from 'axios';
import { ref, onBeforeMount, watch, computed } from 'vue';
import { useChartStore } from 'src/stores/chart-store';
import EchartsDefault from 'src/components/chart/EchartsDefault.vue';
import { colors } from 'src/utils/rule';

const chart = useChartStore();

const candleData = ref<{ [key: string]: number[] }>({});
const candleVolume = ref<{ [key: string]: number[] }>({});
const tradeData = computed(() => chart.tradeData);
const stop = ref(false);

const options = ref({
  animationDuration: 100,
  animationDurationUpdate: 100,
  title: {
    // text: chart.coinFullName[locale.value],
    // subtext: 'KRW-BTC',
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
      data: <string[]>[],
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
      data: [],
      boundaryGap: true,
      axisLine: { lineStyle: { color: '#777' } },
      axisPointer: {
        type: 'shadow',
        triggerTooltip: true,
      },
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
      top: 10,
      height: 290,
    },
    {
      left: 20,
      right: 80,
      height: 60,
      top: 310,
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
      type: 'bar',
      xAxisIndex: 1,
      yAxisIndex: 1,
      data: <any>[],
      itemStyle: {
        color: (value: any) =>
          value.data[2] === 0
            ? colors.same
            : value.data[2] === 1
            ? colors.up
            : colors.down,
      },
      barWidth: '70%',
      colorBy: 'data',
    },
    {
      type: 'candlestick',
      data: <any>[],
      itemStyle: {
        color: colors.down,
        color0: colors.up,
        borderColor: colors.down,
        borderColor0: colors.up,
        borderColorDoji: colors.same,
      },
      barWidth: '70%',
      markLine: {
        symbol: 'none',
        data: [
          {
            yAxis: 0,
            label: {
              color: 'white',
              fontSize: 8,
              lineHeight: 14,
              padding: [0, 4],
              borderRadius: 4,
              formatter: ({ value }: any) => value.toLocaleString(),
            },
            lineStyle: {
              type: [0.5],
              dashOffset: 0.5,
            },
          },
        ],
      },
    },
  ],
});

const updateMarkLine = <T>(
  markLine: any,
  data: T[],
  color: (item: T[]) => string
) => {
  markLine.data[0].yAxis = data[0];
  markLine.data[0].lineStyle.color = color(data);
  markLine.data[0].label.backgroundColor = color(data);
};

const getCandleAPI = async (count = 50) => {
  const response = await axios.get<ICandleStickResponse[]>(
    `https://api.upbit.com/v1/candles/minutes/1?market=${chart.selectCoin}&count=${count}`
  );

  for (const i in response.data.reverse()) {
    const data = response.data[i];
    const time = dayjs(data.candle_date_time_kst).format('HH:mm');
    const tp = data.trade_price;
    const op = data.opening_price;

    candleData.value[time] = [tp, op, data.low_price, data.high_price];

    const volumnKey = Object.keys(candleVolume.value);
    candleVolume.value[time] = [
      volumnKey.indexOf(time) === -1
        ? Object.keys(candleVolume.value).length
        : volumnKey.indexOf(time),
      Math.round(data.candle_acc_trade_volume * 1000) / 1000,
      tp === op ? 0 : tp > op ? 1 : 2,
    ];
  }

  options.value.xAxis.forEach((axis) => {
    axis.data = Object.keys(candleData.value);
  });
  options.value.series[0].data = Object.values(candleVolume.value);
  options.value.series[1].data = Object.values(candleData.value);

  updateMarkLine(
    options.value.series[1].markLine,
    Object.values(candleData.value)[Object.values(candleData.value).length - 1],
    ([tp, op]) => (tp === op ? colors.same : tp > op ? colors.up : colors.down)
  );

  stop.value = false;
};

watch(
  tradeData,
  (r: ISocketTradeResponse) => {
    if (stop.value) {
      return;
    }

    const time = dayjs(r.tms).format('HH:mm');

    if (!candleData.value[time] || !candleVolume.value[time]) {
      stop.value = true;
      getCandleAPI(2);
      return;
    }

    const kline = candleData.value[time];
    const op = kline[1];
    const volume = candleVolume.value[time];
    const { tv, tp } = r;

    volume[1] = Math.round((volume[1] + tv) * 1000) / 1000;
    volume[2] = tp === op ? 0 : tp > op ? 1 : 2;

    kline[0] = tp;
    kline[2] = Math.min(kline[2], tp);
    kline[3] = Math.max(kline[3], tp);

    updateMarkLine(options.value.series[1].markLine, kline, ([tp, op]) =>
      tp === op ? colors.same : tp > op ? colors.up : colors.down
    );
  },
  { deep: true }
);

const reloadCandle = () => {
  candleData.value = {};
  candleVolume.value = {};
  getCandleAPI();
};

onBeforeMount(() => {
  getCandleAPI();
  chart.reloadCandle = reloadCandle;
});
</script>

<style scoped lang="scss"></style>
