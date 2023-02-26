<template>
  <book-cover ref="BookCover">
    <v-list-item-title class="content_card_title text-center mb-3">
      <b>{{ $t("user.profile") }}</b>
    </v-list-item-title>
    <div style="text-align: center" v-if="loading">
      <v-progress-circular indeterminate color="primary"></v-progress-circular>
    </div>
    <div class="info_layout" v-else>
      <v-row class="avatar__detail mt-2">
        <v-avatar size="100px">
          <img alt="Avatar" :src="patient.avatar || 'icons/user.png'" />
        </v-avatar>
        <choose-file v-model="patient.avatar" />
      </v-row>
      <v-row class="name__detail mb-5">
        <b class="mb-1">{{ patient.fullName }}</b>
      </v-row>
      <!-- Info -->
      <v-form v-model="valid" @submit.prevent="update">
        <v-list>
          <v-list-item class="pr-0 pl-0">
            <v-list-item-content class="pa-0">
              <v-list-item-title>
                <b>{{ $t("user.fullName") }}*</b></v-list-item-title
              >
              <v-list-item-subtitle>
                <v-text-field
                  dense
                  outlined
                  v-model="patient.fullName"
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
                  v-model="patient.dob"
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
                  v-model="patient.genderType"
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
                  v-model="patient.profileUrl"
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
                  v-model="patient.email"
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
                  v-model="patient.address"
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
                  v-model="patient.occupation"
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
                  v-model="patient.country"
                ></v-text-field
              ></v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-list>

        <v-card outlined class="toolbox_input pa-2">
          <v-spacer />
          <v-btn
            depressed
            color="#eeeeee"
            class="button"
            @click="$emit('cancel')"
          >
            <v-icon small class="mr-2"> mdi-close-circle-outline </v-icon>
            {{ $t("common.cancel") }}
          </v-btn>
          <v-spacer />
          <v-btn
            type="submit"
            :disabled="!valid"
            :loading="updating"
            depressed
            color="#6166f5"
            class="button white--text ml-2"
          >
            <v-icon class="mr-2" small>mdi-content-save-outline</v-icon>
            {{ $t("common.save") }}
          </v-btn>
          <v-spacer />
        </v-card>
      </v-form>
    </div>
  </book-cover>
</template>

<script>
import ChooseFile from "@/components/chooseFile.vue";
import BookCover from "@/components/BookCover.vue";
import moment from "moment";

export default {
  components: { BookCover, ChooseFile },
  data() {
    return {
      valid: false,
      patient: {
        avatar: "",
        fullName: "Patient Name",
        genderType: false,
        dob: "---",
        occupation: "---",
        country: "---",
        address: "---",
        email: "",
        phone: "",
        profileUrl: "",
      },
      loading: false,
      updating: false,
      textRules: [
        (v) => !!v || "Value is required",
        (v) => v.length >= 6 || "Content must be bigger than 6 characters",
      ],
      genderOptions: [
        {
          text: "Male",
          value: true,
        },
        { text: "Female", value: false },
      ],
    };
  },
  mounted() {
    this.render();
  },
  methods: {
    async render() {
      this.$refs.BookCover.showContent();
      this.loading = true;
      var query = {};
      if (this.$clinicID) {
        query.clinicID = this.$clinicID;
      }
      var patient = await this.$httpClient.get(`/user/profile`, query);
      if (patient.error) {
        this.showError("Cannot get patient profile! Please try again later");
      } else {
        patient.dob = moment(patient.dob).format("DD-MM-YYYY");
        patient.address = patient.address || this.$t("common.notprovide");
        patient.occupation = patient.occupation || this.$t("common.notprovide");
        patient.address = patient.address || this.$t("common.notprovide");
        patient.country = patient.country || this.$t("common.notprovide");
        this.patient = patient;
      }
      this.loading = false;
    },
    async update() {
      var body = { ...this.patient };
      this.updating = true;
      body.dob = moment(this.patient.dob, "DD-MM-YYYY").format("YYYY-MM-DD");
      var query = {};
      if (this.$clinicID) {
        query.clinicID = this.$clinicID;
      }
      var result = await this.$httpClient.put(`/user/profile`, query, body);
      if (!result || result.error) {
        this.updating = false;
        return this.showError(
          "Cannot update personal profile! Please try again later"
        );
      }
      await this.refreshToken();
      this.showSuccess("Update personal profile successfully!");
      this.updating = false;
      this.$emit("success");
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
  },
};
</script>
