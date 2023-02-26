<template>
  <v-form @submit.prevent="createAppointment">
    <v-list-item-title class="content_card_title text-center mb-5">
      <b>{{ $t("common.detail") }}</b>
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
      <!-- List Input -->
      <v-list>
        <!-- Patient Name -->
        <v-list-item class="pr-0 pl-0 ">
          <v-list-item-content class="pa-0">
            <v-list-item-title>
              <b>{{ $t("booking.time") }}</b></v-list-item-title
            >
            <v-list-item-subtitle>
              <date-time-popup v-model="timeData" />
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <!-- Note -->
        <v-list-item class="pr-0 pl-0">
          <v-list-item-content class="pa-0">
            <v-list-item-title>
              <b>{{ $t("booking.complaint") }}</b></v-list-item-title
            >
            <v-list-item-subtitle>
              <v-textarea
                v-model="chiefComplaint"
                placeholder="Input Description"
                outlined
                rows="3"
              ></v-textarea
            ></v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <v-list-item class="pr-0 pl-0">
          <v-list-item-content class="pa-0">
            <v-list-item-title>
              <b>{{ $t("booking.note") }}</b></v-list-item-title
            >
            <v-list-item-subtitle>
              <v-textarea
                v-model="clinicalNote"
                placeholder="Input Description"
                outlined
                rows="3"
              ></v-textarea
            ></v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-list>
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
        <v-icon class="mr-2" small>mdi-content-save-outline</v-icon>
        {{ $t("common.save") }}
      </v-btn>
      <v-spacer />
    </v-card>
  </v-form>
</template>

<script>
import DateTimePopup from "@/components/booking/DateTimePopup";
import ProviderSelector from "@/components/services/ProviderSelector.vue";

export default {
  props: ["patientID"],
  components: {
    DateTimePopup,
    ProviderSelector,
  },
  computed: {
    clinic() {
      return this.$store.getters["Authen/getRole"];
    },
    valid() {
      var { service, doctor } = this.provider;
      var { timeStart, timeEnd } = this.timeData;
      return service.serviceID && doctor.userID && timeStart && timeEnd;
    },
  },
  data: () => ({
    loading: false,
    step: 1,
    chiefComplaint: "",
    clinicalNote: "",
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
    timeData: {
      timeStart: null,
      timeEnd: null,
    },
  }),
  methods: {
    async createAppointment() {
      if (!this.timeData.timeStart) return;
      if (!this.timeData.timeEnd) return;
      if (!this.patientID) return;
      this.loading = true;
      var body = {
        timeStart: this.timeData.timeStart,
        timeEnd: this.timeData.timeEnd,
        chiefComplaint: this.chiefComplaint,
        patientID: this.patientID,
        serviceID: this.provider.service.serviceID,
        doctorID: this.provider.doctor.userID,
        clinicalNote: this.clinicalNote,
      };
      var clinicID = this.clinic.clinicID.toLowerCase();
      var result = await this.$httpClient.post(
        `/owner/clinics/${clinicID}/appointments`,
        null,
        body
      );
      if (result.error) {
        this.showError("Create appointment failed. Please try again later.");
      } else {
        this.clean();
        this.$emit("success", result);
        // this.showSuccess(
        //   `Successfull create appointment for ${result.userCreate.fullName}`
        // );
        this.$router.push(`/booking?id=${result.appointmentID}`);
      }
      this.loading = false;
    },
    clean() {
      this.step = 1;
      this.chiefComplaint = "";
      this.clinicalNote = "";
      this.timeData = {
        timeStart: null,
        timeEnd: null,
      };
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
