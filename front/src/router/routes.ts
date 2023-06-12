import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'home',
    component: () => import('src/pages/Home.vue'),
  },

  {
    path: '/exchange',
    name: 'Exchange',
    component: () => import('src/pages/Exchange.vue'),
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    redirect: () => {
      return '/';
    },
  },
];

export default routes;
