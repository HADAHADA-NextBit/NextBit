<template>
  <div class="exchange-nav-wrap">
    <div class="sort-area">
      <q-btn
        v-for="data in sortTab"
        :key="data.text"
        :class="[
          selectSort.sortTarget === data.sortTarget ? selectSort.sort : '',
        ]"
        @click="[changeSort(data.sortTarget)]"
        unelevated
        tabindex="-1"
      >
        {{ data.text }}
      </q-btn>
    </div>

    <div class="coin-wrap">
      <div
        class="coin-area"
        :class="{ selected: data.market === chart.selectCoin }"
        v-for="data in markets[selectMarket]"
        :key="data.korean_name"
        @click="
          changeCoin(data.market, {
            ko: data.korean_name,
            en: data.english_name,
          })
        "
      >
        <div class="coin-name-wrap">
          <span class="coin-name">{{ data.korean_name }}</span>
          <span class="coin-market">{{ data.market }}</span>
        </div>
        <div class="now-price-wrap">
          <div class="ask-bid-control" :class="data.ab" v-if="data.box"></div>
          <strong :class="data.c">
            {{ data.tp && data.tp.toLocaleString() }}
          </strong>
        </div>
        <div class="signed-change-wrap">
          <span class="coin-info" :class="data.c">
            {{ data.scr && (data.scr * 100).toFixed(2) }}%
          </span>
          <span class="coin-market" :class="data.c">
            {{ data.scp && data.scp.toLocaleString() }}
          </span>
        </div>
        <div class="acc-trade-price24">
          <span class="coin-info">
            {{
              data.atp24h &&
              Number(data.atp24h.toFixed().slice(0, -6))
                .toLocaleString()
                .concat(' M')
            }}
          </span>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import axios from 'axios';
import { ref, onMounted, onUnmounted } from 'vue';
import { useChartStore } from 'src/stores/chart-store';
import { objSort, convertKeys, convertTickerType } from 'src/utils/rule';

const chart = useChartStore();

const selectMarket = ref('KRW');

const selectSort = ref({ sortTarget: '', sort: 'desc' });
const sortTab = [
  {
    text: '한글명',
    sortTarget: 'korean_name',
  },
  {
    text: '현재가',
    sortTarget: 'tp',
  },
  {
    text: '전일대비',
    sortTarget: 'scr',
  },
  {
    text: '거래대금',
    sortTarget: 'atp24h',
  },
];

const markets = ref<IMarkets>({
  KRW: {},
  BTC: {},
  USDT: {},
});

const loadAll = ref(false);

const controlLoadAll = () => {
  loadAll.value = true;

  setTimeout(() => {
    loadAll.value = false;
  }, 1000);
};

const getMarketAPI = async () => {
  controlLoadAll();
  const response = await axios.get<IMarketResponse[]>(
    'https://api.upbit.com/v1/market/all?isDetails=true'
  );

  for (const i in response.data) {
    const data = response.data[i] as IMarketResponse & ITickerResponse;
    const [baseMarket] = data.market.split('-');

    markets.value[baseMarket][data.market] = {
      ...data,
      control() {
        this.box = true;
        setTimeout(() => {
          this.box = false;
        }, 500);
      },
    };
  }
};

const dataSort = (sortTarget: string) => {
  markets.value[selectMarket.value] = objSort(
    markets.value[selectMarket.value],
    sortTarget,
    selectSort.value.sort
  );
};

const changeSort = (sortTarget: string) => {
  if (selectSort.value.sortTarget === sortTarget) {
    selectSort.value.sort = selectSort.value.sort === 'ask' ? 'desc' : 'ask';
  } else {
    selectSort.value.sort = 'ask';
  }
  selectSort.value.sortTarget = sortTarget;

  dataSort(sortTarget);
};

// const changeMarket = (market: string) => {
//   closeTickerSocket();
//   selectMarket.value = market;
//   controlLoadAll();
//   connectTickerSocket(Object.keys(markets.value[market]));
//   selectSort.value.sortTarget = '';
//   selectSort.value.sort = 'desc';
//   dataSort('trp');
// };

const changeCoin = (market: string, name: { ko: string; en: string }) => {
  chart.selectCoin = market;
  chart.coinFullName = name;
  // closeTickerSocket();
  // connectTickerSocket(Object.keys(markets.value[selectMarket.value]));
  loadTicker();
  chart.tickerData = {} as ITickerResponse;
  chart.reloadCandle();
  chart.reloadOrderbook();
  chart.reloadTrade();
  selectSort.value.sortTarget = '';
  selectSort.value.sort = 'desc';
  dataSort('trp');
};

const loadTicker = async () => {
  const res = await axios.get(
    `https://api.upbit.com/v1/ticker?markets=${chart.selectCoin}`
  );

  const data = convertKeys(res.data, convertTickerType);

  chart.tickerData = data[0];
};

let tickerSocket: WebSocket;

