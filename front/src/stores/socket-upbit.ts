import { defineStore } from 'pinia';
import axios from 'axios';
import { comma, objSort, getKoreaDateTime } from 'src/global';

let tickerSocket: WebSocket;
let tradeSocket: WebSocket;
let orderbookSocket: WebSocket;
let timer: ReturnType<typeof setTimeout>;

export const useSocketUpbitStore = defineStore({
  id: 'socketStore',
  state: (): ISocketState => ({
    chartTime: '1',
    sortOptions: {
      sortTarget: 'trp',
      sort: 'desc',
    },
    selectCoinFullName: '비트코인',
    selectCurrency: 'KRW',
    selectCoin: 'KRW-BTC',
    selectMarketInfo: {
      ty: '',
      cd: '',
      op: '',
      hp: 0,
      lp: 0,
      tp: 0,
      pcp: 0,
      c: '',
      cp: '',
      scp: 0,
      cr: '',
      scr: '',
      tv: '',
      atv: '',
      atv24h: 0,
      atp: '',
      atp24h: 0,
      tdt: '',
      ttm: '',
      ttms: '',
      ab: '',
      aav: '',
      abv: '',
      h52wp: 0,
      h52wdt: '',
      l52wp: 0,
      l52wdt: '',
      ts: '',
      ms: '',
      msfi: '',
      its: '',
      dd: '',
      mw: '',
      tms: '',
      st: '',
    },
    krwMarkets: {},
    btcMarkets: {},
    usdtMarkets: {},
    candleDate: [],
    candleData: [],
    candleVolume: [],
    nowCurrency: {},
    tradeList: [
      {
        ab: '',
        c: '',
        cd: '',
        cp: 0,
        pcp: 0,
        sid: 0,
        st: '',
        td: '',
        tms: 0,
        tp: 0,
        tcp: '',
        ttm: '',
        ttms: 0,
        tv: 0,
        ty: '',
        trp: '',
      },
    ],
    orderbookList: {
      ask: {
        ap: [],
        as: [],
      },
      bid: {
        bp: [],
        bs: [],
      },
      tas: '',
      tbs: '',
    },
    setCandle: async () => {
      //
    },
  }),
  actions: {
    socketTickerConnect() {
      tickerSocket = new WebSocket('wss://api.upbit.com/websocket/v1');
    },
    socketTradeConnect() {
      tradeSocket = new WebSocket('wss://api.upbit.com/websocket/v1');
    },
    socketOrderbookConnect() {
      orderbookSocket = new WebSocket('wss://api.upbit.com/websocket/v1');
    },

    async getMarketAPI() {
      const response = await axios.get(
        'https://api.upbit.com/v1/market/all?isDetails=true'
      );
      const data: Array<IGetMarkets> = response.data;

      for (const i in data) {
        if (data[i].market.split('-')[0] === 'KRW') {
          this.krwMarkets[data[i].market] = data[i];
        } else if (data[i].market.split('-')[0] === 'BTC') {
          this.btcMarkets[data[i].market] = data[i];
        } else if (data[i].market.split('-')[0] === 'USDT') {
          this.usdtMarkets[data[i].market] = data[i];
        }
      }

      this.getTickerSocket(Object.keys(this.krwMarkets));
    },

    async getCandleAPI(codes: string) {
      const response = await axios.get(
        `https://api.upbit.com/v1/candles/minutes/${this.chartTime}?market=${codes}&count=50`
      );
      response.data.reverse();
      const data = response.data;

      const candleDate = [];
      const candleData = [];
      const candleVolume = [];
      for (const i in data) {
        candleDate.push(data[i].candle_date_time_kst.split('T')[1]);
        candleData.push([
          data[i].trade_price,
          data[i].opening_price,
          data[i].low_price,
          data[i].high_price,
        ]);
        candleVolume.push({
          value: data[i].candle_acc_trade_volume,
          itemStyle: {
            color:
              data[i].opening_price - data[i].trade_price < 0
                ? '#D24F45'
                : data[i].opening_price - data[i].trade_price === 0
                ? '#000'
                : '#1261C4',
          },
        });
      }
      this.candleDate = candleDate;
      this.candleData = candleData;
      this.candleVolume = candleVolume;
    },

    setControlPriceBox(cd: string, selectCurrency: string) {
      if (selectCurrency === 'KRW') {
        timer = setTimeout(() => {
          this.krwMarkets[cd]['pbc'] = '';
        }, 500);
      } else if (selectCurrency === 'BTC') {
        timer = setTimeout(() => {
          this.btcMarkets[cd]['pbc'] = '';
        }, 500);
      } else if (selectCurrency === 'USDT') {
        timer = setTimeout(() => {
          this.usdtMarkets[cd]['pbc'] = '';
        }, 500);
      }
    },

    async getTickerAPI() {
      try {
        const r = await axios.get(
          `https://api.upbit.com/v1/ticker?markets=${this.selectCoin}`
        );
        const d: IGetTickerAPI = r.data[0];
        this.selectMarketInfo.tp = d.trade_price;
        this.selectMarketInfo.atv24h = d.acc_trade_volume_24h;
        this.selectMarketInfo.atp24h = d.acc_trade_price_24h;
        this.selectMarketInfo.h52wp = d.highest_52_week_price;
        this.selectMarketInfo.l52wp = d.lowest_52_week_price;
        this.selectMarketInfo.h52wdt = d.highest_52_week_date;
        this.selectMarketInfo.l52wdt = d.lowest_52_week_date;
        this.selectMarketInfo.pcp = d.prev_closing_price;
        this.selectMarketInfo.scp = d.signed_change_price;
        this.selectMarketInfo.hp = d.high_price;
        this.selectMarketInfo.lp = d.low_price;
        document.title = comma(d.trade_price) + ' ' + this.selectCoin;
      } catch (e) {
        console.error(e);
      }
    },

    getTickerSocket(codes: Array<string>) {
      clearTimeout(timer);
      try {
        tickerSocket.onopen = () => {
          tickerSocket.send(
            `${JSON.stringify([
              { ticket: 'ticker' },
              { type: 'ticker', codes: codes },
              { format: 'SIMPLE' },
            ])}`
          );
        };

        if (this.selectCurrency === 'KRW') {
          tickerSocket.onmessage = async (payload: any) => {
            const r = await new Response(payload.data).json();
            if (this.selectCoin === r.cd) {
              this.selectMarketInfo = r;
              document.title = comma(r.tp) + ' ' + this.selectCoin;
            }
            this.krwMarkets[r.cd]['change'] = r.c.toLowerCase();
            this.krwMarkets[r.cd]['pbc'] = r.ab;
            this.setControlPriceBox(r.cd, 'KRW');
            this.krwMarkets[r.cd]['trade_price'] = comma(r.tp);
            this.krwMarkets[r.cd]['trp'] = r.tp;
            this.krwMarkets[r.cd]['signed_change_rate'] = Number(
              (r.scr * 100)?.toFixed(2)
            );
            this.krwMarkets[r.cd]['signed_change_price'] = comma(r.scp);
            const n = String(r.atp24h).split('.')[0].slice(0, -6);
            this.krwMarkets[r.cd]['atp24'] = Number(n);
            this.krwMarkets[r.cd]['atpc24'] = comma(n) + '백만';
          };
        } else if (this.selectCurrency === 'BTC') {
          tickerSocket.onmessage = async (payload: any) => {
            const r = await new Response(payload.data).json();
            if (this.selectCoin === r.cd) {
              this.selectMarketInfo = r;
              document.title = comma(r.tp) + ' ' + this.selectCoin;
            }
            this.btcMarkets[r.cd]['change'] = r.c.toLowerCase();
            this.btcMarkets[r.cd]['pbc'] = r.ab ? r.ab : '';
            this.setControlPriceBox(r.cd, 'BTC');
            this.btcMarkets[r.cd]['trade_price'] = comma(r.tp.toFixed(8));
            this.btcMarkets[r.cd]['trp'] = r.tp;
            this.btcMarkets[r.cd]['signed_change_rate'] = Number(
              (r.scr * 100)?.toFixed(2)
            );
            this.btcMarkets[r.cd]['signed_change_price'] = comma(
              r.scp?.toFixed(8)
            );
            this.btcMarkets[r.cd]['atp24'] = r.atp24h;
            this.btcMarkets[r.cd]['atpc24'] = comma(r.atp24h.toFixed(3));
          };
        } else if (this.selectCurrency === 'USDT') {
          tickerSocket.onmessage = async (payload: any) => {
            const r = await new Response(payload.data).json();
            if (this.selectCoin === r.cd) {
              this.selectMarketInfo = r;
              document.title = comma(r.tp) + ' ' + this.selectCoin;
            }
            this.usdtMarkets[r.cd]['change'] = r.c.toLowerCase();
            this.usdtMarkets[r.cd]['pbc'] = r.ab ? r.ab : '';
            this.setControlPriceBox(r.cd, 'USDT');
            this.usdtMarkets[r.cd]['trade_price'] = comma(r.tp.toFixed(3));
            this.usdtMarkets[r.cd]['trp'] = r.tp;
            this.usdtMarkets[r.cd]['signed_change_rate'] = Number(
              (r.scr * 100)?.toFixed(2)
            );
            this.usdtMarkets[r.cd]['signed_change_price'] = comma(
              r.scp?.toFixed(8)
            );
            this.usdtMarkets[r.cd]['atp24'] = r.atp24h;
            this.usdtMarkets[r.cd]['atpc24'] = comma(r.atp24h.toFixed(3));
          };
        }
      } catch (e) {
        console.error(e);
      }
    },

    async getTradeAPI(codes: string) {
      try {
        const r = await axios.get(
          `https://api.upbit.com/v1/trades/ticks?market=${codes}&count=20`
        );
        const d: IGetTradeDefaultTicks = r.data;
        for (const i in d) {
          const o = {
            ab: '',
            c: '',
            cd: '',
            cp: 0,
            pcp: 0,
            sid: 0,
            st: '',
            td: '',
            tms: 0,
            tp: 0,
            tcp: '',
            ttm: '',
            ttms: 0,
            tv: 0,
            ty: '',
            trp: '',
          };
          const j = d[i];
          const t = getKoreaDateTime(j.trade_date_utc, j.trade_time_utc);
          o.ab = j.ask_bid;
          o.trp = comma((j.trade_price * j.trade_volume).toFixed(0));
          o.td = t[0].split('-')[1] + '.' + t[0].split('-')[2];
          o.ttm = t[1];
          o.tv = j.trade_volume;
          o.tp = j.trade_price;
          o.tcp = comma(j.trade_price);
          this.tradeList.push(o);
        }
      } catch (e) {
        console.error(e);
      }
    },

    getTradeSocket(codes: Array<string>) {
      this.getTradeAPI(this.selectCoin);
      try {
        this.tradeList = [
          {
            ab: '',
            c: '',
            cd: '',
            cp: 0,
            pcp: 0,
            sid: 0,
            st: '',
            td: '',
            tms: 0,
            tp: 0,
            tcp: '',
            ttm: '',
            ttms: 0,
            tv: 0,
            ty: '',
            trp: '',
          },
        ];
        this.tradeList.shift();
        tradeSocket.onopen = (e: any) => {
          tradeSocket.send(
            `${JSON.stringify([
              { ticket: 'trade' },
              { type: 'trade', codes: codes },
              { format: 'SIMPLE' },
            ])}`
          );
        };

        tradeSocket.onmessage = async (payload: any) => {
          const r = await new Response(payload.data).json();

          const t = getKoreaDateTime(r.td, r.ttm);
          r.td = t[0].split('-')[1] + '.' + t[0].split('-')[2];
          r.ttm = t[1];
          r.trp = comma((r.tp * r.tv).toFixed(0));
          r.tcp = comma(r.tp);
          if (this.tradeList.length > 200) {
            this.tradeList.pop();
          }
          this.tradeList.unshift(r);
        };
      } catch (e) {
        console.error(e);
      }
    },

    getOrderbookSocket(codes: Array<string>) {
      try {
        orderbookSocket.onopen = (e: any) => {
          orderbookSocket.send(
            `${JSON.stringify([
              { ticket: 'orderbook' },
              { type: 'orderbook', codes: codes },
              { format: 'SIMPLE' },
            ])}`
          );
        };

        orderbookSocket.onmessage = async (payload: any) => {
          const r = await new Response(payload.data).json();

          const arr: ISocketState['orderbookList'] = {
            ask: {
              ap: [],
              as: [],
            },
            bid: {
              bp: [],
              bs: [],
            },
            tas: '',
            tbs: '',
          };
          for (const i in r.obu) {
            const c = r.obu[i];
            arr.ask.ap.unshift({
              v: c.ap,
              c:
                this.selectMarketInfo.pcp > c.ap
                  ? 'ASK'
                  : this.selectMarketInfo.pcp < c.ap
                  ? 'BID'
                  : '',
              p:
                (
                  (c.ap - this.selectMarketInfo.pcp) /
                  (this.selectMarketInfo.pcp / 100)
                ).toFixed(2) + '%',
            });
            arr.ask.as.push(c.as.toFixed(3));
            arr.bid.bp.push({
              v: c.bp,
              c:
                this.selectMarketInfo.pcp > c.bp
                  ? 'ASK'
                  : this.selectMarketInfo.pcp < c.bp
                  ? 'BID'
                  : '',
              p:
                (
                  (c.bp - this.selectMarketInfo.pcp) /
                  (this.selectMarketInfo.pcp / 100)
                ).toFixed(2) + '%',
            });
            arr.bid.bs.unshift(c.bs.toFixed(3));
          }
          arr.tbs = r.tbs.toFixed(8);
          arr.tas = r.tas.toFixed(8);

          this.orderbookList = arr;
        };

        setTimeout(() => {
          this.krwMarkets = objSort(
            this.krwMarkets,
            this.sortOptions.sortTarget,
            this.sortOptions.sort
          );
          this.btcMarkets = objSort(
            this.btcMarkets,
            this.sortOptions.sortTarget,
            this.sortOptions.sort
          );
        }, 200);
      } catch (e) {
        console.error(e);
      }
    },

    closeTickerSocket() {
      tickerSocket.close();
    },

    closeTradeSocket() {
      tradeSocket.close();
    },

    closeOrderbookSocket() {
      orderbookSocket.close();
    },
  },
});
