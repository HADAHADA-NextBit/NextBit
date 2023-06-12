<template>
  <div class="orderbook-wrap" :class="{ 'bg-dark': useCommonStore().darkMode }">
    <nav class="nav-top">
      <div class="nav-btn-wrap">
        <q-btn
          type="button"
          v-for="(data, i) in navBtn"
          :key="i"
          :class="{ selected: navSelect === i }"
          @click="navSelect = i"
        >
          {{ data.text }}
        </q-btn>
      </div>

      <button type="button" class="filter">모아보기</button>
    </nav>
    <perfect-scrollbar ref="scrollRef" :options="scrollOptions">
      <Echarts
        :bindingOptions="bindingOptions"
        style="width: 100%; height: 1000px"
      />
      <div class="order-price-wrap" :key="upbit.selectMarketInfo.hp">
        <ul class="ask">
          <li
            v-for="data in upbit.orderbookList.ask.ap"
            :key="data.v"
            :class="[data.c, { box: data.v === upbit.selectMarketInfo.tp }]"
          >
            <span>
              {{ comma(data.v) }}
            </span>
            <span class="per">
              {{ data.p }}
            </span>
          </li>
        </ul>
        <ul class="bid">
          <li
            v-for="data in upbit.orderbookList.bid.bp"
            :key="data.v"
            :class="[data.c, { box: data.v === upbit.selectMarketInfo.tp }]"
          >
            <span>
              {{ comma(data.v) }}
            </span>
            <span class="per">
              {{ data.p }}
            </span>
          </li>
        </ul>
      </div>
      <div class="trade-wrap">
        <div class="info">
          <span>체결가</span>
          <span>체결량</span>
        </div>
        <ul class="trade">
          <li v-for="(data, i) in upbit.tradeList.slice(0, 22)" :key="i">
            <span class="trade-price">{{ data.tcp }}</span>
            <span class="trade-volume" :class="data.ab">{{
              data.tv.toFixed(5)
            }}</span>
          </li>
        </ul>
      </div>

      <div class="coin-info-wrap">
        <ul class="trade-amount-wrap">
          <li>
            <span> 거래량 </span>
            <span>
              {{ comma(Math.round(upbit.selectMarketInfo.atv24h)) }}
              <span style="font-size: 1rem; color: #888">
                {{ upbit.selectCoin.split('-')[1] }}
              </span>
            </span>
          </li>
          <li>
            <span> 거래대금 </span>
            <div>
              <span>
                {{
                  comma(
                    String(upbit.selectMarketInfo.atp24h)
                      .split('.')[0]
                      .slice(0, -6)
                  )
                }}
                <span style="font-size: 1rem; color: #888">백만원</span>
              </span>
              <span>(최근 24시간)</span>
            </div>
          </li>
        </ul>
        <ul class="highest-lowest-wrap">
          <li>
            <span> 52주 최고</span>
            <div>
              <span class="BID">
                {{ comma(upbit.selectMarketInfo.h52wp) }}
              </span>
              <span>({{ upbit.selectMarketInfo.h52wdt }})</span>
            </div>
          </li>
          <li>
            <span> 52주 최고</span>
            <div>
              <span class="ASK">
                {{ comma(upbit.selectMarketInfo.l52wp) }}
              </span>
              <span> ({{ upbit.selectMarketInfo.l52wdt }}) </span>
            </div>
          </li>
        </ul>
        <ul class="today-info-wrap">
          <li>
            <span>전일종가</span>
            <span>{{ comma(upbit.selectMarketInfo.pcp) }} </span>
          </li>
          <li>
            <span>당일고가</span>
            <div>
              <span class="BID">{{ comma(upbit.selectMarketInfo.hp) }} </span>
              <em class="BID"
                >+{{
                  (
                    (upbit.selectMarketInfo.hp - upbit.selectMarketInfo.pcp) /
                    (upbit.selectMarketInfo.pcp / 100)
                  ).toFixed(2)
                }}%</em
              >
            </div>
          </li>
          <li>
            <span>당일저가</span>
            <div>
              <span class="ASK">{{ comma(upbit.selectMarketInfo.lp) }} </span>
              <em class="ASK"
                >{{
                  (
                    (upbit.selectMarketInfo.lp - upbit.selectMarketInfo.pcp) /
                    (upbit.selectMarketInfo.pcp / 100)
                  ).toFixed(2)
                }}%</em
              >
            </div>
          </li>
        </ul>
      </div>
      <!-- </div> -->
    </perfect-scrollbar>
    <nav class="nav-bottom">
      <span>{{ upbit.orderbookList.tas }}</span>
      <q-btn type="button">수량({{ upbit.selectCoin }})</q-btn>
      <span>{{ upbit.orderbookList.tbs }}</span>
    </nav>
  </div>
