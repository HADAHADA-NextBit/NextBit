export const identityExp = /^[a-zA-Z0-9]{4,16}$/;
export const numberRegex = /[^0-9.]/g;
export const passwordExp =
  /^(?=.*[a-zA-Z0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,15}$/;

export const colors = {
  up: '#D24F45',
  down: '#1261C4',
  same: '#000',
};

export const parseJwt = (credential: any) => {
  const base64Url = credential.split('.')[1];
  const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  const jsonPayload = decodeURIComponent(
    atob(base64)
      .split('')
      .map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
      })
      .join('')
  );

  return JSON.parse(jsonPayload);
};

export const objSort = (data: any, target: any, sort?: string) => {
  const changeObj: any = {};
  if (sort === 'desc') {
    new Map(
      Object.entries(data).sort((a: any, b: any): any => {
        return a[1][target] < b[1][target]
          ? 1
          : a[1][target] > b[1][target]
          ? -1
          : 0;
      })
    ).forEach((v: any, k: any) => {
      changeObj[k] = v;
    });
  } else {
    new Map(
      Object.entries(data).sort((a: any, b: any): any => {
        return a[1][target] > b[1][target]
          ? 1
          : a[1][target] < b[1][target]
          ? -1
          : 0;
      })
    ).forEach((v: any, k: any) => {
      changeObj[k] = v;
    });
  }
  return changeObj;
};

export const convertKeys = (
  obj: ITradeResponse | ITradeResponse[],
  type: { [key: string]: any }
): any => {
  if (Array.isArray(obj)) {
    return obj.map((item) => convertKeys(item, type));
  }

  const convertedObj: any = {};
  for (const key in obj) {
    if (obj.hasOwnProperty(key)) {
      convertedObj[type[key]] = obj[key];
    }
  }
  return convertedObj;
};

export const convertTradeType: Record<string, string> = {
  change_price: 'cp',
  sequential_id: 'sid',
  prev_closing_price: 'pcp',
  timestamp: 'tms',
  trade_price: 'tp',
  trade_volume: 'tv',
  ask_bid: 'ab',
  market: 'market',
  trade_date_utc: 'td',
  trade_time_utc: 'ttm',
};

export const convertTickerType: Record<string, string | number> = {
  market: 'market',
  trade_date: 'tdt',
  trade_time: 'ttm',
  trade_timestamp: 'ttms',
  opening_price: 'op',
  high_price: 'hp',
  low_price: 'lp',
  trade_price: 'tp',
  prev_closing_price: 'pcp',
  change: 'c',
  change_price: 'cp',
  change_rate: 'cr',
  signed_change_price: 'scp',
  signed_change_rate: 'src',
  trade_volume: 'tv',
  acc_trade_price: 'atp',
  acc_trade_price_24h: 'atp24h',
  acc_trade_volume: 'atv',
  acc_trade_volume_24h: 'atv24h',
  highest_52_week_price: 'h52wp',
  highest_52_week_date: 'h52wd',
  lowest_52_week_price: 'l52wp',
  lowest_52_week_date: 'l52wd',
  timestamp: 'tms',
};

export const comma = (change: number | string, digit = 8): string => {
  return removeOthers(change).toLocaleString(undefined, {
    maximumFractionDigits: digit,
  });
};

export const removeOthers = (num: any) => {
  num = String(num);
  if (num.split('.').length > 2) {
    num = num.split('.')[0].concat('.').concat(num.split('.')[1]);
  }
  num = Number(num.replaceAll(numberRegex, ''));
  return num;
};
