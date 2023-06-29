import { RouteRecordRaw } from 'vue-router';

declare module 'vue-router' {
  interface RouteMeta {
    title: string;
    icon?: any;
  }
}

export const pages = [
  {
    path: '/',
    name: 'home',
    icon: 'home',
    component: () => import('src/pages/Home.vue'),
  },
  {
    path: '/exchange',
    name: 'exchange',
    icon: 'insert_chart',
    component: () => import('src/pages/ExchangePage.vue'),
  },
  {
    path: '/wallet',
    name: 'wallet',
    icon: 'account_balance_wallet',
    component: () => import('src/pages/WalletPage.vue'),
  },
  {
    path: '/news',
    name: 'news',
    icon: 'feed',
    component: () => import('src/pages/NewsPage.vue'),
  },
  {
    path: '/notice',
    name: 'notice',
    icon: 'campaign',
    component: () => import('src/pages/NoticePage.vue'),
  },
  {
    path: '/customer',
    name: 'customer',
    icon: 'support_agent',
    component: () => import('src/pages/CustomerPage.vue'),
  },
];

const routes: RouteRecordRaw[] = [
  ...pages,
  {
    path: '/:catchAll(.*)*',
    redirect: () => {
      return '/';
    },
  },
];

export default routes;
