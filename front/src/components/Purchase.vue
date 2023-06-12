<template>
  <div class="purchase-wrap" :class="{ 'bg-dark': useCommonStore().darkMode }">
    <nav class="nav-top">
      <div class="nav-btn-wrap">
        <q-btn
          type="button"
          v-for="(text, i) in ['매수', '매도', '간편주문', '거래내역']"
          :key="i"
          :class="{ selected: navSelect === i }"
          @click="navSelect = i"
        >
          {{ text }}
        </q-btn>
      </div>
    </nav>

    <dl class="content">
      <dt class="check-option">
        <span>주문구분</span>
      </dt>
      <dd class="check-option">
        <span>
          <a
            href="#"
            v-for="(text, i) in ['지정가', '시장가', '예약-지정가']"
            :key="i"
            @click="radioSelect = i"
            :disabled="radioSelect === i"
            :class="{ disabled: radioSelect === i }"
          >
            <em :class="{ selected: radioSelect === i }"> - </em>
            <span>{{ text }}</span>
          </a>
        </span>
      </dd>

      <dt class="price">
        <span>주문가능</span>
      </dt>
      <dd class="price">
        <strong> 0 </strong>
        <span class="price-currency">{{ upbit.selectCoin.split('-')[0] }}</span>
      </dd>

      <dt class="purchase-price">
        <span
          >매수가격
          <span>({{ upbit.selectCoin.split('-')[0] }})</span>
        </span>
      </dt>
      <dd class="purchase-price">
        <q-input
          square
          outlined
          v-model="purchasePrice"
          label="매수가격"
          style="width: 100%"
          dense
        />
      </dd>

      <dt class="quantity-title">
        <span
          >주문수량
          <span>({{ upbit.selectCoin.split('-')[1] }})</span>
        </span>
      </dt>
      <dd class="quantity-">
        <q-input
          square
          outlined
          v-model="purchasePrice"
          label="주문수량"
          style="width: 100%"
          dense
        />
      </dd>
      <dd class="quantity-per">
        <q-btn
          type="button"
          v-for="(text, i) in ['10%', '25%', '50%', '100%']"
          :key="i"
          size="sm"
        >
          {{ text }}
        </q-btn>
        <q-btn type="button" size="sm">입력</q-btn>
      </dd>

      <dt class="total-amount">
        <span
          >주문총액
          <span>({{ upbit.selectCoin.split('-')[0] }})</span>
        </span>
      </dt>
      <dd class="total-amount">
        <q-input
          square
          outlined
          v-model="purchasePrice"
          label="주문총액"
          style="width: 100%"
          dense
        />
      </dd>
    </dl>

    <div class="bottom">
      <q-btn-group spread outline>
        <q-btn label="회원가입" size="2rem" square color="blue" />
        <q-btn label="로그인" size="2rem" square color="blue" />
      </q-btn-group>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useSocketUpbitStore } from 'src/stores/socket-upbit';
import { useCommonStore } from 'src/stores/common';

const upbit = useSocketUpbitStore();
const navSelect = ref(0);
const radioSelect = ref(0);
const purchasePrice = ref();
</script>

<style scoped lang="scss">
.purchase-wrap {
  width: 100%;
  height: 50rem;
  margin-bottom: 1rem;
  background-color: #fff;
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
    width: 100%;
    height: 100%;

    > button {
      width: 100%;
      height: 100%;
      font-weight: 700;

      &.selected {
        color: #d24f45;
        border-bottom: 3px solid #d24f45;
      }
    }
  }
}

.content {
  padding: 1.5rem;
  height: 75%;
  > dt {
    width: 30%;
    float: left;
    height: 3rem;
    line-height: 3rem;

    > span {
      font-size: 1.3rem;
      font-weight: 700;
      > span {
        font-size: 1.1rem;
        color: #666;
      }
    }
  }
  .check-option {
    height: 3rem;
  }

  > dd {
    width: 70%;
    float: right;
    display: flex;
    align-items: center;

    > input {
      width: 100%;
      height: 3.8rem;
      display: inline-block;
      border: 1px solid #dfe0e5;
    }

    > span {
      display: flex;
      align-items: center;
      font-size: 1.2rem;
    }

    a {
      margin-right: 2rem;
      display: flex;
      align-items: center;

      &.disabled {
        pointer-events: none;
      }
    }

    em {
      width: 1.8rem;
      height: 1.8rem;
      background-color: transparent;
      border-radius: 50em;
      margin-right: 0.8rem;
      text-indent: -999em;
      display: inline-block;
      border: 1px solid #cccdd5;
      position: relative;
      margin-top: 0.1rem;

      &::after {
        content: '';
        width: 0.8rem;
        height: 0.8rem;
        position: absolute;
        top: 0;
        left: 0;
        bottom: 0;
        right: 0;
        margin: auto;
        border-radius: 50em;
      }

      &.selected {
        border: 1px solid #1261c4;

        &::after {
          background-color: #1261c4;
        }
      }
    }
  }

  .price {
    justify-content: right;
    padding-right: 1rem;
    height: 5rem;
    line-height: 5rem;

    strong {
      margin-right: 0.3rem;
      font-size: 2rem;
    }
    .price-currency {
      font-size: 1.3rem;
      padding-top: 0.5rem;
    }
  }

  .purchase-price {
    height: 3.8rem;
    margin-bottom: 1rem;
  }

  .quantity-title {
    height: 7.8rem;
    margin-bottom: 0.5rem;
  }

  .quantity {
    height: 3.8rem;
  }

  .quantity-per {
    height: 4rem;
    margin-bottom: 0.5rem;
    display: flex;
    justify-content: space-between;

    > button {
      width: 19%;
      border: 1px solid #dfe0e5;
      height: 2.8rem;
    }
  }

  .total-amount {
    height: 3.8rem;
  }
}
.bottom {
  margin-top: 1rem;
}

.bg-dark {
  background-color: $dark-component !important;
  border: 1px solid $dark-component-border;

  .nav-top {
    background-color: $dark-component !important;
    border-bottom-color: $dark-component-border;
  }
}
</style>
