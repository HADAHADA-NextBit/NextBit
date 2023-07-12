<template>
  <div
    class="orderbook-container"
    :class="$q.dark.isActive ? 'bg-dark-09' : ''"
  >
    <div class="orderbook-layout orderbook-navi">
      <div class="price">가격</div>
      <div class="per">등락률</div>
      <div class="size">수량</div>
    </div>
    <div class="orderbook-wrap">
      <div class="ask-wrap">
        <div
          class="orderbook-layout"
          v-for="(data, i) in orderbookAsk"
          :key="i"
        >
          <div class="price">{{ data.ap.toLocaleString() }}</div>
          <div class="per">
            {{ data.per > 0 ? '+' : '' }}{{ data.per.toFixed(2) }}%
          </div>
          <div class="size">{{ data.as.toFixed(3) }}</div>
          <div
            :style="{ width: `${data.barWidth.toFixed(2)}%` }"
            class="ask-bar"
          ></div>
        </div>
      </div>
      <div class="orderbook-info text-center">
        {{ tickerData.tp?.toLocaleString() }}
      </div>
      <div class="bid-wrap">
        <div
          class="orderbook-layout"
          v-for="(data, i) in orderbookBid"
          :key="i"
        >
          <div class="price">{{ data.bp.toLocaleString() }}</div>
          <div class="per">
            {{ data.per > 0 ? '+' : '' }}{{ data.per.toFixed(2) }}%
          </div>
          <div class="size">{{ data.bs.toFixed(3) }}</div>
          <div
            :style="{ width: `${data.barWidth.toFixed(2)}%` }"
            class="bid-bar"
          ></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onUnmounted, onMounted, onBeforeMount } from 'vue';
import { useChartStore } from 'src/stores/chart-store';
import { throttle } from 'quasar';

const chart = useChartStore();
const tickerData = computed(() => chart.tickerData);
const orderbookList = ref<ISocketOrderbookResponse>(
  {} as ISocketOrderbookResponse
);

const askSizeHighest = computed(() => {
  return (
    orderbookList.value.obu &&
    Math.max(...orderbookList.value?.obu.map((e) => e.as))
  );
});
const orderbookAsk = computed(() => {
  return orderbookList.value.obu
    ?.map((e) => ({
      ap: e.ap,
      as: e.as,
      per: (e.ap - tickerData.value.pcp) / (tickerData.value.pcp / 100),
      barWidth: (e.as / askSizeHighest.value) * 100,
    }))
    .reverse();
});
const bidSizeHighest = computed(() => {
  return (
    orderbookList.value.obu &&
    Math.max(...orderbookList.value?.obu.map((e) => e.bs))
  );
});
const orderbookBid = computed(() => {
  return orderbookList.value.obu?.map((e) => ({
    bp: e.bp,
    bs: e.bs,
    per: (e.bp - tickerData.value.pcp) / (tickerData.value.pcp / 100),
    barWidth: (e.bs / bidSizeHighest.value) * 100,
  }));
});

const changeOrderbookData = throttle((r: ISocketOrderbookResponse) => {
  orderbookList.value = r;
}, 200);

let orderbookSocket: WebSocket;

const connectOrderbookSocket = () => {
  orderbookSocket = new WebSocket('wss://api.upbit.com/websocket/v1');

  orderbookSocket.onopen = (e: any) => {
    orderbookSocket.send(
      `${JSON.stringify([
        { ticket: 'orderbook' },
        { type: 'orderbook', codes: [chart.selectCoin] },
        { format: 'SIMPLE' },
      ])}`
    );
  };

  orderbookSocket.onmessage = async (payload: any) => {
    const r = (await new Response(
      payload.data
    ).json()) as ISocketOrderbookResponse;

    changeOrderbookData(r);
  };
};

const reloadOrderbook = () => {
  disconnect();
  connectOrderbookSocket();
};

const disconnect = () => {
  orderbookSocket.close();
};

onBeforeMount(() => {
  connectOrderbookSocket();
  chart.reloadOrderbook = reloadOrderbook;
});

onUnmounted(() => {
  disconnect();
});
</script>

<style scoped lang="scss">
$box-height: 40px;
$info-height: 50px;

.orderbook-container {
  height: 100%;
  border-radius: 10px;
  overflow: hidden;
  border: $light-chart-page-border;
  position: relative;
  padding-top: $box-height;
  padding-bottom: 10px;
}

.orderbook-wrap {
  overflow-y: auto;
}

.orderbook-layout {
  display: flex;
  justify-content: space-around;
  align-items: center;
  padding: 5px;
  position: relative;
  height: $box-height;

  &:not(:last-of-type) {
    border-bottom: $light-chart-page-border;
  }
}

.orderbook-navi {
  width: 100%;
  position: absolute;
  left: 0;
  top: 0;
  background-color: $dark-09;
  z-index: 2;
}

.orderbook-info {
  height: $info-height;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: $light-02;
  color: white;
}

.ask-wrap,
.bid-wrap {
  display: flex;
  flex-direction: column;
  height: 450px;
}

.price {
  width: 34%;
  z-index: 1;
}

.per {
  width: 33%;
  text-align: center;
  z-index: 1;
}

.size {
  width: 33%;
  text-align: right;
  z-index: 1;
}

.ask-bar,
.bid-bar {
  position: absolute;
  right: 0;
  height: 90%;
  z-index: 0;

  &:after {
    content: '';
    position: absolute;
    right: 0;
    width: 100%;
    height: 100%;
  }
}

.ask-bar {
  &::after {
    background-color: $positive;
    opacity: 0.6;
  }
}
.bid-bar {
  &::after {
    background-color: $negative;
    opacity: 0.6;
  }
}

.body--dark {
  .orderbook-container {
    border: $dark-chart-page-border;
  }
  .orderbook-layout {
    &:not(:last-of-type) {
      border-bottom: $dark-chart-page-border;
    }
  }

  .orderbook-info {
    background-color: #222238;
  }
}
</style>
