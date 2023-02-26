<template>
  <v-form @submit.prevent="checkin">
    <v-list-item-title class="content_card_title text-center mb-5">
      <b>{{ $t("common.checkin") }}</b>
    </v-list-item-title>
    <provider-selector
      ref="ProviderSelector"
      v-model="provider"
      v-show="step == 1"
      @done="step = 2"
    />
    <div v-show="step == 2">
      <div class="pl-1 pr-1">
        <p>
          <v-icon class="mr-2" color="black">
            mdi-clipboard-file-outline
          </v-icon>
          <b>{{ $t("nav.service") }}:</b>
          {{ provider.service.serviceName }}
        </p>
        <p>
          <v-icon class="mr-2" color="black">mdi-clipboard-text-outline</v-icon>
          <b>{{ $t("nav.doctor") }} :</b>
          {{ provider.doctor.fullName }}
        </p>
        <p>
          <v-icon class="mr-2" color="black">
            mdi-map-marker-radius-outline
          </v-icon>
          <b>{{ $t("nav.clinic") }}:</b>
          {{ clinic.clinicName }}
        </p>
      </div>
    </div>
    <v-card outlined class="toolbox_input pa-2">
      <v-spacer />
      <v-btn depressed color="#eeeeee" class="button" @click="cancel">
        <v-icon small class="mr-2">
          mdi-close-circle-outline
        </v-icon>
        {{ $t("common.cancel") }}
      </v-btn>
      <v-spacer />
      <v-btn
        type="submit"
        depressed
        color="#6166f5"
        class="button white--text ml-2"
        :loading="loading"
        :disabled="!valid"
      >
        <v-icon class="mr-2" small>mdi-logout</v-icon>
        {{ $t("common.checkin") }}
      </v-btn>
      <v-spacer />
    </v-card>
  </v-form>
</template>

<script>
import ProviderSelector from "@/components/services/ProviderSelector.vue";

export default {
  props: ["patientID"],
  components: {
    ProviderSelector,
  },
  computed: {
    clinic() {
      return this.$store.getters["Authen/getRole"];
    },
    valid() {
      var { service, doctor } = this.provider;
      return service.serviceID && doctor.userID && this.patientID;
    },
  },
  data: () => ({
    loading: false,
    step: 1,
    provider: {
      service: {
        serviceID: "",
        serviceName: "",
      },
      doctor: {
        userID: "",
        fullName: "",
      },
    },
  }),
  methods: {
    async checkin() {
      this.loading = true;
      var body = {
        admissionType: 1,
        userAdmittedID: this.patientID,
        records: [
          {
            userAppointID: this.provider.doctor.userID,
            serviceID: this.provider.service.serviceID,
          },
        ],
      };
      var clinicID = this.clinic.clinicID.toLowerCase();
      var result = await this.$httpClient.post(
        `/owner/clinics/${clinicID}/episodes`,
        null,
        body
      );
      if (result.error) {
        this.showError("Check In failed. Please try again later.");
      } else {
        // this.showSuccess(
        //   `Successfull checkin patient into ${this.provider.service.serviceName}`
        // );
        this.$emit("success", result);
        this.clean();
        this.$router.push(`/episode?id=${result.episodeID}`);
      }
      this.loading = false;
    },
    clean() {
      this.step = 1;
      this.provider = {
        service: {
          serviceID: "",
          serviceName: "",
        },
        doctor: {
          userID: "",
          fullName: "",
        },
      };
      this.$refs.ProviderSelector.clean();
    },
    cancel() {
      this.clean();
      this.$emit("cancel");
    },
  },
};
</script>
