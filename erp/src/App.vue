<template>
  <v-app>
    <transition name="fade-transition" v-if="isVerified">
      <router-view />
    </transition>
    <download-update
      v-else
      @success="isVerified = true"
      transition="fade-transition"
    />
    <qrcode-stream v-if="showScanner" @decode="onDecode"></qrcode-stream>
    <!-- <qrcode-drop-zone v-if="showScanner" @decode="onDecode"></qrcode-drop-zone> -->
    <v-btn
      fixed
      @click="closeScanner"
      v-if="showScanner"
      style="left: calc(50vw - 30px); bottom: 20px"
      fab
      dark
    >
      <v-icon>mdi-close</v-icon>
    </v-btn>
    <v-avatar
      tile
      size="62"
      v-if="isVerified && clinicProfile.logo && isLargeWebVersion"
      class="elevation-2 company_logo"
      @click="openBrowserNewTab(clinicProfile.publicNameUrl)"
      style="border-radius: 12px !important"
    >
      <img alt="Avatar" :src="clinicProfile.logo" />
    </v-avatar>
    <mini-apps v-if="isVerified && isAuthenticated && isLargeWebVersion" />
  </v-app>
</template>

<script>
import DownloadUpdate from "@/components/DownloadUpdate.vue";
import { QrcodeStream } from "vue-qrcode-reader";
import MiniApps from "@/components/MiniApps.vue";

export default {
  name: "App",
  components: { DownloadUpdate, QrcodeStream, MiniApps },
  computed: {
    showScanner() {
      return this.$store.getters["Asset/getShowScanner"] || false;
    },
    userToken() {
      return this.$store.getters["Authen/getToken"] || null;
    },
  },
  watch: {
    $clinicID: {
      handler: function (val) {
        if (val) return this.getClinicProfile();
        this.clinicProfile = {};
      },
    },
  },
  data() {
    return {
      isVerified: false,
      clinicProfile: {},
    };
  },
  created() {
    this.getClinicProfile();
  },
  mounted() {
    if (this.userToken) {
      window.parent.postMessage(
        JSON.stringify({ channel: "Authentication", token: this.userToken }),
        "*"
      );
      console.log("Post authentication to parent iframe success!");
    }
  },
  methods: {
    onDecode(decodedString) {
      this.closeScanner();
      var assetID = decodedString.split("?assetid=")[1];
      if (assetID) {
        this.$store.commit("Asset/SET_ASSET_ID", assetID);
      }
      var serviceID = decodedString.split("?serviceid=")[1];
      if (serviceID) {
        this.$store.commit("Asset/SET_SERVICE_ID", serviceID);
      }
      var episodeID = decodedString.split("?episodeid=")[1];
      if (episodeID) {
        this.$store.commit("Public/SET_EPISODE_NAME", episodeID);
      }
    },
    closeScanner() {
      this.$store.commit("Asset/CLOSE_SCANNER");
    },
    async getClinicProfile() {
      if (!this.$clinicID) return;
      var clinic = await this.$httpClient.get(
        `/owner/clinics/${this.$clinicID}`
      );
      if (!clinic || clinic.error) return;
      this.clinicProfile = clinic;
      this.$store.commit("Authen/SET_CLINIC_PROFILE", clinic);
    },
  },
};
</script>

<style lang="scss">
@import "./App.scss";

.company_logo {
  position: fixed;
  top: 10px;
  right: 10px;
}
</style>
