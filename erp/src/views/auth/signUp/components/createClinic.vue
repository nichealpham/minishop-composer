<template>
  <div>
    <img class="logo mb-1" :src="data.logo" />
    <v-file-input
      id="select-file"
      style="display: none; opacity: 0"
      @change="handleFileUpload()"
      accept="image/*"
      v-model="file"
      truncate-length="15"
    ></v-file-input>
    <v-form v-model="valid" @submit.prevent="registerClinic">
      <v-btn
        class="mb-3 btn-upload"
        style="margin-top: -5px; width: 90px; padding: 5px"
        @click="chooseFileUpload"
        :loading="loading"
      >
        {{ $t("register.editlogo") }}
      </v-btn>
      <label class="label_title text-capitalize">{{
        $t("register.registerclinic")
      }}</label>
      <v-text-field
        v-model="data.clinicName"
        :label="$t('clinic.name')"
        filled
        outlined
        color="grey lighten-1"
        required
        :rules="textRules"
        hide-details
      ></v-text-field>
      <v-text-field
        v-model="data.shortDescription"
        :label="$t('clinic.description')"
        filled
        outlined
        color="grey lighten-1"
        hide-details
      ></v-text-field>
      <v-text-field
        v-model="data.phone"
        :label="$t('clinic.hotline')"
        filled
        outlined
        color="grey lighten-1"
        required
        hide-details
      ></v-text-field>
      <v-text-field
        v-model="data.address"
        :label="$t('user.address')"
        filled
        outlined
        color="grey lighten-1"
        required
        :rules="textRules"
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
        {{ $t("register.register") }}
      </v-btn>
    </v-form>
  </div>
</template>

<script>
import { registerClinic } from "../owner";
import { firebaseUpload } from "@/plugins/firebase";

export default {
  data() {
    return {
      loading: false,
      uploading: false,
      valid: false,
      textRules: [
        (v) => !!v || "Value is required",
        (v) => v.length >= 6 || "Content must be bigger than 6 characters",
      ],
      data: {
        clinicName: "",
        shortDescription: "",
        logo: "icons/book.png",
        phone: "",
        taxNumber: "",
        address: "",
      },
      file: null,
    };
  },
  mounted() {},
  methods: {
    async registerClinic() {
      this.loading = true;
      try {
        var clinic = await registerClinic(this.$httpClient, this.data);
        if (clinic.error) {
          this.showError(`Register clinic failed! Please try again later`);
        } else {
          await this.refreshToken(clinic.clinicID);
          this.$emit("success", clinic);
        }
      } catch (error) {
        this.showError(`Register clinic failed! Please try again later`);
      }
      this.loading = false;
    },
    async refreshToken(clinicID) {
      var { user, token, error } = await this.$httpClient.get(
        `/user/refreshtoken`
      );
      if (error)
        return this.showError("Cannot get refresk token! Please try again.");
      else {
        this.$store.commit("Authen/SET_USER", user);
        this.$store.commit("Authen/SET_TOKEN", token);
        this.$store.commit("Authen/SET_ROLE", clinicID);
      }
    },
    chooseFileUpload() {
      document.querySelector("#select-file").click();
    },
    async handleFileUpload() {
      if (!this.file) {
        return;
      }
      var { name } = this.file;
      var path = `logo/${name}`;
      this.uploading = true;
      var downloadUrl = await firebaseUpload(this.file, path);
      this.uploading = false;
      if (downloadUrl) {
        this.data.logo = downloadUrl;
      }
    },
  },
};
</script>
