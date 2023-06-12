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
// Type-define 'en-US' as the master schema for the resource
export type MessageSchema = typeof messages['en-US'];

// See https://vue-i18n.intlify.dev/guide/advanced/typescript.html#global-resource-schema-type-definition
/* eslint-disable @typescript-eslint/no-empty-interface */
declare module 'vue-i18n' {
  // define the locale messages schema
  export interface DefineLocaleMessage extends MessageSchema {}

  // define the datetime format schema
  export interface DefineDateTimeFormat {}

  // define the number format schema
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
