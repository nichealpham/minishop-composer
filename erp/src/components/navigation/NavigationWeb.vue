<template>
  <div class="web_navigation_container">
    <div class="content" style="position: absolute">
      <div class="d-flex flex-column justify-space-between h-100 align-center">
        <menu-change-profile @openChangePassword="openChangePassword" />
        <popup-change-password ref="PopupChangePassword" />
        <div
          class="content__routing"
          :style="{
            'margin-bottom': `calc(50vh - 30px - ${routes.length * 50}px)`,
          }"
        >
          <template v-for="(itemRoute, index) in routes">
            <router-link
              :to="itemRoute.to"
              :tag="itemRoute.tag"
              :key="index"
              active-class="active"
              style="display: block; padding: 12px 7px; margin-bottom: 25px"
            >
              <v-icon color="white" class="mb-1">{{ itemRoute.icon }}</v-icon>
              <v-badge
                color="pink"
                dot
                offset-y="-15"
                offset-x="-5"
                v-if="count && itemRoute.title == 'Home'"
              ></v-badge>
              <p
                style="color: white; font-size: 0.9rem"
                class="pa-0 ma-0 text-capitalize"
              >
                {{ $t(`nav.${itemRoute.title.toLowerCase()}`) }}
              </p>
            </router-link>
          </template>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ListRouteView from "./routesView";
import MenuChangeProfile from "./menuChangeProfile.vue";
import { OAuthSignOut } from "@/plugins/firebase";
import { sleep } from "@/plugins/helpers";
import PopupChangePassword from "@/components/users/PopupChangePassword.vue";

export default {
  components: { MenuChangeProfile, PopupChangePassword },
  computed: {
    fcmCount() {
      return this.$store.getters["Fcm/count"];
    },
    routes() {
      var isStaff = this.$store.getters["Authen/isClinicStaff"];
      if (isStaff) {
        return ListRouteView.filter((i) => i.roles.includes("staff")).filter(
          (a) => a.title != "Search"
        );
      }
      return ListRouteView.filter((i) => i.roles.includes("patient")).filter(
        (a) => a.title != "Search"
      );
    },
  },
  watch: {
    fcmCount(val, old) {
      if (val && val != old) this.renderCount();
    },
  },
  data() {
    return {
      count: 0,
    };
  },
  mounted() {
    this.renderCount();
  },
  methods: {
    openChangePassword() {
      this.$refs.PopupChangePassword.open();
    },
    async logout() {
      window.parent.postMessage(JSON.stringify({ channel: "Logout" }), "*");
      var fcmToken = this.$store.getters["Authen/getFcm"];
      this.$httpClient.post("user/notifications/unsubscribe", null, null, {
        fcmToken,
      });
      await sleep(50);
      OAuthSignOut();
      this.$store.commit("Authen/LOGOUT");
      this.$store.commit("Signup/CLEAR_REGISTER");
      this.$router.push("/login");
    },
    async renderCount() {
      var count = await this.$httpClient.get(`/user/activities/countnotread`);
      this.count = count || 0;
    },
  },
};
</script>
