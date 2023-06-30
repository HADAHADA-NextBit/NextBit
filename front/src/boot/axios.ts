import { boot } from 'quasar/wrappers';
import axios, { AxiosInstance } from 'axios';
import { Dialog, LocalStorage, SessionStorage } from 'quasar';
import { useAuthStore } from 'src/stores/auth-store';
import { i18n } from 'src/boot/i18n';

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $axios: AxiosInstance;
  }
}

const client = axios.create({ baseURL: `` });

const send = async <T>(options: SendParams): Promise<T> => {
  const auth = useAuthStore();
  const { url, method, data, params, isForm, isRefreshToken } = options;

  try {
    const accessToken = SessionStorage.getItem('token');

    const response = await client.request({
      url,
      method,
      data,
      params,
      headers: {
        ...(accessToken ? { Authorization: `bearer ${accessToken}` } : {}),
        ...(isForm
          ? {
              'Content-Type': 'multipart/form-data',
            }
          : {}),
        'Accept-Language': LocalStorage.getItem<ILocaleList>('lang')?.code,
      },
    });

    if (response.status !== 200) {
      throw response;
    }

    return response.data as T;
  } catch (error: any) {
    if (error.response?.status === 401 || error.response?.status === 403) {
      Dialog.create({
        title: `<div class="text-center fs-15 pt-10">
        ${i18n.global.t('notify.info.session_logout')}
        </div>`,
        html: true,
        persistent: true,
        ok: i18n.global.t('common.word.confirm'),
        transitionHide: 'no',
      }).onOk(() => {
        auth.logout();
      });
    }
    throw error;
  }
};

const request = {
  get: async <T = any>(url: string, params?: object | undefined) =>
    await send<T>({ url, method: 'GET', data: undefined, params }),
  delete: async <T = any>(url: string, params?: object | undefined) =>
    await send<T>({ url, method: 'DELETE', data: undefined, params }),
  post: async <T = any>(
    url: string,
    data?: object | undefined,
    params?: object | undefined,
    isForm?: boolean,
    isRefreshToken?: boolean
  ) =>
    await send<T>({
      url,
      method: 'POST',
      data,
      params,
      isForm,
      isRefreshToken,
    }),
  put: async <T = any>(
    url: string,
    data?: object | undefined,
    params?: object | undefined,
    isForm?: boolean
  ) => await send<T>({ url, method: 'PUT', data, params, isForm }),
};

const api = {};

export default boot(({ app }) => {
  app.config.globalProperties.$axios = axios;
  app.config.globalProperties.$api = api;
});

export { api };
