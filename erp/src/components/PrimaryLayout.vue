<template>
  <backdrop-blur>
    <div class="primary_layout">
      <div class="web_navigation_drawer">
        <navigation-web-menu />
      </div>
      <navigation-mobile-menu />
      <transition v-if="isMobileVersion" name="slide-x-transition">
        <router-view></router-view>
      </transition>
      <router-view v-else></router-view>
      <bottom-navigation />
      <popup-asset-detail />
      <popup-public-episode />
      <popup-service-detail />
    </div>
  </backdrop-blur>
</template>

<script>
import NavigationWebMenu from "./navigation/NavigationWeb";
import NavigationMobileMenu from "./navigation/NavigationMobile";
import BackdropBlur from "./BackdropBlur.vue";
import BottomNavigation from "./navigation/bottomNavigation.vue";
import { getDeviceFcmToken } from "@/plugins/firebase";
import { TransformFcmNotificationItem, StatusCode } from "@/plugins/contants";
import PopupAssetDetail from "./asset/PopupAssetDetail.vue";
import PopupPublicEpisode from "./episodes/PopupPublicEpisode.vue";
import PopupServiceDetail from "./services/PopupServiceDetail.vue";

export default {
  name: "PrimaryLayout",
  components: {
    NavigationWebMenu,
    NavigationMobileMenu,
    BackdropBlur,
    BottomNavigation,
    PopupAssetDetail,
    PopupPublicEpisode,
    PopupServiceDetail,
  },
  async mounted() {
    if (!this.$store.getters["Authen/getFcm"]) {
      this.subscribe();
    }
    this.$fcm.onMessage((payload) => {
      var notification = TransformFcmNotificationItem(
        JSON.parse(payload.data.metadata)
      );
      this.$store.commit("Fcm/SET_NEW", notification);
      this.$store.commit("Fcm/SET_COUNT");
      var { statusCode, message } = notification;
      if (statusCode == StatusCode.Danger) {
        this.showError(message);
      } else if (statusCode == StatusCode.Info) {
        this.showInfo(message);
      } else if (statusCode == StatusCode.Warning) {
        this.showWarning(message);
      } else {
        this.showSuccess(message);
      }
    });
    if (this.$route.query.assetid) {
      this.$store.commit("Asset/SET_ASSET_ID", this.$route.query.assetid);
    }
  },
  methods: {
    async subscribe() {
      try {
        var token = await getDeviceFcmToken();
        if (token) {
          var result = await this.$httpClient.post(
            `user/notifications/subscribe`,
            null,
            null,
            { fcmToken: token }
          );
          if (result) {
            this.$store.commit("Authen/SET_FCM", token);
          }
        }
      } catch (err) {
        console.error(err);
      }
    },
  },
};
</script>

<style lang="scss">
@import "./primaryLayout.scss";
</style>
