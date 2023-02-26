<template>
  <v-row class="signup" justify="center" align="center">
    <v-col
      lg="5"
      md="6"
      sm="9"
      xs="12"
      class="main_container"
      style="max-width: 600px"
    >
      <v-container fill-height>
        <v-row>
          <v-col cols="12" justify="center" align="center" v-show="step < 3">
            <img
              class="logo mb-1"
              src="/icon.png"
              style="border-radius: 18px"
            />
            <h1 class="page_title text-center mb-5">{{ $t("auth.signup") }}</h1>
          </v-col>
          <v-col cols="12" justify="center" align="center" class="pl-7 pr-7">
            <send-code v-show="step == 1" @success="step = 2" />
            <verify-phone v-show="step == 2" @success="reload()" />
            <create-user v-show="step == 3" @success="step = 4" />
            <more-info v-show="step == 4" @success="step = 5" />
            <thank-you v-show="step == 5" @continue="step = 6" />
            <create-clinic v-show="step == 6" @success="step = 7" />
            <final-thankyou v-show="step == 7" />
          </v-col>
          <v-col
            cols="12"
            justify="center"
            align="center"
            class="pl-7 pr-7 mt-10"
          >
            <span class="bottom"
              >{{ $t("auth.alreadyhaveaccount") }}
              <u
                ><a
                  style="color: #605bff; font-weight: bold"
                  @click="toSignIn()"
                  >{{ $t("auth.login") }}</a
                ></u
              >.</span
            >
          </v-col>
        </v-row>
      </v-container>
    </v-col>
  </v-row>
</template>

<script>
import { OAuthProviderType } from "@/plugins/firebase";
import SendCode from "./components/sendCode.vue";
import VerifyPhone from "./components/verifyPhone.vue";
import CreateUser from "./components/createUser.vue";
import MoreInfo from "./components/moreInfo.vue";
import ThankYou from "./components/thankYou.vue";
import CreateClinic from "./components/createClinic.vue";
import FinalThankyou from "./components/finalThankyou.vue";
import { mapGetters } from "vuex";

export default {
  components: {
    SendCode,
    VerifyPhone,
    CreateUser,
    MoreInfo,
    ThankYou,
    CreateClinic,
    FinalThankyou,
  },
  computed: {
    ...mapGetters({
      isDoneVerifyPhone: "Signup/isDoneVerifyPhone",
      isDoneCreateUser: "Signup/isDoneCreateUser",
      isDoneAddMoreInfo: "Signup/isDoneAddMoreInfo",
    }),
  },
  data() {
    return {
      provider: OAuthProviderType,
      loading: false,
      step: 1,
    };
  },
  mounted() {
    this.getCurrentStep();
  },
  methods: {
    async getCurrentStep() {
      if (!this.isDoneVerifyPhone) {
        return (this.step = 1);
      }
      if (!this.isDoneCreateUser) {
        return (this.step = 3);
      }
      if (!this.isDoneAddMoreInfo) {
        return (this.step = 4);
      }
      // this.step = 5;
      this.step = 6;
    },
    toSignIn() {
      this.$router.push("/login");
    },
    reload() {
      window.location.reload();
    },
  },
};
</script>

<style lang="scss">
@import "./signup.scss";
</style>
