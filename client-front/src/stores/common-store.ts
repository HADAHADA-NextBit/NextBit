import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useCommonStore = defineStore({
  id: 'commonStore',
  state: (): ICommonState => ({
    exchangeNavShow: false,
    ajaxBar: false,
    language: 'ko',
  }),
  actions: {
    setExchangeNavShow() {
      this.exchangeNavShow = !this.exchangeNavShow;
    },
    setAjaxBar(active: boolean) {
      this.ajaxBar = active;
    },
    setLanguage(lang: string) {
      this.language = lang;
    },
  },
});
