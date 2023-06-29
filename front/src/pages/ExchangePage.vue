<template>
  <div class="exchange-wrap">
    <div class="exchange-area">
      <div class="exchange-info-wrap contents-area">
        <ExchangeChart />
        <!-- <Chart /> -->
        <div class="center">
          <!-- <Orderbook /> -->
          <div class="up-down">
            <!-- <Purchase /> -->
            <!-- <MiniChart /> -->
          </div>
        </div>
        <!-- <Conclusion /> -->
      </div>
      <!-- <ExchangeNavigation /> -->
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onBeforeMount, watch, onUnmounted, onMounted } from 'vue';
import ExchangeChart from 'src/components/ExchangeChart.vue';
// import Chart from 'src/components/Chart.vue';
// import ExchangeNavigation from 'src/components/ExchangeNavigation.vue';
// import Conclusion from 'src/components/Trade.vue';
// import Orderbook from 'src/components/Orderbook.vue';
// import Purchase from 'src/components/Purchase.vue';
// import MiniChart from 'src/components/MiniChart.vue';
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

.body--dark {
  .exchange-wrap,
  .exchange-info-wrap {
    background-color: $dark-03;
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
