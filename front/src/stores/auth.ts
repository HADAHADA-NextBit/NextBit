import { defineStore } from 'pinia';
import { SessionStorage } from 'quasar';

export const useAuthStore = defineStore({
  id: 'authStore',
  state: () => ({}),
  actions: {
    logout() {
      SessionStorage.remove('token');
    },
  },
});
