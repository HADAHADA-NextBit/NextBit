<template>
  <div class="exchange-nav-controller">
    <q-fab
      flat
      icon="menu_open"
      active-icon="close"
      padding="5px"
      round
      glossy
      class="shadow-12"
      @click="listControl = !listControl"
    />
  </div>

  <CoinInfo />

  <div class="exchange-page">
    <div class="bg" :class="{ show: listControl }"></div>

    <div class="orderbook">
      <ExchangeOrderbook />
    </div>

    <div class="chart spacing-y-xl">
      <ExchangeChart />

      <ExchangeForm />

      <ExchangeTrade />
    </div>

    <div class="market" :class="{ show: listControl }">
      <MarketList />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { debounce } from 'quasar';
import ExchangeChart from 'src/components/chart/ExchangeChart.vue';
import ExchangeOrderbook from 'src/components/chart/ExchangeOrderbook.vue';
import MarketList from 'src/components/chart/MarketList.vue';
import ExchangeTrade from 'src/components/chart/ExchangeTrade.vue';
import CoinInfo from 'src/components/chart/CoinInfo.vue';
import ExchangeForm from 'src/components/chart/ExchangeForm.vue';

const listControl = ref(false);

window.addEventListener(
  'resize',
  debounce(() => {
    if (window.innerWidth > 1200) {
      listControl.value = false;
    }
  }, 200)
);
</script>

<style scoped lang="scss">
.exchange-page {
  margin-top: 20px;
  display: flex;
  position: relative;
}

.bg {
  position: fixed;
  top: 0;
  left: 0;
  background-color: #00000080;
  width: 100%;
  height: 100%;
  z-index: 2;
  opacity: 0;
  visibility: hidden;
  transition: 0.3s ease;

  &.show {
    opacity: 1;
    visibility: visible;
  }
}

.orderbook {
  width: 25%;
  height: 100%;
}

.chart {
  width: 50%;
  display: flex;
  flex-direction: column;
  padding: 0 10px;
}

.market {
  width: 25%;
  min-width: 320px;
  height: 1240px;
}

.exchange-nav-controller {
  display: none;
}

@media (max-width: 1200px) {
  .orderbook {
    width: 30%;
  }

  .chart {
    width: 70%;
    height: inherit;
  }

  .market {
    position: absolute;
    right: 0;
    top: 10px;
    transform: translateX(150%);
    transition: 0.3s ease;
    opacity: 0;
    height: 75vh;
    z-index: 3;

    &.show {
      opacity: 1;
      transform: translateX(0);
    }
  }

  .exchange-nav-controller {
    display: block;
    position: fixed;
    top: 70px;
    right: 10px;
    z-index: 10;
  }
}

@media (max-width: 768px) {
  .exchange-page {
    flex-direction: column;
  }

  .orderbook {
    order: 2;
    margin-top: 20px;
    width: 100%;
  }

  .chart {
    order: 1;
    padding: 0;
    width: 100%;
  }
}
</style>
