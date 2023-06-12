import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useCommonStore = defineStore({
  id: 'commonStore',
  state: (): ICommonState => ({
    darkMode: false,
    exchangeNavShow: false,
    ajaxBar: false,
  }),
  actions: {
    setDarkMode(darkMode: boolean) {
      this.darkMode = darkMode;
    },
    setExchangeNavShow() {
      this.exchangeNavShow = !this.exchangeNavShow;
    },
    setAjaxBar(active: boolean) {
      this.ajaxBar = active;
    },
  },
});
