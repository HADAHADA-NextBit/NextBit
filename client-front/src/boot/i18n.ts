import { boot } from 'quasar/wrappers';
import { createI18n } from 'vue-i18n';
import { LocalStorage, Quasar } from 'quasar';
import messages from 'src/i18n';
import { localeList } from 'src/utils/constants';

const locale = Quasar.lang.getLocale();

if (LocalStorage.getItem<ILocaleList>('lang')) {
  document.documentElement.lang = LocalStorage.getItem<ILocaleList>('lang')
    ?.value as string;
} else {
  LocalStorage.set(
    'lang',
    localeList.find((e) => e.value === locale)
  );
}

export type MessageLanguages = keyof typeof messages;
export type MessageSchema = typeof messages['en-US'];

/* eslint-disable @typescript-eslint/no-empty-interface */
declare module 'vue-i18n' {
  export interface DefineLocaleMessage extends MessageSchema {}

  export interface DefineDateTimeFormat {}

  export interface DefineNumberFormat {}
}
/* eslint-enable @typescript-eslint/no-empty-interface */

export const i18n = createI18n({
  locale: LocalStorage.getItem<ILocaleList>('lang')
    ? (LocalStorage.getItem<ILocaleList>('lang')?.value as string | undefined)
    : locale,
  legacy: false,
  messages,
  globalInjection: true,
});

export default boot(({ app }) => {
  app.use(i18n);
});
