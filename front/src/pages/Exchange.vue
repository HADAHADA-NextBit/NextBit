<template>
  <div class="exchange-wrap" :class="{ 'bg-dark': useCommonStore().darkMode }">
    <div class="exchange-area">
      <div class="exchange-info-wrap contents-area">
        <Chart />
        <div class="center">
          <Orderbook />
          <div class="up-down">
            <Purchase />
            <MiniChart />
          </div>
        </div>
        <Conclusion />
      </div>
      <ExchangeNavigation />
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onBeforeMount, watch, onUnmounted, onMounted } from 'vue';
import { useSocketUpbitStore } from 'src/stores/socket-upbit';
import { useCommonStore } from 'src/stores/common';
import Chart from 'src/components/Chart.vue';
import ExchangeNavigation from 'src/components/ExchangeNavigation.vue';
import Conclusion from 'src/components/Trade.vue';
import Orderbook from 'src/components/Orderbook.vue';
import Purchase from 'src/components/Purchase.vue';
import MiniChart from 'src/components/MiniChart.vue';

const upbit = useSocketUpbitStore();
const selectCurrency = computed(() => upbit.selectCurrency);
const selectCoin = computed(() => upbit.selectCoin);

const reConnect = () => {
  upbit.socketTradeConnect();
  upbit.getTradeSocket([selectCoin.value]);
  upbit.socketOrderbookConnect();
  upbit.getOrderbookSocket([selectCoin.value]);
};

watch(selectCurrency, () => {
  upbit.closeTickerSocket();
  upbit.socketTickerConnect();
  if (selectCurrency.value === 'KRW') {
    upbit.getTickerSocket(Object.keys(upbit.krwMarkets));
  } else if (selectCurrency.value === 'BTC') {
    upbit.getTickerSocket(Object.keys(upbit.btcMarkets));
  } else if (selectCurrency.value === 'USDT') {
    upbit.getTickerSocket(Object.keys(upbit.usdtMarkets));
  }
});

watch(selectCoin, () => {
  upbit.getTickerAPI();
  upbit.closeOrderbookSocket();
  upbit.closeTradeSocket();
  reConnect();
});

onBeforeMount(async () => {
  useCommonStore().setAjaxBar(true);
  upbit.socketTickerConnect();
  await upbit.getMarketAPI();
  upbit.getTickerAPI();
  reConnect();
});

onMounted(() => {
  setTimeout(() => {
    useCommonStore().setAjaxBar(false);
  }, 1000);
});

onUnmounted(() => {
  document.title = 'QUASAR-UPBIT';
  upbit.closeTickerSocket();
  upbit.closeTradeSocket();
  upbit.closeOrderbookSocket();
  setTimeout(() => {
    upbit.$reset();
  }, 1000);
});
</script>

<style scoped lang="scss">
.exchange-wrap {
  background-color: #e9ecf1;
  width: 100%;
  position: relative;
  padding: 0 0 10rem;
}
.exchange-area {
  max-width: 150rem;
  width: 100%;
  display: flex;
  justify-content: center;
  margin: auto;
}

.contents-area {
  background-color: #fff;
}

.exchange-info-wrap {
  width: 70%;
  margin-right: 1rem;
  background-color: #e9ecf1;
}

.center {
  display: flex;
  margin-bottom: 1rem;
}

.up-down {
  width: 50%;
  height: 100%;
}

.bg-dark {
  .exchange-info-wrap {
    background-color: $dark;
  }
}

@media (max-width: 1350px) {
  .exchange-info-wrap {
    width: 100%;
  }
}

@media (max-width: 992px) {
  .center {
    display: block;
  }

  .orderbook-wrap {
    width: 100%;
    margin-bottom: 1rem;
  }

  .up-down {
    width: 100%;
    display: flex;
  }

  .purchase-wrap {
    width: 100%;
  }

  .mini-chart-wrap {
    width: 50%;
    height: 50rem;
  }
}
</style>
