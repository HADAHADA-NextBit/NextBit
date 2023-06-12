<template>
  <div
    class="exchange-nav-wrap"
    :class="{
      'bg-dark': useCommonStore().darkMode,
      show: useCommonStore().exchangeNavShow,
    }"
  >
    <nav>
      <q-btn
        type="button"
        class="market-change-btn"
        v-for="(data, i) in currencyTab"
        :class="{ active: upbit.selectCurrency === data.currency }"
        @click="
          [
            (upbit.selectCurrency = data.currency),
            (sortBtnSelect.index = 1),
            (sortBtnSelect.sort = 'desc'),
            dataSort('trp'),
          ]
        "
        :key="i"
        :disabled="upbit.selectCurrency === data.currency"
      >
        {{ data.text }}
      </q-btn>
    </nav>
    <div class="sort-area">
      <q-btn-group>
        <q-btn
          type="button"
          :class="[sortBtnSelect.index === i ? sortBtnSelect.sort : '']"
          v-for="(data, i) in sortBtn"
          @click="[changeSort(i, data.sortTarget)]"
          :key="i"
          :label="data.text"
        />
      </q-btn-group>
    </div>
    <perfect-scrollbar>
      <div
        class="coin-area"
        :class="{ selected: data.market === upbit.selectCoin }"
        v-for="(data, i) in selectCurrency === 'KRW'
          ? krwMarkets
          : selectCurrency === 'BTC'
          ? btcMarkets
          : selectCurrency === 'USDT'
          ? usdtMarkets
          : usdtMarkets"
        :key="i"
        @click="changeCoin(data.market, data.korean_name)"
      >
        <div class="coin-name-wrap">
          <span class="coin-name">{{ data.korean_name }}</span>
          <span class="coin-market">{{ data.market }}</span>
        </div>
        <div class="now-price-wrap">
          <div
            class="ask-bid-control"
            :class="data['pbc']"
            v-if="lazyLoading"
          ></div>
          <strong :class="data.change">
            {{ data.trade_price }}
          </strong>
        </div>
        <div class="signed-change-wrap">
          <span class="coin-info" :class="data.change">
            {{ data.signed_change_rate }}%
          </span>
          <span class="coin-market" :class="data.change">
            {{ data.signed_change_price }}
          </span>
        </div>
        <div class="acc-trade-price24">
          <span class="coin-info"> {{ data.atpc24 }}</span>
        </div>
      </div>
    </perfect-scrollbar>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed, watch } from 'vue';
import { useSocketUpbitStore } from 'src/stores/socket-upbit';
import { useCommonStore } from 'src/stores/common';
import { objSort } from 'src/global';

const upbit = useSocketUpbitStore();

const krwMarkets = computed(() => upbit.krwMarkets);
const btcMarkets = computed(() => upbit.btcMarkets);
const usdtMarkets = computed(() => upbit.usdtMarkets);
const selectCurrency = computed(() => upbit.selectCurrency);
const sortBtnSelect = ref({
  index: 1,
  sort: 'desc',
});
const currencyTab = ref([
  {
    text: '원화',
    currency: 'KRW',
  },
  {
    text: 'BTC',
    currency: 'BTC',
  },
  {
    text: 'USDT',
    currency: 'USDT',
  },
]);

const sortBtn = [
  {
    text: '한글명',
    sortTarget: 'korean_name',
  },
  {
    text: '현재가',
    sortTarget: 'trp',
  },
  {
    text: '전일대비',
    sortTarget: 'signed_change_rate',
  },
  {
    text: '거래대금',
    sortTarget: 'atp24',
  },
];
const lazyLoading = ref(false);

const changeCoin = (market: string, fullName: string) => {
  useCommonStore().setAjaxBar(true);

  upbit.selectCoin = market;
  upbit.selectCoinFullName = fullName;
  upbit.setCandle();

  setTimeout(() => {
    useCommonStore().setAjaxBar(false);
  }, 100);
};

