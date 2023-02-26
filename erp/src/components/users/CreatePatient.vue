<template>
  <v-form v-model="valid" @submit.prevent="patientID ? update() : create()">
    <v-list-item-title
      class="content_card_title text-center mb-3 text-capitalize"
    >
      <b>{{ $t("common.detail") }}</b>
    </v-list-item-title>
    <v-row class="avatar__detail mt-2">
      <v-avatar size="120px">
        <img alt="Avatar" :src="body.avatar || 'icons/user.png'" />
      </v-avatar>
      <choose-file v-model="body.avatar" />
    </v-row>
    <v-list>
      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("user.phone") }}*</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-text-field
              placeholder="+## #### ### ###"
              dense
              outlined
              v-model="body.phone"
              required
              v-mask="'+## #### ### ###'"
              :rules="textRules"
              @change="handleInviteUserByPhone"
            ></v-text-field
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("user.fullName") }}*</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-text-field
              placeholder="*required"
              dense
              outlined
              v-model="body.fullName"
              required
              :rules="textRules"
            ></v-text-field
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("user.dob") }}*</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-text-field
              placeholder="DD-MM-YYYY"
              dense
              outlined
              v-model="body.dob"
              required
              v-mask="'##-##-####'"
              :rules="textRules"
            ></v-text-field
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("user.gender") }}*</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-select
              placeholder="Choose"
              dense
              outlined
              v-model="body.genderType"
              :items="genderOptions"
              required
            ></v-select>
          </v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("user.profileUrl") }}</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-text-field
              placeholder="Input code"
              dense
              outlined
              v-model="body.profileUrl"
            ></v-text-field
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("user.email") }}</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-text-field
              placeholder="Input email"
              dense
              outlined
              v-model="body.email"
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
            <v-textarea
              placeholder="Input Address"
              dense
              outlined
              v-model="body.address"
              rows="3"
            ></v-textarea
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("user.occupation") }}</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-text-field
              placeholder="Input occupation"
              dense
              outlined
              v-model="body.occupation"
            ></v-text-field
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-list-item class="pr-0 pl-0">
        <v-list-item-content class="pa-0">
          <v-list-item-title>
            <b>{{ $t("user.country") }}</b></v-list-item-title
          >
          <v-list-item-subtitle>
            <v-text-field
              placeholder="Input country"
              dense
              outlined
              v-model="body.country"
            ></v-text-field
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-list>
    <v-card outlined class="toolbox_input pa-2">
      <v-spacer />
      <v-btn depressed color="#eeeeee" class="button" @click="close">
        <v-icon small class="mr-2"> mdi-close-circle-outline </v-icon>
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
    <popup-user-profile ref="PopupUserProfile" :alias="alias" />
  </v-form>
</template>

<script>
import ChooseFile from "@/components/chooseFile.vue";
import PopupUserProfile from "@/components/PopupUserProfile.vue";
import moment from "moment";

export default {
  props: {
    alias: {
      type: String,
      default: "patients",
    },
  },
  components: { ChooseFile, PopupUserProfile },
  computed: {
    clinicID() {
      return this.$store.getters["Authen/getRole"].clinicID || "";
    },
  },
  data() {
    return {
      genderOptions: [
        {
          text: "Male",
          value: true,
        },
        { text: "Female", value: false },
      ],
      patientID: null,
      valid: false,
      loading: false,
      body: {
        fullName: "",
        genderType: false,
        dob: "",
        avatar: "",
        phone: "+84",
        email: "",
        address: "",
        occupation: "",
        country: "",
        profileUrl: "",
      },
      textRules: [
        (v) => !!v || "Value is required",
        (v) => v.length >= 6 || "Content must be bigger than 6 characters",
      ],
    };
  },
  methods: {
    prepareDob(dob) {
      return moment(dob, "DD-MM-YYYY").format("YYYY-MM-DD");
    },
    preparePhone(phone) {
      return phone.split("-").join("").split(" ").join("");
    },
    prepareBody() {
      var body = { ...this.body };
      body.dob = this.prepareDob(body.dob);
      body.phone = this.preparePhone(body.phone);
      return body;
    },
    async openEdit(patientID) {
      if (!patientID || !this.clinicID) return;
      this.clean();
      this.loading = true;
      this.patientID = patientID;
      var data = await this.$httpClient.get(
        `/owner/clinics/${this.clinicID}/${this.alias}/${patientID}`
      );
      if (data.error) {
        this.showError(`Register patient failed! Please try again later`);
      } else {
        data.dob = moment(data.dob).format("DD-MM-YYYY");
        this.body = data;
      }
      this.loading = false;
    },
    async update() {
      if (!this.patientID || !this.clinicID) return;
      this.loading = true;
      var body = this.prepareBody();
      var data = await this.$httpClient.put(
        `/owner/clinics/${this.clinicID}/${this.alias}/${this.patientID}`,
        null,
        body
      );
      if (data.error) {
        this.showError(`Update patient profile failed! Please try again later`);
      } else {
        this.$emit("success", data);
        this.close();
      }
      this.loading = false;
    },
    async create() {
      this.loading = true;
      if (!this.clinicID) return;
      var body = this.prepareBody();
      var data = await this.$httpClient.post(
        `/owner/clinics/${this.clinicID}/${this.alias}`,
        null,
        body
      );
      if (data.error) {
        this.showError(`Register clinic failed! Please try again later`);
      } else {
        this.$emit("success", data);
        this.close();
      }
      this.loading = false;
    },
    async handleInviteUserByPhone() {
      var phone = this.preparePhone(this.body.phone);
      var user = await this.$httpClient.post(`/user/getbyphone`, {}, { phone });
      if (user && !user.error) {
        if (await this.$refs.PopupUserProfile.open(user)) {
          await this.$httpClient.put(
            `/owner/clinics/${this.clinicID}/${this.alias}/${user.userID}/invite`
          );
          this.$emit("success", user);
          this.close();
        }
      }
    },
    clean() {
      this.patientID = null;
      this.body = {
        fullName: "",
        genderType: false,
        dob: "",
        avatar: "",
        phone: "+84",
        email: "",
        address: "",
        occupation: "",
        country: "",
      };
    },
    close() {
      this.closeDiaglogIfMobile();
      this.$emit("close");
      this.clean();
    },
  },
};
</script>
