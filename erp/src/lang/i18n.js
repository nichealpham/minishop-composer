import Vue from "vue";
import VueI18n from "vue-i18n";

import en from "@/lang/en";
import vn from "@/lang/vn";

Vue.use(VueI18n);

const messages = {
  en: en,
  vn: vn,
};

export function getBrowserLanguage() {
  var name = navigator.language.toLowerCase();
  if (name.includes("en") || name.includes("us")) return "en";
  return "vn";
}

var DefaultLanguageLocale = getBrowserLanguage();
const i18n = new VueI18n({
  locale:
    localStorage.getItem(process.env.VUE_APP_DEFAULT_LANGUAGE) ||
    DefaultLanguageLocale,
  fallbackLocale: "vn", // set fallback locale
  messages, // set locale messages
});

export default i18n;
