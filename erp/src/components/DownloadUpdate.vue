<template>
  <div class="backdrop_blur">
    <div
      class="container_background"
      :style="{
        'background-image': backgroundImage,
      }"
    >
      <!-- <div class="container_background"> -->
      <div class="blur_filter"></div>
    </div>
    <div class="app_updater_container">
      <v-container fill-height>
        <v-row justify="center" align="center">
          <v-col cols="12" justify="center" align="center" v-if="isVerifying">
            <p class="text-center mt-5 white--text">
              {{ $t("common.verifyUpdate") }}
            </p>
            <p style="width:190px" class="mt-3">
              <v-progress-linear
                color="#BBDEFB"
                indeterminate
                rounded
                height="7"
              ></v-progress-linear>
            </p>
          </v-col>

          <v-col cols="12" justify="center" align="center" v-if="isError">
            <p class="text-center white--text">
              {{ $t("common.serverunavailable") }}
            </p>
            <p style="width:190px" class="mt-3">
              <v-btn
                type="submit"
                color="blue-grey"
                block
                class="btn_login mb-5 text-uppercase white--text"
                @click="backtohome"
              >
                {{ $t("common.backtohome") }}
              </v-btn>
            </p>
          </v-col>
        </v-row>
      </v-container>
    </div>
  </div>
</template>

<script>
import { sleep } from "@/plugins/helpers";
import { mapGetters } from "vuex";

export default {
  computed: {
    ...mapGetters({
      backgroundImage: "Theme/getBackground",
    }),
    tokenID() {
      return this.$store.getters["Authen/getToken"] || null;
    },
    clinicID() {
      return this.$store.getters["Authen/getRole"].clinicID || "";
    },
  },
  data() {
    return {
      isVerifying: true,
      isError: false,
      retryCounts: 0,
    };
  },
  created() {
    this.checkForUpdate();
  },
  methods: {
    async checkForUpdate() {
      await sleep(500);
      while (this.retryCounts < 30) {
        var result = await this.ping();
        if (result) {
          // check if user is logged-in
          if (this.tokenID) {
            // force update new token
            await this.refreshToken();
          }
          this.$emit("success", true);
          this.isVerifying = false;
          return;
        }
        this.retryCounts += 1;
        await sleep(2000);
      }
      this.isVerifying = false;
      this.isError = true;
    },
    async ping() {
      var result = await this.$httpClient.get("/user/ping");
      if (result == "pong") {
        return true;
      }
      return false;
    },
    async refreshToken() {
      var { user, token, error } = await this.$httpClient.get(
        `/user/refreshtoken`
      );
      if (error)
        return this.showError("Cannot get refresk token! Please try again.");
      else {
        this.$store.commit("Authen/SET_USER", user);
        this.$store.commit("Authen/SET_TOKEN", token);
      }
    },
    backtohome() {
      window.location.href = "https://sandrasoft.app";
    },
  },
};
</script>

<style lang="scss">
.app_updater_container {
  width: 100vw;
  height: 100vh;
  position: fixed;
  background-color: rgba(0, 0, 0, 0.35);
  z-index: 999999;
  img.logo {
    width: 90px;
    height: 90px;
    border-radius: 50%;
  }
  p {
    font-size: 1.1rem;
  }
}
</style>
