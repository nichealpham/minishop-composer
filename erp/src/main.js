import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import vuetify from "./plugins/vuetify";
import store from "./stores";
import Toast from "vue-toastification";
import i18n from "./lang/i18n";
import "./registerServiceWorker";

import EventBus from "@/plugins/eventBus";
Vue.use(EventBus);

import VueMask from "v-mask";
Vue.use(VueMask);

import "vue-toastification/dist/index.css";
const toastOptions = {
  transition: "Vue-Toastification__fade",
  maxToasts: 1,
  newestOnTop: true,
  hideProgressBar: true,
  position: "top-center",
  timeout: 3000,
};
Vue.use(Toast, toastOptions);

import { FirebaseMessaging, database } from "@/plugins/firebase";
import { HttpClient } from "./plugins/httpClient";
import { sleep } from "@/plugins/helpers";
import { Capacitor } from "@capacitor/core";
import { RoleType } from "./plugins/contants";
import moment from "moment";

Vue.mixin({
  computed: {
    isNative() {
      return Capacitor.isNativePlatform();
    },
    isAuthenticated() {
      return this.$store.getters["Authen/getToken"] || "";
    },
    $clinicID() {
      return this.$store.getters["Authen/getRole"].clinicID || "";
    },
    $isPatient() {
      return this.$store.getters["Authen/getRole"].roleType == RoleType.Patient;
    },
    isLargeWebVersion() {
      return this.$vuetify.breakpoint.width >= 1280;
    },
    isWebVersion() {
      return this.$vuetify.breakpoint.width >= 820;
    },
    isMobileVersion() {
      return this.$vuetify.breakpoint.width < 820;
    },
    $httpClient() {
      return new HttpClient(process.env.VUE_APP_API_URL);
    },
    $fcm() {
      return FirebaseMessaging;
    },
    $minishopUrl() {
      var clinicName = this.$store.getters["Authen/getRole"].clinicName;
      if (!clinicName) return "";
      clinicName = clinicName.split(" ").join("").split("-").join("");
      return `${process.env.VUE_APP_MINISHOP_URL}?cname=${clinicName}`;
    },
  },
  mounted() {
    // if back button is pressed
    window.onpopstate = () => {
      if (this.$store.getters["Mobile/diaglogState"]) {
        this.$router.push(this.$route.name);
        setTimeout(() => {
          this.closeDiaglogIfMobile();
        }, 50);
      }
    };
  },
  methods: {
    sleep,
    formatPhoneNumber(phoneStr) {
      if (!phoneStr) return "";
      return phoneStr.split("-").join("").split(" ").join("");
      // return `84${phoneStr.substr(phoneStr.length - 9)}`;
    },
    getPhoneLastDigits(phoneStr) {
      if (!phoneStr) return "";
      phoneStr = phoneStr.split("-").join("").split(" ").join("");
      return phoneStr.substr(phoneStr.length - 9);
    },
    formatDob(str) {
      return moment(str).format("DD-MM-YYYY");
    },
    async $dbGet(ref) {
      return new Promise((resolve) => {
        database
          .ref()
          .child(ref)
          .get()
          .then((snapshot) => {
            if (snapshot.exists()) {
              return resolve(snapshot.val());
            } else {
              console.log("No data available");
              return resolve(false);
            }
          })
          .catch((error) => {
            console.error(error);
            return resolve(false);
          });
      });
    },
    async checkAuthorizedDomain(url) {
      url = new URL(url);
      var domainName = url.hostname.split(".").join("@").trim();
      if (await this.$dbGet(`authorizedDomains/${domainName}`)) return true;
      return false;
    },
    callPhone(phone) {
      if (!phone) return;
      if (this.isMobileVersion) window.open(`tel:${phone}`);
      else window.open(`https://zalo.me/${phone}`);
    },
    openDiaglogIfMobile(title) {
      if (this.isMobileVersion) {
        this.$store.commit("Mobile/SET_DIAGLOG_STATE", true);
        this.$store.commit("Mobile/SET_DIAGLOG_TITLE", title);
      }
    },
    closeDiaglogIfMobile() {
      if (this.isMobileVersion) {
        this.$store.commit("Mobile/SET_DIAGLOG_STATE", false);
        this.$store.commit("Mobile/SET_DIAGLOG_TITLE", "");
      }
    },
    openBrowserNewTab(url) {
      window.open(url, "_blank");
    },
    showError: function (message) {
      if (["Error: Request failed with status code 404"].includes(message)) {
        console.error(message);
        return;
      }
      this.$toast.error(message);
    },
    showErrorPopup(message) {
      this.$toast.error(message);
    },
    showSuccess: function (message) {
      this.$toast.success(message);
    },
    showInfo: function (message) {
      this.$toast.info(message);
    },
    showWarning: function (message) {
      this.$toast.warning(message);
    },
    copyToClipboard(copyText = "") {
      navigator.clipboard.writeText(copyText);
    },
  },
});

Vue.config.productionTip = false;

export const VueInstance = new Vue({
  i18n,
  router,
  vuetify,
  store,
  render: (h) => h(App),
}).$mount("#app");
