/* eslint-disable @typescript-eslint/no-explicit-any */
export const comma = (e: number | string | undefined) => {
  const num = String(e).split('.');
  num[0] = num[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
  return num.join('.');
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

export const objSort2 = (data: any, target: any, sort?: string) => {
  const entries = Object.entries(data);
  const compareFn = (a: any, b: any) =>
    a[1][target] > b[1][target] ? 1 : a[1][target] < b[1][target] ? -1 : 0;

  const sortedEntries =
    sort === 'desc'
      ? entries.sort(compareFn).reverse()
      : entries.sort(compareFn);

  const changeObj: any = {};
  sortedEntries.forEach(([key, value]) => {
    changeObj[key] = value;
  });
  return changeObj;
};

const koreaTimeDiffer = 9 * 60 * 60 * 1000 * 2;
export const getKoreaDateTime = (date: string, time: string) => {
  return new Date(new Date(date + 'T' + time).getTime() + koreaTimeDiffer)
    .toISOString()
    .split('.')[0]
    .split('T');
};