</template>

<script setup lang="ts">
import Echarts from 'src/components/Echarts.vue';
import { ref, computed, watch, onMounted } from 'vue';
import { useSocketUpbitStore } from 'src/stores/socket-upbit';
import { useCommonStore } from 'src/stores/common';
import { comma } from 'src/global';

const upbit = useSocketUpbitStore();
const selectCoin = computed(() => upbit.selectCoin);
const scrollRef = ref();
const scrollOptions = {
  wheelSpeed: 0.7,
};
const darkMode = computed(() => useCommonStore().darkMode);

const navBtn = [
  {
    text: '일반호가',
  },
  {
    text: '누적호가',
  },
  {
    text: '호가주문',
  },
];
const navSelect = ref(0);

const bindingOptions = ref({
  xAxis: [
    {
      type: 'value',
      inverse: true,
      show: false,
    },
    {
      gridIndex: 1,
      type: 'value',
      inverse: false,
      show: false,
    },
  ],
  yAxis: [
    {
      type: 'category',
      position: 'right',
      show: false,
    },
    {
      gridIndex: 1,
      type: 'category',
      position: 'left',
      show: false,
    },
  ],
  grid: [
    {
      left: 0,
      right: '68%',
      top: 0,
      height: 500,
    },
    {
      left: '68%',
      right: 0,
      bottom: 0,
      height: 500,
    },
  ],
  series: [
    {
      data: computed(() => upbit.orderbookList.ask.as),
      type: 'bar',
      barWidth: '30',
      showBackground: true,
      backgroundStyle: {
        color: '#ECF3FA',
      },
      itemStyle: {
        color: '#CCDDF2',
      },
      label: {
        show: true,
        position: 'insideRight',
      },
    },
    {
      xAxisIndex: 1,
      yAxisIndex: 1,
      data: computed(() => upbit.orderbookList.bid.bs),
      type: 'bar',
      barWidth: '30',
      showBackground: true,
      backgroundStyle: {
        color: '#F1EAEB',
      },
      itemStyle: {
        color: '#EAD2CF',
      },
      label: {
        show: true,
        position: 'insideLeft',
      },
    },
  ],
});

const setScroll = () => {
  scrollRef.value.$el.scrollTop = 100;
};

watch(selectCoin, () => {
  setScroll();
});

watch(darkMode, (darkMode: boolean) => {
  if (darkMode) {
    bindingOptions.value.series[0].backgroundStyle.color = '#0e1b2b';
    bindingOptions.value.series[0].itemStyle.color = '#0f3460';
    bindingOptions.value.series[1].backgroundStyle.color = '#291b1d';
    bindingOptions.value.series[1].itemStyle.color = '#763632';
  } else {
    bindingOptions.value.series[0].backgroundStyle.color = '#F1EAEB';
    bindingOptions.value.series[0].itemStyle.color = '#CCDDF2';
    bindingOptions.value.series[1].backgroundStyle.color = '#F1EAEB';
    bindingOptions.value.series[1].itemStyle.color = '#EAD2CF';
  }
});

onMounted(() => {
  setScroll();
});
</script>

<style scoped lang="scss">
.orderbook-wrap {
  width: 50%;
  margin-right: 1rem;
  background-color: #fff;
}

.ps {
  height: 80rem;
  position: relative;
}

.order-price-wrap {
  position: absolute;
  width: calc(36% - 0.3rem);
  height: 100%;
  background-color: #fff;
  top: 0;
  left: 0;
  right: 0;
  margin: auto;
  height: 100rem;

  > ul {
    &.ask,
    &.bid {
      height: 50%;
      display: flex;
      flex-direction: column;
      justify-content: space-around;

      > li {
        text-align: center;
        height: 3rem;
        line-height: 2;
        font-weight: 700;
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding-left: 1.5rem;
        padding-right: 0.5rem;
        position: relative;

        &.trade {
          border: 1px solid #000;
        }

        > span {
          width: 50%;
          text-align: right;
          font-size: 1.3rem;
          &.per {
            font-size: 1.2rem;
          }
        }
      }

      .box {
        position: relative;

        &::before {
          content: '';
          width: 100%;
          height: 100%;
          position: absolute;
          box-sizing: border-box;
          top: 0;
          left: 0;
          border: 2px solid #000;
        }

        &::after {
          content: '';
          position: absolute;
          top: 0;
          bottom: 0;
          left: 0;
          width: 0px;
          height: 0px;
          margin: auto;
          border-top: 7px solid transparent;
          border-bottom: 7px solid transparent;
          border-left: 7px solid #000;
        }
      }
    }

    &.ask {
      > li {
        background-color: #ecf3fa;
      }
    }

    &.bid {
      > li {
        background-color: #f1eaeb;
      }
    }
  }
}

