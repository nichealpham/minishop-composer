<template>
  <v-row class="login" justify="center" align="center">
    <v-col
      xl="5"
      lg="6"
      md="6"
      sm="9"
      xs="12"
      class="main_container"
      style="max-width: 600px"
    >
      <v-container fill-height>
        <v-row>
          <v-col cols="12" justify="center" align="center">
            <img
              class="logo mb-1"
              src="/icon.png"
              style="border-radius: 18px"
            />
            <h1 class="page_title text-center mb-3">{{ $t("auth.login") }}</h1>
          </v-col>
          <v-col cols="12" justify="center" align="center" class="pl-7 pr-7">
            <v-form v-model="valid" @submit.prevent="login">
              <v-text-field
                v-model="email"
                :label="$t('auth.emailorphone')"
                filled
                outlined
                color="grey lighten-1"
                required
                hide-details
              ></v-text-field>
              <v-text-field
                v-model="password"
                :label="$t('user.password')"
                filled
                outlined
                color="grey lighten-1"
                type="password"
                required
                :rules="passwordRules"
                hide-details
              ></v-text-field>
              <v-btn
                type="submit"
                color="blue-grey"
                block
                class="btn_login mb-5 text-uppercase"
                :loading="loading"
                :disabled="!valid"
              >
                {{ $t("auth.login") }}
              </v-btn>
            </v-form>
            <v-divider style="margin-top: 30px"></v-divider>
            <label v-if="!isNative" class="label_or mb-5 white--text">{{
              $t("auth.or")
            }}</label>
            <!-- <v-btn
              color="blue-grey"
              block
              @click="oauth2Login(provider.Google)"
              v-if="!isNative"
            >
              <v-icon left dark> mdi-google </v-icon>
              Google
            </v-btn>
            <v-btn
              color="blue-grey"
              class="mt-3 mb-10"
              block
              @click="oauth2Login(provider.Facebook)"
              v-if="!isNative"
            >
              <v-icon left dark> mdi-facebook </v-icon>
              Facebook
            </v-btn> -->
            <p class="bottom">{{ $t("auth.donthaveaccount") }}</p>
            <p>
              <u
                ><a
                  style="color: #605bff; font-weight: bold"
                  @click="toSignup()"
                  >{{ $t("auth.signup") }} {{ $t("common.account") }}
                </a>
              </u>
              <!-- <span class="ml-2 mr-2">|</span>
              <u
                ><a style="color:#605bff;font-weight:bold;" @click="toClinic()"
                  >{{ $t("auth.signup") }} {{ $t("nav.clinic") }}
                </a>
              </u> -->
            </p>
          </v-col>
        </v-row>
      </v-container>
    </v-col>
  </v-row>
</template>

<script>
import { OAuthProviderType } from "@/plugins/firebase";
import { login, oauth2Login } from "./login";
export default {
  data() {
    return {
      provider: OAuthProviderType,
      loading: false,
      valid: false,
      email: "",
      password: "",
      passwordRules: [
        (v) => !!v || "Password is required",
        (v) => v.length >= 6 || "Password must be bigger than 6 characters",
      ],
      // emailRules: [
      //   (v) => !!v || "E-mail is required",
      //   (v) => /.+@.+/.test(v) || "E-mail must be valid",
      // ],
      emailRules: [
        (v) => !!v || "Phone is required",
        (v) => v.length == 12 || "Value must be 10 characters",
      ],
    };
  },
  created() {},
  methods: {
    async login() {
      this.loading = true;
      var phone = this.formatPhoneNumber(this.email);
      var { token, user, error } = await login(
        this.$httpClient,
        phone,
        this.password
      );
      if (error) {
        this.loading = false;
        return this.showError("Invalid login credentials!");
      }
      this.handleAuthData({ token, user });
      this.loading = false;
    },
    async oauth2Login(provider = OAuthProviderType.Google) {
      this.loading = true;
      var result = await oauth2Login(this.$httpClient, provider);
      this.loading = false;
      if (result) {
        var { token, user, error } = result;
        if (error) {
          return this.showError("Invalid login credentials!");
        }
        this.handleAuthData({ token, user });
      }
    },
    async handleAuthData({ token, user }) {
      this.$store.commit("Authen/SET_USER", user);
      this.$store.commit("Authen/SET_TOKEN", token);
      var redirect = this.$route.query.redirect;
      if (redirect && (await this.checkAuthorizedDomain(redirect))) {
        return (window.location.href = `${redirect}?token=${token}`);
      }
      window.parent.postMessage(
        JSON.stringify({ channel: "Authentication", token }),
        "*"
      );
      this.$router.push("/home");
    },
    toSignup() {
      // this.$router.push({ path: "/register", query: this.$route.query });
      this.$router.push({ path: "/signup", query: this.$route.query });
      // window.open("https://his.sandrasoft.app/register");
    },
    toClinic() {
      this.$router.push({ path: "/clinic", query: this.$route.query });
    },
  },
};
</script>

<style lang="scss">
@import "./login.scss";
</style>
