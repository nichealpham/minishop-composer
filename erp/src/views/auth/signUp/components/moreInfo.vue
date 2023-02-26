<template>
  <v-form v-model="valid" @submit.prevent="register">
    <label class="label_title text-capitalize">{{
      $t("register.additionalinfo")
    }}</label>
    <v-text-field
      v-model="dob"
      :label="$t('user.dob') + ' (DD-MM-YYYY)'"
      filled
      outlined
      color="grey lighten-1"
      required
      :rules="dobRules"
      @change="saveProgress"
      v-mask="'##-##-####'"
      hide-details
    ></v-text-field>
    <v-select
      v-model="genderType"
      :label="$t('user.gender')"
      filled
      outlined
      color="grey lighten-1"
      required
      :items="genderOptions"
      @change="saveProgress"
      hide-details
    ></v-select>
    <v-btn
      type="submit"
      color="blue-grey"
      block
      class="btn_login mb-5 text-uppercase"
      :loading="loading"
      :disabled="!valid"
      style="margin-top:15px"
    >
      {{ $t("register.register") }}
    </v-btn>
    <label class="label_title">{{ $t("register.message1") }}</label>
  </v-form>
</template>

<script>
import moment from "moment";
import { sleep } from "@/plugins/helpers";

export default {
  data() {
    return {
      loading: false,
      valid: false,
      dob: this.$store.getters["Signup/userRegister"].dob,
      genderType:
        this.$store.getters["Signup/userRegister"].genderType || false,
      password: "",
      dobRules: [(v) => (!!v && v.length == 10) || "DOB must be in DD-MM-YYYY"],
      genderOptions: [
        { text: this.$t("register.male"), value: true },
        { text: this.$t("register.female"), value: false },
      ],
    };
  },
  mounted() {},
  methods: {
    saveProgress() {
      var data = {
        dob: this.dob,
        genderType: this.genderType,
      };
      this.$store.commit("Signup/SET_MORE_INFO", data);
    },
    async register() {
      this.loading = true;
      try {
        var body = this.$store.getters["Signup/userRegister"];
        body.dob = moment(body.dob, "DD-MM-YYYY").format("YYYY-MM-DD");
        var { token, user, error } = await this.$httpClient.post(
          "user/register",
          null,
          body
        );
        if (error) {
          this.showError(`Register account failed. Wrong data input`);
        } else {
          this.handleAuthData({ token, user });
          await sleep(200);
          // check if there is any reference in the header
          var clinicID = this.$route.query.companyid;
          var userID = user.userID;
          if (clinicID) {
            // invite user as patient directly into this clinic
            await this.$httpClient.put(
              `/owner/clinics/${clinicID}/patients/${userID}/Invite`
            );
          }
          this.$emit("success");
        }
      } catch (error) {
        this.showError(`Register account failed. Wrong data input`);
      }
      this.loading = false;
    },
    handleAuthData({ token, user }) {
      this.$store.commit("Authen/SET_USER", user);
      this.$store.commit("Authen/SET_TOKEN", token);
    },
  },
};
</script>
