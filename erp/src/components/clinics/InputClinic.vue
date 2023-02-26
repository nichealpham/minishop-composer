<template>
  <v-form
    v-model="valid"
    @submit.prevent="clinicID ? updateClinic() : createClinic()"
  >
    <v-list-item-title class="content_card_title text-center mb-3">
      <b>{{ $t("common.detail") }}</b>
    </v-list-item-title>
    <v-row class="avatar__detail mt-2">
      <v-avatar size="120px">
        <img alt="Avatar" :src="body.logo" />
      </v-avatar>
      <choose-file v-model="body.logo" />
    </v-row>
    <v-list>
      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("clinic.name") }}</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-text-field
              placeholder="Input Clinic Name"
              dense
              outlined
              v-model="body.clinicName"
              required
              :rules="textRules"
            ></v-text-field
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("clinic.hotline") }}</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-text-field
              placeholder="Input Address"
              dense
              outlined
              v-model="body.phone"
              required
              :rules="textRules"
            ></v-text-field
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("user.address") }}</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-text-field
              placeholder="Input Address"
              dense
              outlined
              v-model="body.address"
              required
              :rules="textRules"
            ></v-text-field
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("clinic.description") }}</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-textarea
              placeholder="Input Description"
              outlined
              v-model="body.shortDescription"
            ></v-textarea
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title> <b>Website</b></v-list-item-title>
          <v-list-item-subtitle>
            <v-text-field
              placeholder="Input Clinic website"
              dense
              outlined
              v-model="body.publicNameUrl"
            ></v-text-field
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-list>
    <v-card outlined class="toolbox_input pa-2">
      <v-spacer />
      <v-btn depressed color="#eeeeee" class="button" @click="close">
        <v-icon small class="mr-2">
          mdi-close-circle-outline
        </v-icon>
        {{ $t("common.cancel") }}
      </v-btn>
      <v-spacer />
      <v-btn
        type="submit"
        :disabled="!valid"
        :loading="loading"
        depressed
        color="#6166f5"
        class="button white--text ml-2"
      >
        <v-icon class="mr-2" small>mdi-content-save-outline</v-icon>
        {{ $t("common.save") }}
      </v-btn>
      <v-spacer />
    </v-card>
    <popup-confirm
      ref="PopupConfirm"
      :title="confirm.title"
      :message="confirm.message"
    />
  </v-form>
</template>

<script>
import ChooseFile from "@/components/chooseFile.vue";
import PopupConfirm from "@/components/PopupConfirm.vue";
export default {
  components: { ChooseFile, PopupConfirm },
  data() {
    return {
      confirm: {
        title: "Authentication",
        message:
          "Create new clinic will cost fee and max 24 hours for verification. Are you sure to proceed?",
      },
      clinicID: null,
      valid: false,
      loading: false,
      body: {
        clinicName: "",
        shortDescription: "",
        logo: "icons/clinic.png",
        phone: "",
        taxNumber: "",
        address: "",
        publicNameUrl: "",
      },
      textRules: [
        (v) => !!v || "Value is required",
        (v) => v.length >= 6 || "Content must be bigger than 6 characters",
      ],
    };
  },
  methods: {
    async openEdit(clinicID) {
      if (!clinicID) return;
      this.clean();
      this.clinicID = clinicID;
      this.loading = true;
      var clinic = await this.$httpClient.get(`/owner/clinics/${clinicID}`);
      if (clinic.error) {
        this.showError(`Register clinic failed! Please try again later`);
      } else {
        this.body = clinic;
      }
      this.loading = false;
    },
    async updateClinic() {
      if (!this.clinicID) return;
      var clinic = await this.$httpClient.put(
        `/owner/clinics/${this.clinicID}`,
        null,
        this.body
      );
      if (clinic.error) {
        this.showError(`Update clinic failed! Please try again later`);
      } else {
        this.$emit("success", clinic);
        this.clean();
        this.close();
      }
      this.loading = false;
    },
    async createClinic() {
      if (!(await this.$refs.PopupConfirm.confirm())) return;
      this.loading = true;
      var clinic = await this.$httpClient.post(
        `/owner/clinics`,
        null,
        this.body
      );
      if (clinic.error) {
        this.showError(`Register clinic failed! Please try again later`);
      } else {
        await this.refreshToken(clinic.clinicID);
        this.$emit("success", clinic);
        this.clean();
        this.close();
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
    clean() {
      this.clinicID = null;
      this.body = {
        clinicName: "",
        shortDescription: "",
        logo: "icons/clinic.png",
        phone: "",
        taxNumber: "",
        address: "",
        publicNameUrl: "",
      };
    },
    close() {
      this.clean();
      this.closeDiaglogIfMobile();
      this.$emit("close");
    },
  },
};
</script>
