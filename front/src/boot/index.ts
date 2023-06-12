import { boot } from 'quasar/wrappers';
import { useCommonStore } from 'src/stores/common';
import { useSocketUpbitStore } from 'src/stores/socket-upbit';
import { useBinanceStore } from 'src/stores/binance';

const commonStore = useCommonStore();
const upbitStore = useSocketUpbitStore();
const binanceStore = useBinanceStore();

export default boot(({ app }) => {
  app.config.globalProperties.$commonStore = commonStore;
  app.config.globalProperties.$upbitStore = upbitStore;
  app.config.globalProperties.$binanceStore = binanceStore;
});
