<template>
  <div>
    <v-form v-model="valid" @submit.prevent="$emit('success')">
      <label class="label_title text-capitalize">{{
        $t("register.registeraccount")
      }}</label>
      <v-text-field
        v-model="fullName"
        :label="$t('user.fullName')"
        filled
        outlined
        color="grey lighten-1"
        required
        :rules="nameRules"
        @change="saveProgress"
        hide-details
      ></v-text-field>
      <v-text-field
        v-model="email"
        :label="$t('user.email')"
        filled
        outlined
        color="grey lighten-1"
        required
        @change="saveProgress"
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
        @change="saveProgress"
        hide-details
      ></v-text-field>
      <v-btn
        type="submit"
        color="blue-grey"
        block
        class="btn_login mb-5 text-uppercase"
        :loading="loading"
        :disabled="!valid"
        style="margin-top: 15px"
      >
        {{ $t("register.continue") }}
      </v-btn>
    </v-form>
    <v-divider style="margin-top: 30px"></v-divider>
    <label class="label_or mb-5 white--text">{{ $t("auth.or") }}</label>
    <!-- <v-btn color="blue-grey" block @click="oauthSignIn(provider.Google)">
      <v-icon left dark> mdi-google </v-icon>
      Google
    </v-btn>
    <v-btn
      color="blue-grey"
      block
      @click="oauthSignIn(provider.Facebook)"
      class="mt-3"
    >
      <v-icon left dark> mdi-facebook </v-icon>
      Facebook
    </v-btn> -->
  </div>
</template>

<script>
import { OAuthSignIn, OAuthProviderType } from "@/plugins/firebase";

export default {
  data() {
    return {
      provider: OAuthProviderType,
      loading: false,
      valid: false,
      fullName: this.$store.getters["Signup/userRegister"].fullName,
      email: this.$store.getters["Signup/userRegister"].email,
      password: this.$store.getters["Signup/userRegister"].password,
      nameRules: [
        (v) => !!v || "Name is required",
        (v) =>
          (!!v && v.length >= 6) || "Name must be bigger than 6 characters",
      ],
      passwordRules: [
        (v) => !!v || "Password is required",
        (v) =>
          (!!v && v.length >= 6) || "Password must be bigger than 6 characters",
      ],
      emailRules: [(v) => (!!v && /.+@.+/.test(v)) || "E-mail must be valid"],
    };
  },
  mounted() {},
  methods: {
    saveProgress() {
      var data = {
        fullName: this.fullName,
        password: this.password,
        email: this.email,
      };
      this.$store.commit("Signup/SET_INFO", data);
    },
    async oauthSignIn(provider) {
      this.loading = true;
      try {
        var token = await OAuthSignIn(provider);
        if (!token) {
          this.showError(`OAuth Token is missing!`);
        } else {
          this.$store.commit("Signup/SET_OAUTH_TOKEN", token);
          this.$emit("success");
        }
      } catch (err) {
        this.showError(`OAuth Signin failed!`);
      }
      this.loading = false;
    },
  },
};
</script>
