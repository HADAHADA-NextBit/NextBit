import { boot } from 'quasar/wrappers';
import { useCommonStore } from 'src/stores/common-store';
import { useChartStore } from 'src/stores/chart-store';
import { identityExp, passwordExp, colors } from 'src/utils/rule';

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $identityExp: RegExp;
    $passwordExp: RegExp;
    $colors: { up: string; down: string; same: string };
  }
}

const commonStore = useCommonStore();
const chartStore = useChartStore();

export default boot(({ app }) => {
  app.config.globalProperties.$commonStore = commonStore;
  app.config.globalProperties.$chartStore = chartStore;
  app.config.globalProperties.$identityExp = identityExp;
  app.config.globalProperties.$passwordExp = passwordExp;
  app.config.globalProperties.$colors = colors;
});
