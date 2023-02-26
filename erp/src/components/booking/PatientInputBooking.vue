<template>
  <v-form>
    <v-list-item-title class="content_card_title text-center mb-5">
      <b>{{ $t("common.info") + " " + $t("nav.booking") }}</b>
    </v-list-item-title>
    <div class="pl-1 pr-1">
      <p>
        <v-icon class="mr-2" color="black">
          mdi-clipboard-file-outline
        </v-icon>
        <b>{{ $t("nav.service") }}:</b>
        {{ infoData.service.serviceName }}
      </p>
      <p>
        <v-icon class="mr-2" color="black">mdi-clipboard-text-outline</v-icon>
        <b>{{ $t("nav.doctor") }} :</b>
        {{ infoData.doctor.fullName }}
      </p>
      <p>
        <v-icon class="mr-2" color="black">
          mdi-map-marker-radius-outline
        </v-icon>
        <b>{{ $t("nav.clinic") }}:</b>
        {{ infoData.clinic.clinicName }}
      </p>
      <p>
        <v-icon class="mr-2" color="black">mdi-clipboard-text-outline</v-icon>
        <b>{{ $t("user.address") }}:</b>
        {{ infoData.clinic.address }}
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
              v-model="note"
              placeholder="Input Description"
              outlined
            ></v-textarea
          ></v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-list>

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
        depressed
        color="#6166f5"
        class="button white--text ml-2"
        :loading="loading"
        @click="createAppointment"
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

export default {
  components: {
    DateTimePopup,
  },
  data: () => ({
    note: "",
    loading: false,
    timeData: {
      timeStart: null,
      timeEnd: null,
    },
    infoData: {
      providerID: "",
      doctor: {
        fullName: "",
      },
      service: {
        serviceName: "",
      },
      clinic: {
        clinicName: "",
        address: "",
      },
    },
  }),
  methods: {
    render(infoData = {}) {
      this.clean();
      this.infoData = infoData;
    },
    async createAppointment() {
      if (!this.infoData.providerID) return;
      if (!this.timeData.timeStart) return;
      if (!this.timeData.timeEnd) return;
      this.loading = true;
      var body = {
        providerID: this.infoData.providerID,
        timeStart: this.timeData.timeStart,
        timeEnd: this.timeData.timeEnd,
        chiefComplaint: this.note,
      };
      var result = await this.$httpClient.post(
        `/user/appointments`,
        null,
        body
      );
      if (result.error) {
        this.showError("Create appointment failed. Please try again later.");
      } else {
        this.$emit("success", result);
      }
      this.loading = false;
    },
    clean() {
      this.note = "";
      this.timeData = {
        timeStart: null,
        timeEnd: null,
      };
      this.infoData = {
        providerID: "",
        doctor: {
          fullname: "",
        },
        service: {
          serviceName: "",
        },
        clinic: {
          clinicName: "",
          address: "",
        },
      };
    },
    cancel() {
      this.$emit("cancel");
    },
  },
};
</script>
