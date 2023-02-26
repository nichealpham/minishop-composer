<template>
  <div>
    <v-form v-if="!errored" v-model="valid" @submit.prevent="sendCode">
      <label class="label_title mb-5">{{ $t("auth.verifyphone") }}</label>
      <v-text-field
        v-model="phoneNumber"
        filled
        outlined
        color="grey lighten-1"
        required
        :rules="phoneNumberRules"
        v-mask="'+## #### ### ###'"
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
        id="verify-phone-number-btn"
      >
        {{ $t("auth.sendcode") }}
      </v-btn>
    </v-form>
    <div v-else>
      <v-btn
        type="submit"
        color="blue-grey"
        block
        class="btn_login mb-5 text-uppercase"
        style="margin-top: 15px"
        @click="reload"
      >
        {{ $t("error.retrysendcode") }}
      </v-btn>
    </div>
  </div>
</template>

<script>
import {
  sendConfirmationPhoneNumber,
  getRecapthaVerifierInstance,
} from "@/plugins/firebase";

export default {
  data() {
    return {
      valid: false,
      loading: false,
      phoneNumberRules: [
        (v) => v.length >= 15 || "Phone must be at least 10 characters",
      ],
      phoneNumber: "+84",
      errored: false,
    };
  },
  mounted() {
    getRecapthaVerifierInstance();
  },
  methods: {
    async sendCode() {
      this.loading = true;
      var phoneNumber = this.formatPhoneNumber(this.phoneNumber);
      var exist = await this.$httpClient.post(
        `/user/checkphone`,
        {},
        { phone: phoneNumber }
      );
      if (exist) {
        this.loading = false;
        return this.showErrorPopup(this.$t("error.phonealreadyinuse"));
      }
      try {
        var confirmationResult = await sendConfirmationPhoneNumber(phoneNumber);
        this.$emit("success", confirmationResult);
      } catch (err) {
        this.showErrorPopup(`${this.$t("error.verifycodefailed")}`);
        this.errored = true;
      }
      this.loading = false;
    },
    reload() {
      window.location.reload();
    },
  },
};
</script>
