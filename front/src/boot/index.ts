import { boot } from 'quasar/wrappers';
import { useCommonStore } from 'src/stores/common-store';
import { useChartStore } from 'src/stores/chart-store';

const commonStore = useCommonStore();
const chartStore = useChartStore();

export default boot(({ app }) => {
  app.config.globalProperties.$commonStore = commonStore;
  app.config.globalProperties.$chartStore = chartStore;
});
