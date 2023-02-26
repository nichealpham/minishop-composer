<template>
  <div class="backdrop_blur">
    <div class="app_updater_container">
      <v-container fill-height>
        <v-row justify="center" align="center">
          <v-col cols="12" justify="center" align="center">
            <p class="text-center mt-5">Logging out...</p>
            <v-progress-circular
              color="primary"
              indeterminate
            ></v-progress-circular>
          </v-col>
        </v-row>
      </v-container>
    </div>
  </div>
</template>

<script>
import { OAuthSignOut } from "@/plugins/firebase";

export default {
  created() {
    this.logout();
  },
  methods: {
    async logout() {
      window.parent.postMessage(JSON.stringify({ channel: "Logout" }), "*");
      var fcmToken = this.$store.getters["Authen/getFcm"];
      this.$httpClient.post("user/notifications/unsubscribe", null, null, {
        fcmToken,
      });
      await this.sleep(50);
      OAuthSignOut();
      this.$store.commit("Authen/LOGOUT");
      this.$store.commit("Signup/CLEAR_REGISTER");
      var redirect = this.$route.query.redirect;
      if (redirect && (await this.checkAuthorizedDomain(redirect))) {
        return (window.location.href = `${redirect}`);
      }
      this.$router.push("/login");
    },
  },
};
</script>
