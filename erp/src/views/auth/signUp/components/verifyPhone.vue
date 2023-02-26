<template>
  <v-form v-model="valid" @submit.prevent="verify">
    <label class="label_title mb-5">{{ $t("register.verifyCode") }}</label>
    <!-- <v-text-field
      v-model="code"
      :label="$t('register.verifyCode')"
      filled
      outlined
      color="grey lighten-1 "
      required
      :rules="codeRules"
      class="verify-code"
      hide-details
    ></v-text-field> -->
    <v-otp-input
      length="6"
      type="number"
      v-model="code"
      color="grey lighten-1 "
    ></v-otp-input>
    <v-btn
      type="submit"
      color="blue-grey"
      block
      class="btn_login mb-5 text-uppercase"
      :loading="loading"
      :disabled="!valid"
      style="margin-top: 15px"
    >
      {{ $t("common.confirm") }}
    </v-btn>
  </v-form>
</template>

<script>
import { verifyConfirmationPhoneNumber } from "@/plugins/firebase";
import { sleep } from "@/plugins/helpers";

export default {
  data() {
    return {
      valid: false,
      loading: false,
      codeRules: [(v) => v.length >= 6 || "Code is 6 characters"],
      code: "",
    };
  },
  created() {},
  methods: {
    async verify() {
      this.loading = true;
      try {
        var { phoneNumber } = await verifyConfirmationPhoneNumber(this.code);
        this.$store.commit("Signup/SET_PHONE_NUMBER", phoneNumber);
        await sleep(3000);
        this.$emit("success", phoneNumber);
      } catch (err) {
        this.showError("Phone Verification Failed");
      }
      this.loading = false;
    },
  },
};
</script>
