<template>
  <v-menu transition="slide-y-transition" rounded offset-x min-width="150">
    <template v-slot:activator="{ attrs, on }">
      <div v-bind="attrs" v-on="on">
        <avartar />
      </div>
    </template>
    <v-list nav dense>
      <v-list-item-group>
        <v-list-item link style="max-width: 260px" @click="openProfile">
          <v-list-item-icon>
            <v-avatar size="24">
              <img :src="user.avatar || '/icons/user.png'" alt="User" />
            </v-avatar>
          </v-list-item-icon>
          <v-list-item-title style="font-size: 1.05rem; line-height: 32px">
            {{ user.fullName }}
          </v-list-item-title>
          <v-list-item-icon>
            <v-icon>mdi-square-edit-outline</v-icon>
          </v-list-item-icon>
        </v-list-item>

        <v-list-item
          v-for="role in roles"
          :key="role.clinicID"
          @click="setRole(role.clinicID)"
          active-class="white_background"
        >
          <v-list-item-icon>
            <v-icon
              :color="currentRole.clinicID == role.clinicID ? '#6166f5' : ''"
              >mdi-shield-home-outline</v-icon
            >
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title
              :style="{
                color: currentRole.clinicID == role.clinicID ? '#6166f5' : '',
              }"
              >{{ role.clinicName }}</v-list-item-title
            >
          </v-list-item-content>
        </v-list-item>

        <v-list-item v-if="isWebVersion" @click="showScanner">
          <v-list-item-icon>
            <v-icon> mdi-qrcode-scan </v-icon>
          </v-list-item-icon>
          <v-list-item-title> Scan QR code </v-list-item-title>
        </v-list-item>

        <v-list-item
          active-class="white_background"
          v-if="isStaff && isMobileVersion"
          @click="$router.push('setting')"
        >
          <v-list-item-icon>
            <v-icon>mdi-hammer-wrench</v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title class="text-capitalize">{{
              $t("user.setting")
            }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item @click="openChangePassword">
          <v-list-item-icon>
            <v-icon> mdi-lock-reset </v-icon>
          </v-list-item-icon>
          <v-list-item-title>
            {{ $t("common.change") }} {{ $t("user.password") }}
          </v-list-item-title>
        </v-list-item>

        <v-list-item @click="logout()" active-class="white_background">
          <v-list-item-icon>
            <v-icon>mdi-logout</v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title class="text-capitalize">{{
              $t("user.logout")
            }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list-item-group>
    </v-list>
  </v-menu>
</template>

<script>
import ListRouteView from "./routesView";
import { OAuthSignOut } from "@/plugins/firebase";
import { sleep } from "@/plugins/helpers";
import avartar from "@/components/avartar.vue";

export default {
  components: { avartar },
  computed: {
    roles() {
      return this.$store.getters["Authen/getUser"].roles || [];
    },
    currentRole() {
      return this.$store.getters["Authen/getRole"];
    },
    isStaff() {
      return this.$store.getters["Authen/isClinicStaff"];
    },
    user() {
      return this.$store.getters["Authen/getUser"];
    },
  },
  data: function () {
    return {
      routes: ListRouteView,
    };
  },
  methods: {
    showScanner() {
      this.$store.commit("Asset/OPEN_SCANNER");
    },
    setRole(clinicID) {
      this.$store.commit("Authen/SET_ROLE", clinicID);
    },
    openProfile() {
      if (this.isWebVersion) {
        if (this.$route.name.toLowerCase() != "home") {
          return this.$router.push("/home?profile=true");
        }
      }
      this.$bus.emit("openProfileDetail");
    },
    openChangePassword() {
      this.$emit("openChangePassword");
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
  },
};
</script>

<style lang="scss" scoped>
.white_background.v-list-item--link:before {
  background-color: white;
}
</style>
