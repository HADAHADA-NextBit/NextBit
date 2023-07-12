<template>
  <q-card class="trade-wrap">
    <ul class="trade-title">
      <li
        v-for="title in ['체결시간', '체결가격', '체결량', '체결금액']"
        :key="title"
      >
        {{ title }}
      </li>
    </ul>
    <div class="trade-area" @scroll="scrolling">
      <ul class="trade-info-wrap">
        <li v-for="data in chart.tradeList" :key="data.sid" class="trade-info">
          <div class="time-zone">
            <span>{{ dayjs(data.tms).format('MM.DD') }}</span>
            <span>{{ dayjs(data.tms).format('HH:mm') }}</span>
          </div>
          <div class="trade-price">{{ data.tp.toLocaleString() }}</div>
          <div :class="data.ab">{{ data.tv }}</div>
          <div>
            {{ Number((data.tp * data.tv).toFixed()).toLocaleString() }}
          </div>
        </li>
      </ul>
    </div>
  </q-card>
</template>

<script setup lang="ts">
import { onMounted, computed, onUnmounted } from 'vue';
import axios from 'axios';
import dayjs from 'dayjs';
import { useChartStore } from 'src/stores/chart-store';
import { convertKeys, convertTradeType } from 'src/utils/rule';
import { debounce } from 'quasar';

const chart = useChartStore();

const getTradeAPI = async () => {
  const cursor = chart.tradeList?.at(-1)?.sid;
  const queryParams = `market=${chart.selectCoin}&count=20${
    cursor ? `&cursor=${cursor}` : ''
  }`;

  const res = await axios.get(
    `https://api.upbit.com/v1/trades/ticks?${queryParams}`
  );

  const data = res.data as ITradeResponse[];

  if (!cursor) {
    data.shift();
  }

  const convertedData = convertKeys(data, convertTradeType);
  chart.tradeList.push(...convertedData);
};

const scrolling = debounce((a: any) => {
  const percent = Math.floor(
    (a.target.scrollTop / (a.target.scrollHeight - a.target.clientHeight)) * 100
  );

  if (percent > 90) {
    getTradeAPI();
  }
}, 100);

const reloadTrade = async () => {
  chart.tradeList = [];
  await getTradeAPI();
  disconnect();
  chart.connectTradeSocket();
};

const disconnect = () => {
  chart.disconnectTradeSocket();
};

onMounted(async () => {
  await getTradeAPI();
  chart.connectTradeSocket();
  chart.reloadTrade = reloadTrade;
});

onUnmounted(() => {
  disconnect();
});
</script>

<style scoped lang="scss">
.trade-wrap {
  width: 100%;
  border: $light-chart-page-border;
  border-radius: 10px;
  overflow: hidden;
  height: 380px;

  .trade-area {
    height: 350px;
    overflow-y: auto;
  }
}

.trade-info-wrap {
  padding: 0;
}

.trade-title {
  padding: 5px 20px;
  display: flex;
  border-bottom: 1px solid #eee;
  list-style-type: none;
  margin: 0;

  & :first-child {
    text-align: left;
  }

  > li {
    font-size: 12px;
    width: 25%;
    text-align: right;
    margin: 0;
  }
}

.trade-info {
  width: 100%;
  display: flex;
  justify-content: space-between;
  padding: 5px 20px;

  & :first-child {
    text-align: left;
  }

  div {
    font-size: 11px;
    flex: 1;
    display: block;
    text-align: right;

    &.trade-price {
      font-weight: 700;
    }
  }

  &:nth-child(2n) {
    background-color: #f9fafc;
  }
}
.time-zone {
  > span {
    margin-right: 7px;

    &:last-child {
      font-size: 10px;
      color: #777;
    }
  }
}

.body--dark {
  .trade-wrap {
    background-color: $dark-09;
    border: $dark-chart-page-border;
  }

  .trade-title {
    border-bottom: $dark-chart-page-border;
  }

  .trade-info {
    &:nth-child(2n) {
      background-color: $dark-03;
    }
  }
}
</style>