.trade-wrap {
  width: 32%;
  position: absolute;
  left: 0;
  bottom: -20.2rem;
  height: 50rem;
  overflow: hidden;

  .info {
    background-color: #f2f2f2;
    height: 3rem;

    > span {
      display: inline-block;
      width: 50%;
      text-align: center;
      line-height: 2.3;
      font-size: 1.3rem;
    }
  }

  .trade {
    height: 47rem;
    overflow: hidden;
    > li {
      margin-bottom: 0.3rem;
      span {
        display: inline-block;
        text-align: center;
        width: 50%;
        font-size: 1.1rem;
      }
    }
  }
}

.coin-info-wrap {
  width: 32%;
  position: absolute;
  right: 0;
  bottom: 30rem;
  font-size: 1.1rem;
  padding: 0.7rem;

  > ul {
    padding-bottom: 1.6rem;

    &:nth-child(2) {
      padding: 0 0 1.6rem;
      border-top: 1px solid #eee;
      border-bottom: 1px solid #eee;
    }
    > li {
      display: flex;
      justify-content: space-between;
      margin-top: 1.6rem;

      > div {
        display: flex;
        flex-direction: column;
        text-align: right;

        > span {
          &:last-child {
            color: #999;
            font-size: 1.1rem;
          }
        }

        > em {
          font-style: normal;
          font-size: 1.1rem;
        }
      }
    }
  }
}

.nav-top {
  height: 4rem;
  background-color: #fff;
  border-bottom: 1px solid #eee;
  z-index: 1;
  display: flex;
  justify-content: space-between;

  .nav-btn-wrap {
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 60%;
    height: 100%;

    > button {
      width: 100%;
      height: 100%;
      font-weight: 700;

      &.selected {
        color: #0062df;
        border-bottom: 3px solid #0062df;
      }
    }
  }

  .filter {
    margin-right: 2rem;
    font-size: 1.1rem;
    color: #555;
  }
}

.nav-bottom {
  width: 100%;
  height: 4rem;
  display: flex;
  background-color: #f2f2f2;

  > button {
    width: 40%;
    height: 100%;
    border-left: 1px solid #ccc;
    border-right: 1px solid #ccc;
    margin-left: 0.1rem;
  }

  > span {
    display: inline-block;
    width: 30%;
    height: 100%;
    text-align: right;
    line-height: 2.8;
    padding-right: 1rem;

    &:last-child {
      text-align: left;
      padding-left: 1rem;
    }
  }
}

.bg-dark {
  background-color: $dark-component !important;
  border: 1px solid $dark-component-border;

  .nav-top {
    background-color: $dark-component;
    border-bottom-color: $dark-component-border;
  }
  .order-price-wrap {
    background-color: transparent;
  }
  .trade-wrap {
    .info {
      background-color: $dark-component;
    }
  }

  .nav-bottom {
    background-color: $dark-component;

    > button {
      border-left: 1px solid $dark-component-border;
      border-right: 1px solid $dark-component-border;
    }
  }

  .trade-wrap {
    background-color: $dark-component;
  }

  .order-price-wrap {
    .box {
      &::before {
        border-color: #fff !important;
      }

      &::after {
        border-left-color: #fff !important;
      }
    }
    > ul.ask {
      > li {
        background-color: $dark-chart-ask-color !important;
      }
    }
    > ul.bid {
      > li {
        background-color: $dark-chart-bid-color !important;
      }
    }
  }
}

@media (max-width: 720px) {
  .order-price-wrap {
    > ul {
      &.ask,
      &.bid {
        > li {
          > span {
            width: 100%;
            text-align: center;
          }
        }
      }
    }
  }
  .per {
    display: none;
  }
}
</style>
