import { defineStore } from 'pinia';
import axios from 'axios';
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