const connectTickerSocket = (codes: string[]) => {
  tickerSocket = new WebSocket('wss://api.upbit.com/websocket/v1');

  tickerSocket.onopen = (e: any) => {
    tickerSocket.send(
      `${JSON.stringify([
        { ticket: 'ticker' },
        { type: 'ticker', codes: codes },
        { format: 'SIMPLE' },
      ])}`
    );
  };

  tickerSocket.onmessage = async (payload: any) => {
    const res = (await new Response(payload.data).json()) as ITickerResponse;

    const [baseMarket] = res.cd.split('-');

    markets.value[baseMarket][res.cd] = {
      ...markets.value[baseMarket][res.cd],
      ...res,
    };

    if (loadAll.value) {
      return;
    }

    markets.value[baseMarket][res.cd].control();

    chart.tickerData = markets.value[baseMarket][chart.selectCoin];
  };
};

const closeTickerSocket = () => {
  tickerSocket.close();
};

onMounted(async () => {
  loadTicker();
  await getMarketAPI();
  connectTickerSocket(Object.keys(markets.value[selectMarket.value]));
});

onUnmounted(() => {
  closeTickerSocket();
});
</script>

<style scoped lang="scss">
.exchange-nav-wrap {
  position: relative;
  max-width: 400px;
  border: $light-chart-page-border;
  border-radius: 10px;
  overflow: hidden;
  z-index: 2;
  background-color: #fff;
  height: 100%;
}

nav {
  display: flex;
  width: 100%;
  align-items: center;
  justify-content: space-between;

  .market-change-btn {
    padding: 10px;
    height: 100%;
    width: 50%;
    border-bottom: 1px solid #eee;
    transition: 0.3s ease;
    background-color: transparent;
    border: none;
    cursor: pointer;

    &.active {
      border-bottom: 1px solid #1890ff;
      color: #1890ff;
      pointer-events: none;
    }
  }
}

.sort-area {
  display: flex;
  justify-content: space-around;
  border-bottom: $light-chart-page-border;
  height: 35px;

  > button {
    width: 100%;
    font-size: 11px;
    position: relative;
    padding: 10px 0;
    background-color: transparent;
    border: none;
    cursor: pointer;

    &:hover {
      text-decoration: underline;
    }

    &::before {
      content: '';
      position: absolute;
      top: 5px;
      right: 5px;
      left: inherit;
      bottom: inherit;
      box-shadow: inherit;
      border-radius: 0;
      border-top: none;
      border-bottom: 8px solid #ddd;
      border-right: 5px solid transparent;
      border-left: 5px solid transparent;
    }

    &::after {
      content: '';
      position: absolute;
      bottom: 5px;
      right: 5px;
      border-top: 8px solid #ddd;
      border-right: 5px solid transparent;
      border-left: 5px solid transparent;
    }

    &.ask {
      &::before {
        border-bottom-color: #0178e7;
      }
    }

    &.desc {
      &::after {
        border-top-color: #0178e7;
      }
    }
  }
}

.coin-wrap {
  height: 100%;
  overflow-x: hidden;

  .coin-area {
    display: flex;
    width: 100%;
    align-items: center;
    padding: 3px;
    cursor: pointer;

    &:not(:last-of-type) {
      border-bottom: $light-chart-page-border;
    }

    &.selected {
      background-color: rgb(221, 221, 221);
      transition: 0.2s ease;
      pointer-events: none;
    }

    > div {
      width: 25%;
      padding: 3px;
    }

    &:hover {
      background-color: #eee;
      transition: 0.2s ease;
    }
  }
}

.coin-name-wrap {
  display: flex;
  flex-direction: column;
}

.coin-name {
  font-size: 12px;
  cursor: pointer;

  &:hover {
    text-decoration: underline;
  }
}

.coin-info {
  font-size: 12px;
}

.coin-market {
  font-size: 10px;
  color: #999;
}

.now-price-wrap,
.signed-change-wrap,
.acc-trade-price24 {
  text-align: right;
  display: flex;
  flex-direction: column;
  position: relative;
}

.now-price-wrap {
  strong {
    font-size: 13px;
    line-height: 2;
  }
}

.ask-bid-control {
  position: absolute;
  width: 90%;
  height: 100%;
  top: 0;
  right: 0;
  bottom: 0;
  margin: auto;

  &.ASK {
    border: 1px solid $negative;
  }
  &.BID {
    border: 1px solid $positive;
  }
}

.RISE {
  color: $positive;
}

.FALL {
  color: $negative;
}

.body--dark {
  .exchange-nav-wrap {
    border: $dark-chart-page-border;
    background-color: $dark-09;
  }

  .sort-area {
    border-bottom: $dark-chart-page-border;
  }

  .coin-area {
    &:not(:last-of-type) {
      border-bottom: $dark-chart-page-border;
    }

    &.selected {
      background-color: #000;
    }

    &:hover {
      background-color: #000;
    }
  }
}
</style>