const dataSort = (sortTarget: string) => {
  setTimeout(() => {
    if (upbit.selectCurrency === 'KRW') {
      upbit.krwMarkets = objSort(
        upbit.krwMarkets,
        sortTarget,
        sortBtnSelect.value.sort
      );
    } else if (upbit.selectCurrency === 'BTC') {
      upbit.btcMarkets = objSort(
        upbit.btcMarkets,
        sortTarget,
        sortBtnSelect.value.sort
      );
    } else if (upbit.selectCurrency === 'USDT') {
      upbit.usdtMarkets = objSort(
        upbit.usdtMarkets,
        sortTarget,
        sortBtnSelect.value.sort
      );
    }
  }, 200);
};

const changeSort = (i: number, sortTarget: string) => {
  if (sortBtnSelect.value.index === i) {
    sortBtnSelect.value.sort =
      sortBtnSelect.value.sort === 'ask' ? 'desc' : 'ask';
  } else {
    sortBtnSelect.value.sort = 'ask';
  }
  sortBtnSelect.value.index = i;

  upbit.sortOptions.sortTarget = sortTarget;
  upbit.sortOptions.sort = sortBtnSelect.value.sort;
  dataSort(sortTarget);
};

let lazyLoadingTimer: ReturnType<typeof setTimeout>;

watch(selectCurrency, () => {
  clearTimeout(lazyLoadingTimer);
  lazyLoading.value = false;
  lazyLoadingTimer = setTimeout(() => {
    lazyLoading.value = true;
  }, 1000);
});

onMounted(() => {
  lazyLoadingTimer = setTimeout(() => {
    lazyLoading.value = true;
  }, 1000);
});
</script>

<style scoped lang="scss">
.exchange-nav-wrap {
  max-width: 40rem;
  width: 100%;
  background-color: #fff;
  position: relative;
  border-left: 1px solid $white-component-border;
}

.ps {
  height: calc(187rem - 8.8rem);
  position: relative;
}
nav {
  border-bottom: 1px solid $white-component-border;
  height: 5rem;
  display: flex;
  width: 100%;
  align-items: center;
  justify-content: space-between;

  .market-change-btn {
    padding: 1rem;
    height: 100%;
    width: 50%;
    border-radius: 0;

    &.active {
      border-bottom: 1px solid #1890ff;
    }
  }
}

.sort-area {
  display: flex;
  justify-content: space-around;
  border-bottom: 1px solid $white-component-border;
  width: 100%;

  .q-btn-group {
    width: 100%;
  }

  button {
    width: 100%;
    font-size: 1.1rem;
    position: relative;
    height: 4rem;

    &:hover {
      text-decoration: underline;
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
.coin-area {
  height: 4.5rem;
  display: flex;
  width: 100%;
  align-items: center;
  border-bottom: 1px solid $white-component-border;
  padding: 1rem;
  cursor: pointer;
  &.selected {
    background-color: rgb(221, 221, 221);
    transition: 0.2s ease;
    pointer-events: none;
    cursor: none;
  }

  > div {
    width: 25%;
    padding: 0.3rem;
  }

  &:hover {
    background-color: $white-component-border;
    transition: 0.2s ease;
  }
}

.coin-name-wrap {
  display: flex;
  flex-direction: column;
}

.coin-name {
  font-size: 1.1rem;
  cursor: pointer;

  &:hover {
    text-decoration: underline;
  }
}

.coin-info {
  font-size: 1.1rem;
}

.coin-market {
  font-size: 1rem;
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
    font-size: 1.3rem;
  }
}

.ask-bid-control {
  position: absolute;
  width: 90%;
  height: 100%;
  top: 0.15rem;
  right: 0;

  &.ASK {
    border: 1px solid #1261c4;
  }
  &.BID {
    border: 1px solid #d24f45;
  }
}

.rise {
  color: #d24f45;
}

.fall {
  color: #1261c4;
}

.bg-dark {
  background-color: $dark-component !important;
  border: 1px solid $dark-component-border;

  nav {
    border-bottom: 1px solid $dark-component-border;
  }

  .sort-area {
    border-bottom: 1px solid $dark-component-border;
  }
  .coin-area {
    border-bottom: 1px solid $dark-component-border;
    &.selected {
      background-color: $dark-component-select-color;
    }

    &:hover {
      background-color: $dark-component-select-color;
    }
  }
}

@media (max-width: 1350px) {
  .exchange-nav-wrap {
    position: fixed;
    right: -100%;
    top: auto;
    bottom: auto;
    left: auto;
    transition: 0.2s ease;

    &.show {
      right: 0;
    }
  }
}
</style>
