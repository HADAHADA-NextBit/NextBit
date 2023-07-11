export {};

declare global {
  interface Window {
    naver: any;
    google: any;
  }

  type SendParams = {
    url: string;
    method: 'GET' | 'POST' | 'PUT' | 'DELETE';
    data?: object;
    params?: object;
    isForm?: boolean;
    isRefreshToken?: boolean;
  };

  interface ICommonState {
    exchangeNavShow: boolean;
    ajaxBar: boolean;
    language: string;
  }

  interface ILocaleList {
    value: string | number;
    label: string;
    flag: string;
    code: string;
  }

  interface ISocketState {
    chartTime: string;
    coinFullName: { [key: string]: string; ko: string; en: string };
    selectCoin: string;
    tradeData: ISocketTradeResponse;
    tickerData: ITickerResponse;
    tradeList: ISocketTradeResponse[];
    markets: IMarketResponse;
    reloadCandle: Function;
    reloadTrade: Function;
    reloadOrderbook: Function;
  }

  interface ITickerResponse {
    dd: null;
    its: false;
    aav: number;
    abv: number;
    atp: number;
    atp24h: number;
    atv: number;
    atv24h: number;
    cp: number;
    cr: number;
    h52wp: number;
    hp: number;
    l52wp: number;
    lp: number;
    op: number;
    pcp: number;
    scp: number;
    scr: number;
    tms: number;
    tp: number;
    ttms: number;
    tv: number;
    ab: string;
    c: string;
    cd: string;
    h52wdt: string;
    l52wdt: string;
    ms: string;
    mw: string;
    st: string;
    tdt: string;
    ttm: string;
    ty: string;
    korean_name: string;
  }

  interface IMarketResponse {
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
    pbc: string;
    beforePrice: string;
  }

  type BoxControl = { box?: boolean; control: Function };

  interface IMarkets {
    [key: string]: {
      [key: string]: IMarketResponse & ITickerResponse & BoxControl;
    };
  }

  interface ICandleStickResponse {
    market: string;
    candle_date_time_utc: string;
    candle_date_time_kst: string;
    opening_price: number;
    high_price: number;
    low_price: number;
    trade_price: number;
    timestamp: number;
    candle_acc_trade_price: number;
    candle_acc_trade_volume: number;
    unit: number;
  }

  interface ITradeResponse {
    [key: string]: any;
    change_price: number;
    sequential_id: number;
    prev_closing_price: number;
    timestamp: number;
    trade_price: number;
    trade_volume: number;
    ask_bid: string;
    market: string;
    trade_date_utc: string;
    trade_time_utc: string;
  }

  interface ISocketTradeResponse {
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
    ttm: string;
    ttms: number;
    tv: number;
    ty: string;
  }

  interface ISocketOrderbookResponse {
    cd: string;
    tms: number;
    tas: number;
    tbs: number;
    obu: [
      {
        ap: number;
        bp: number;
        as: number;
        bs: number;
      }
    ];
    st: string;
    ty: string;
  }
}
