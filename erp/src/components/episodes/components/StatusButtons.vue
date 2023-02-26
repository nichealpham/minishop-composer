<template>
  <v-card outlined class="toolbox_input pa-2" v-if="isShowButtons">
    <v-spacer />
    <v-btn
      depressed
      class="button white--text text-capitalize"
      style="width: 160px"
      @click="cancel"
      :loading="cancelLoading"
      color="red"
      v-if="isAllowCancel"
    >
      <v-icon small class="mr-2"> mdi-close-circle-outline </v-icon>
      {{ $t("common.cancel") }} {{ $t("nav.episode") }}
    </v-btn>
    <v-spacer v-if="isAllowCancel && isAllowFinish" />
    <v-btn
      depressed
      color="#6166f5"
      class="button white--text text-capitalize"
      :loading="finishLoading"
      style="width: 180px"
      v-if="isAllowFinish"
      @click="finish"
      :disabled="disableFinish"
    >
      <v-icon class="mr-2" small>mdi-check-circle-outline</v-icon>
      {{ $t("common.save") + " & " + $t("common.finish") }}
    </v-btn>
    <v-spacer v-if="!isAllowBookMore" />
    <v-btn
      depressed
      class="button text-capitalize"
      style="width: 160px"
      v-if="isAllowBookMore"
      @click="$emit('book-more')"
    >
      <v-icon class="mr-2" small>mdi-calendar-range</v-icon>
      {{ $t("common.rebooking") }}
    </v-btn>
    <v-spacer v-if="isAllowBookMore" />
    <popup-confirm
      ref="PopupConfirm"
      :title="confirm.title"
      :message="confirm.message"
    />
  </v-card>
</template>

<script>
import { EpisodeStatusType, RoleType } from "@/plugins/contants";
import PopupConfirm from "@/components/PopupConfirm.vue";

export default {
  components: { PopupConfirm },
  props: {
    episode: {
      type: Object,
      default: null,
    },
  },
  computed: {
    roles() {
      return this.$store.getters["Authen/getUser"].roles || [];
    },
    userID() {
      return this.$store.getters["Authen/getUser"].userID || "";
    },
    isStaff() {
      return this.$store.getters["Authen/isClinicStaff"] || false;
    },
  },
  watch: {
    episode: {
      handler() {
        this.render();
      },
    },
  },
  data() {
    return {
      confirm: {
        title: "",
        message: "",
      },
      service: {},
      patient: {},
      doctor: {},
      isShowButtons: false,
      isAllowCancel: false,
      cancelLoading: false,
      finishLoading: false,
      isAllowFinish: false,
      disableFinish: false,
      isAllowBookMore: false,
      loading: true,
    };
  },
  mounted() {
    this.render();
  },
  methods: {
    render() {
      if (!this.episode) return;
      var {
        statusID,
        records,
        userAdmitted,
        clinicID,
        // userAdmittedID,
      } = this.episode;
      if (!records || !records.length) return;
      var { userAppointID, userAppoint, service } = records[0];
      if (!userAppointID) return;

      this.patient = userAdmitted;
      this.doctor = userAppoint;
      this.service = service;

      this.isAllowFinish =
        statusID == EpisodeStatusType.CheckedIn &&
        (this.userID.toLowerCase() == userAppoint.userID ||
          this.roles.find(
            (r) =>
              r.clinicID.toLowerCase() == clinicID &&
              r.roleType != RoleType.Patient
          ));

      // this.isAllowCancel =
      //   this.isAllowFinish || this.userID.toLowerCase() == userAdmittedID;

      // this.isAllowCancel = this.isAllowFinish;
      this.isAllowCancel = statusID == EpisodeStatusType.CheckedIn;

      this.isAllowBookMore =
        this.isStaff && statusID != EpisodeStatusType.CheckedIn;

      this.isShowButtons =
        statusID == EpisodeStatusType.CheckedIn || this.isAllowBookMore;
    },
    async cancel() {
      var episodeID = this.episode.episodeID;
      var { clinicID } = this.episode.clinic;
      this.confirm = {
        title: "Cancel episode",
        message: `Cancel episode with ${this.patient.fullName} for service ${this.service.serviceName}?`,
      };
      if (!(await this.$refs.PopupConfirm.confirm())) return;
      this.cancelLoading = true;
      var result = await this.$httpClient.delete(
        `/owner/clinics/${clinicID}/episodes/${episodeID}`
      );
      this.cancelLoading = false;
      if (result.error) {
        return this.showError("Cancel episode. Please try again later.");
      } else {
        // this.showSuccess(`Successfull cancel ${this.patient.fullName}`);
        this.$emit("cancel", this.episode.episodeID);
        // this.episode.statusID = EpisodeStatusType.Canceled;
        // this.render();
      }
    },
    async finish() {
      var episodeID = this.episode.episodeID;
      var { clinicID } = this.episode.clinic;
      this.confirm = {
        title: "Finish episode",
        message: `Finish episode for ${this.patient.fullName} with service ${this.service.serviceName}?`,
      };
      if (!(await this.$refs.PopupConfirm.confirm())) return;
      this.finishLoading = true;
      var result = await this.$httpClient.post(
        `/owner/clinics/${clinicID}/episodes/${episodeID}/complete`
      );
      this.finishLoading = false;
      if (result.error) {
        return this.showError("Finish episode failed. Please try again later.");
      } else {
        // this.showSuccess(
        //   `Successfull finish episode for ${this.patient.fullName}`
        // );
        this.$emit("success", this.episode.episodeID);
        // this.episode.statusID = EpisodeStatusType.Success;
        // this.render();
      }
    },
    disableFinishBtn() {
      this.disableFinish = true;
    },
    enableFinishBtn() {
      this.disableFinish = false;
    },
  },
};
</script>
