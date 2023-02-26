<template>
  <v-toolbar
    tile
    class="mobile_header_drawer"
    flat
    elevation="0"
    v-if="isMobileVersion"
  >
    <menu-change-profile @openChangePassword="openChangePassword" />
    <popup-change-password ref="PopupChangePassword" />
    <!-- <v-toolbar-title
      class="text-capitalize ml-3"
      v-if="$route.name.toLowerCase() == 'search'"
    >
      {{ $t("nav.search") }}
    </v-toolbar-title> -->
    <v-toolbar-title class="text-capitalize ml-3">
      {{ $t(`nav.${$route.name.toLowerCase()}`) }}
    </v-toolbar-title>
    <!-- <v-text-field
      :placeholder="$t('common.search') + '...'"
      background-color="#8380FF"
      append-icon="mdi-magnify"
      class="mobile_header_search ml-3 mr-3"
      full-width
      solo
      hide-details
      dense
      flat
      v-if="$route.name.toLowerCase() != 'search'"
      @click="$router.push('/search')"
    ></v-text-field>
    <v-spacer v-if="$route.name.toLowerCase() == 'search'" /> -->
    <v-spacer />
    <v-avatar
      size="32"
      v-if="getClinicProfile.logo"
      @click="openBrowserNewTab(getClinicProfile.publicNameUrl)"
      class="mr-3"
    >
      <img alt="Avatar" :src="getClinicProfile.logo" />
    </v-avatar>
    <v-btn
      fab
      elevation="0"
      max-height="42px"
      max-width="42px"
      color="#6C68FF"
      @click="showScanner"
      class="mr-3"
      style="background-color: transparent; opacity: 0.6"
    >
      <v-icon color="white"> mdi-qrcode-scan </v-icon>
    </v-btn>

    <v-btn
      fab
      elevation="0"
      max-height="42px"
      max-width="42px"
      color="#6C68FF"
      @click="diaglog = true"
    >
      <v-icon color="white"> mdi-bell </v-icon>
      <v-badge color="pink" dot offset-y="-10" v-if="count"></v-badge>
    </v-btn>
    <div class="w-100" style="overflow-x: hidden; position: fixed">
      <v-dialog
        v-model="diaglog"
        fullscreen
        hide-overlay
        transition="dialog-bottom-transition"
        :persistent="true"
        :no-click-animation="true"
      >
        <v-card style="overflow-x: hidden">
          <v-toolbar dark color="#6166f5">
            <v-toolbar-title>
              {{ $t("home.notification") }}
            </v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-items>
              <v-btn icon dark @click="$refs.Notification.render()">
                <v-icon>mdi-refresh</v-icon>
              </v-btn>
              <v-btn icon dark @click="diaglog = false">
                <v-icon>mdi-close</v-icon>
              </v-btn>
            </v-toolbar-items>
          </v-toolbar>
          <div class="info_layout pa-5">
            <div class="title_area">
              <div class="user__detail mb-10">
                <notification ref="Notification" />
              </div>
            </div>
          </div>
        </v-card>
      </v-dialog>

      <v-dialog
        v-model="diaglog2"
        fullscreen
        hide-overlay
        transition="dialog-bottom-transition"
      >
        <v-card style="overflow-x: hidden">
          <v-toolbar dark color="#6166f5">
            <v-toolbar-title>
              {{ $t("user.profile") }}
            </v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-items>
              <v-btn icon dark @click="diaglog2 = false">
                <v-icon>mdi-close</v-icon>
              </v-btn>
            </v-toolbar-items>
          </v-toolbar>
          <div class="info_layout pa-5">
            <div class="title_area">
              <div class="user__detail mb-10">
                <detail-profile
                  @cancel="diaglog2 = false"
                  @success="diaglog2 = false"
                />
              </div>
            </div>
          </div>
        </v-card>
      </v-dialog>
    </div>
  </v-toolbar>
</template>

<script>
import MenuChangeProfile from "./menuChangeProfile.vue";
import Notification from "../home/Notification.vue";
import DetailProfile from "../users/DetailProfile.vue";
import PopupChangePassword from "@/components/users/PopupChangePassword.vue";
import { mapGetters } from "vuex";

export default {
  components: {
    MenuChangeProfile,
    Notification,
    DetailProfile,
    PopupChangePassword,
  },
  computed: {
    fcmCount() {
      return this.$store.getters["Fcm/count"];
    },
    ...mapGetters("Authen", ["getClinicProfile"]),
  },
  watch: {
    fcmCount(val, old) {
      if (val && val != old) this.renderCount();
    },
  },
  data: function () {
    return {
      count: 0,
      diaglog: false,
      diaglog2: false,
    };
  },
  mounted() {
    this.renderCount();
    this.$bus.on("openNotificationViewBooking", ({ id }) =>
      this.viewBooking(id)
    );
    this.$bus.on("openNotificationViewEpisode", ({ id }) =>
      this.viewEpisode(id)
    );
    this.$bus.on("openProfileDetail", () => (this.diaglog2 = true));
  },
  methods: {
    openChangePassword() {
      this.$refs.PopupChangePassword.open();
    },
    showScanner() {
      this.$store.commit("Asset/OPEN_SCANNER");
    },
    viewBooking(id) {
      if (this.$refs.Notification) {
        this.diaglog = true;
        this.$refs.Notification.openBooking(id);
      }
    },
    viewEpisode(id) {
      if (this.$refs.Notification) {
        this.diaglog = true;
        this.$refs.Notification.openEpisode(id);
      }
    },
    async renderCount() {
      var count = await this.$httpClient.get(`/user/activities/countnotread`);
      this.count = count || 0;
    },
  },
};
</script>

<style lang="scss">
.btn_home_mobile {
  background-color: red;
}
.mobile_header_search {
  * {
    outline: none !important;
  }
  .v-input__slot {
    padding: 0 10px 0 15px !important;
    input {
      font-size: 0.92rem !important;
      padding: 0px !important;
      margin-top: -1px !important;
    }
    i {
      color: rgba(0, 0, 0, 0.54) !important;
      opacity: 0.6; /* Firefox */
    }
  }
  ::placeholder {
    /* Chrome, Firefox, Opera, Safari 10.1+ */
    color: rgba(0, 0, 0, 0.54) !important;
    opacity: 1; /* Firefox */
    font-size: 0.92rem !important;
  }
}
</style>
