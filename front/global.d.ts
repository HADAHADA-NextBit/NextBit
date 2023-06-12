export {};

declare global {
  type SendParams = {
    url: string;
    method: 'GET' | 'POST' | 'PUT' | 'DELETE';
    data?: object;
    params?: object;
    isForm?: boolean;
    isRefreshToken?: boolean;
  };

  interface ICommonState {
    darkMode: boolean;
    exchangeNavShow: boolean;
    ajaxBar: boolean;
  }

  interface ILocaleList {
    value: string | number;
    label: string;
    flag: string;
    code: string;
  }

  interface ISocketState {
    chartTime: string;
    sortOptions: {
      sortTarget: string;
      sort: string;
    };
    selectCoinFullName: string;
    selectCurrency: string;
    selectCoin: string;
    selectMarketInfo: {
      ty: string;
      cd: string;
      op: string;
      hp: number;
      lp: number;
      tp: number;
      pcp: number;
      c: string;
      cp: string;
      scp: number;
      cr: string;
      scr: string;
      tv: string;
      atv: string;
      atv24h: number;
      atp: string;
      atp24h: number;
      tdt: string;
      ttm: string;
      ttms: string;
      ab: string;
      aav: string;
      abv: string;
      h52wp: number;
      h52wdt: string;
      l52wp: number;
      l52wdt: string;
      ts: string;
      ms: string;
      msfi: string;
      its: string;
      dd: string;
      mw: string;
      tms: string;
      st: string;
    };
    krwMarkets: {
      [key: string]: {
        market: string;
        english_name: string;
        korean_name: string;
        market_warning: string;
        trade_price: string;
        trp: string;
        signed_change_rate: number;
        signed_change_price: string;
        change: string;
        atpc24: string;
        atp24: number;
        ['pbc']: string;
        beforePrice: string;
      };
    };
    btcMarkets: {
      [key: string]: {
        market: string;
        english_name: string;
        korean_name: string;
        market_warning: string;
        trade_price: string;
        trp: string;
        signed_change_rate: number;
        signed_change_price: string;
        change: string;
        atpc24: string;
        atp24: number;
        ['pbc']: string;
        beforePrice: string;
      };
    };
    usdtMarkets: {
      [key: string]: {
        market: string;
        english_name: string;
        korean_name: string;
        market_warning: string;
        trade_price: string;
        trp: string;
        signed_change_rate: number;
        signed_change_price: string;
        change: string;
        atpc24: string;
        atp24: number;
        ['pbc']: string;
        beforePrice: string;
      };
    };
    candleDate: Array<string>;
    candleData: Array<Array<number>>;
    candleVolume: Array<{ value: number; itemStyle: { color: string } }>;
    nowCurrency: Record<string, unknown>;
    // eslint-disable-next-line @typescript-eslint/ban-types
    setCandle: Function | any;
    tradeList: Array<{
      ab: string;
      c: string;
      cd: string;
      cp: number;
      pcp: number;
      sid: number;
      st: string;
      td: string;
      tms: number;
      tp: number;
      tcp: string;
      ttm: string;
      ttms: number;
      tv: number;
      ty: string;
      trp: string;
    }>;
    orderbookList: {
      ask: {
        ap: Array<{
          v?: number;
          c?: string;
          p?: string;
        }>;
        as: Array<number>;
      };
      bid: {
        bp: Array<{
          v?: number;
          c?: string;
          p?: string;
        }>;
        bs: Array<number>;
      };
      tas: string;
      tbs: string;
    };
  }

  interface IGetMarkets {
    english_name: string;
    korean_name: string;
    market: string;
    market_warning: string;
    trade_price: string;
    trp: string;
    signed_change_rate: number;
    signed_change_price: string;
    change: string;
    atpc24: string;
    atp24: number;
    ['pbc']: string;
    beforePrice: string;
  }

  interface IGetTradeDefaultTicks {
    [key: string]: {
      market: string;
      trade_date_utc: string;
      trade_time_utc: string;
      timestamp: number;
      trade_price: number;
      trade_volume: number;
      prev_closing_price: number;
      chane_price: number;
      ask_bid: string;
    };
  }

  interface IGetTickerAPI {
    market: string;
    trade_date: string;
    trade_time: string;
    trade_date_kst: string;
    trade_time_kst: string;
    trade_timestamp: number;
    opening_price: number;
    high_price: number;
    low_price: number;
    trade_price: number;
    prev_closing_price: number;
    change: string;
    change_price: number;
    change_rate: number;
    signed_change_price: number;
    signed_change_rate: number;
    trade_volume: string;
    acc_trade_price: number;
    acc_trade_price_24h: number;
    acc_trade_volume: number;
    acc_trade_volume_24h: number;
    highest_52_week_price: number;
    highest_52_week_date: string;
    lowest_52_week_price: number;
    lowest_52_week_date: string;
    timestamp: number;
  }
}
