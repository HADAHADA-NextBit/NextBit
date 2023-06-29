import { defineStore } from 'pinia';
import axios from 'axios';

let tradeSocket: WebSocket;

export const useChartStore = defineStore({
  id: 'chartStore',
  state: (): ISocketState => ({
    chartTime: '1',
    coinFullName: { ko: '비트코인', en: 'Bitcoin' },
    selectCoin: 'KRW-BTC',
    tradeData: {} as ISocketTradeResponse,
    tradeList: [] as ISocketTradeResponse[],
    tickerData: {} as ITickerResponse,
    markets: {} as IMarketResponse,
    reloadCandle: async () => {
      return null;
    },
    reloadTrade: async () => {
      return null;
    },
    reloadOrderbook: async () => {
      return null;
    },
  }),
  actions: {
    connectTradeSocket() {
      tradeSocket = new WebSocket('wss://api.upbit.com/websocket/v1');

      tradeSocket.onopen = (e: any) => {
        tradeSocket.send(
          `${JSON.stringify([
            { ticket: 'trade' },
            { type: 'trade', codes: ['KRW-BTC'] },
            { format: 'SIMPLE' },
          ])}`
        );
      };

      tradeSocket.onmessage = async (payload: any) => {
        const r = (await new Response(
          payload.data
        ).json()) as ISocketTradeResponse;

        this.tradeData = r;

        this.tradeList.unshift(r);

        if (this.tradeList.length > 50) {
          this.tradeList.pop();
        }
      };
    },

    disconnectTradeSocket() {
      tradeSocket.close();
    },
  },
});
