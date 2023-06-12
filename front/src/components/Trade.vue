<template>
  <div class="trade-wrap" :class="{ 'bg-dark': useCommonStore().darkMode }">
    <nav class="trade-btn-wrap">
      <q-btn
        type="button"
        v-for="(header, i) in ['체결', '일별']"
        :key="header"
        :class="{ selected: btnSelect === i }"
        @click="btnSelect = i"
      >
        {{ header }}
      </q-btn>
    </nav>

    <q-table
      class="my-sticky-dynamic my-sticky-header-table"
      table-class="pc-scroll"
      dense
      style="height: 41rem"
      :rows="upbit.tradeList"
      :columns="columns"
      :loading="loading"
      row-key="index"
      virtual-scroll
      :rows-per-page-options="[0]"
    />
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useSocketUpbitStore } from 'src/stores/socket-upbit';
import { useCommonStore } from 'src/stores/common';

const upbit = useSocketUpbitStore();

const btnSelect = ref(0);
const loading = ref(false);

const columns = [
  {
    name: 'ttm',
    label: '체결시간(UTC)',
    field: 'ttm',
  },
  {
    name: 'tcp',
    label: '체결가격(KRW)',
    field: 'tcp',
  },
  {
    name: 'tv',
    label: '체결량(BTC)',
    field: 'tv',
  },
  {
    name: 'trp',
    label: '체결금액(KRW)',
    field: 'trp',
  },
];
</script>

<style scoped lang="scss">
.ps {
  height: 40rem;
  position: relative;
}

.trade-wrap {
  width: 100%;
  background-color: #fff;
}

.trade-btn-wrap {
  display: flex;

  > button {
    width: 50%;
    height: 4rem;
    border-bottom: 1px solid #ccc;
    font-weight: 700;

    &.selected {
      border-bottom: 2px solid cornflowerblue;
      color: cornflowerblue;
    }

    &:hover {
      text-decoration: underline;
    }
  }
}

.trade-title {
  padding: 0.5rem 2rem;
  display: flex;
  background-color: #f9fafc;
  border-bottom: 1px solid #eee;

  > li {
    font-size: 1.2rem;
    color: rgb(63, 63, 63);
    width: 30%;
    text-align: right;
  }
}

.trade-info {
  width: 100%;
  display: flex;
  justify-content: space-between;
  padding: 0.5rem 2rem;

  span {
    font-size: 1.1rem;
    width: 30%;
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
  display: flex;
  width: 10% !important;
  text-align: left !important;

  > span {
    margin-right: 0.7rem;

    &:last-child {
      font-size: 1rem;
      color: #777;
    }
  }
}

.bg-dark {
  border: 1px solid $dark-component-border;

  .trade-btn-wrap {
    > button {
      background-color: $dark-component;
      border-bottom-color: $dark-component-border;

      &.selected {
        border-bottom-color: $border-bottom-color;
      }
    }
  }
}
.q-dark {
  background-color: $dark-component;
}
</style>
