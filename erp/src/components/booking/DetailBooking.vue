<template>
  <book-cover ref="BookCover">
    <v-btn
      icon
      @click="render()"
      fixed
      top
      right
      color="white"
      style="right: 70px; top: -45px; position: absolute"
    >
      <v-icon>mdi-refresh</v-icon>
    </v-btn>
    <v-list-item-title class="content_card_title text-center mb-3">
      <b>{{ $t("common.detail") }}</b>
    </v-list-item-title>
    <div style="text-align: center" v-if="loading">
      <v-progress-circular indeterminate color="#6166f5"></v-progress-circular>
    </div>
    <div class="info_layout" v-else>
      <v-row class="avatar__detail mt-2">
        <v-avatar size="100px">
          <img
            alt="Avatar"
            :src="
              (userID.toLowerCase() == patient.userCreate.userID
                ? patient.userAppoint.avatar
                : patient.userCreate.avatar) || 'icons/user.png'
            "
          />
        </v-avatar>
      </v-row>
      <v-row class="name__detail">
        <b class="mb-1">{{
          (userID.toLowerCase() == patient.userCreate.userID
            ? patient.userAppoint.fullName
            : patient.userCreate.fullName) || ""
        }}</b>
      </v-row>
      <v-row class="mb-2 user_button_detail">
        <v-btn
          :disabled="
            !(userID.toLowerCase() == patient.userCreate.userID
              ? patient.userAppoint.email
              : patient.userCreate.email)
          "
          depressed
          color="#eeeeee"
          class="button mr-2 text-capitalize"
          style="width: 100px"
        >
          <v-icon left dark> mdi-email-edit-outline </v-icon>
          {{ $t("user.email") }}
        </v-btn>
        <v-btn
          @click="
            callPhone(
              userID.toLowerCase() == patient.userCreate.userID
                ? patient.userAppoint.phone
                : patient.userCreate.phone
            )
          "
          :disabled="
            !(userID.toLowerCase() == patient.userCreate.userID
              ? patient.userAppoint.phone
              : patient.userCreate.phone)
          "
          depressed
          color="#eeeeee"
          class="button text-capitalize"
          style="width: 100px"
        >
          <v-icon left dark> mdi-phone </v-icon>
          {{ $t("user.call") }}
        </v-btn>
      </v-row>
      <v-list-item class="user_divider pr-2 pl-2 mb-5">
        <v-divider></v-divider>
      </v-list-item>
      <!-- Info -->
      <div class="pl-1 pr-1">
        <p>
          <v-icon class="mr-2" color="black">mdi-calendar-range</v-icon>
          <b>{{ $t("booking.time") }}:</b>
          <b class="green--text ml-1">{{
            formatTime(patient.timeStart, patient.timeEnd)
          }}</b>
        </p>
        <p>
          <v-icon class="mr-2" color="black">mdi-clipboard-file-outline</v-icon>
          <b>{{ $t("nav.service") }}:</b>
          {{ patient.service.serviceName }}
        </p>
        <p>
          <v-icon class="mr-2" color="black">mdi-currency-usd</v-icon>
          <b>{{ $t("booking.price") }}:</b>
          <b class="green--text ml-1">{{
            formatPrice(patient.service.price)
          }}</b>
        </p>
        <p>
          <v-icon class="mr-2" color="black">
            mdi-shield-account-outline
          </v-icon>
          <b
            >{{
              userID.toLowerCase() == patient.userCreate.userID
                ? $t("nav.patient")
                : $t("nav.doctor")
            }}:</b
          >
          {{
            userID.toLowerCase() == patient.userCreate.userID
              ? patient.userCreate.fullName
              : patient.userAppoint.fullName
          }}
        </p>
        <p>
          <v-icon class="mr-2" color="black">mdi-home-plus-outline</v-icon>
          <b>{{ $t("nav.clinic") }}:</b>
          {{ patient.clinic.clinicName }}
        </p>
        <p>
          <v-icon class="mr-2" color="black"
            >mdi-map-marker-radius-outline</v-icon
          >
          <b>{{ $t("user.address") }}:</b>
          {{ patient.clinic.address }}
        </p>
        <p v-if="patient.chiefComplaint">
          <v-icon class="mr-2" color="black">mdi-note-text-outline</v-icon>
          <b>{{ $t("booking.complaint") }}:</b>
          {{ patient.chiefComplaint }}
        </p>
        <p v-if="patient.clinicalNote">
          <v-icon class="mr-2" color="black">mdi-note-text-outline</v-icon>
          <b>{{ $t("booking.note") }}:</b>
          {{ patient.clinicalNote }}
        </p>
      </div>
      <div
        v-if="patient.episodeID"
        class="pa-5 mt-5 mb-5 elevation-1"
        style="border: 1px solid #bdbdbd; overflow: hidden; border-radius: 12px"
      >
        <EpisodeDetail :episodeID="patient.episodeID" />
      </div>
    </div>
    <v-card outlined class="toolbox_input pa-2">
      <v-spacer />
      <v-btn
        depressed
        class="button text-capitalize white--text"
        style="width: 160px"
        @click="cancel"
        :loading="cancelLoading"
        v-if="isAllowCancel"
        color="pink"
      >
        <v-icon small class="mr-2"> mdi-close-circle-outline </v-icon>
        {{ $t("common.delete") }}
      </v-btn>
      <v-spacer v-if="isAllowCancel && isAllowCheckIn" />
      <v-btn
        depressed
        color="#6166f5"
        class="button white--text text-capitalize"
        style="width: 160px"
        @click="checkin"
        :loading="checkInLoading"
        v-if="isAllowCheckIn"
      >
        <v-icon small class="mr-1"> mdi-logout </v-icon>
        {{ $t("common.checkin") }}
      </v-btn>
      <v-spacer />
    </v-card>
    <popup-confirm
      ref="PopupConfirm"
      :title="confirm.title"
      :message="confirm.message"
    />
  </book-cover>
</template>

<script>
import { convertPriceString } from "@/plugins/helpers";
import { RoleType } from "@/plugins/contants";
import BookCover from "@/components/BookCover.vue";
import PopupConfirm from "@/components/PopupConfirm.vue";
import EpisodeDetail from "@/components/episodes/EpisodeDetail.vue";

import moment from "moment";
export default {
  components: { BookCover, PopupConfirm, EpisodeDetail },
  props: ["bookingID"],
  watch: {
    bookingID() {
      this.render();
    },
  },
  mounted() {
    this.render();
  },
  computed: {
    roles() {
      return this.$store.getters["Authen/getUser"].roles || [];
    },
    userID() {
      return this.$store.getters["Authen/getUser"].userID || "";
    },
  },
  data() {
    return {
      isAllowCheckIn: false,
      isAllowCancel: false,
      checkInLoading: false,
      cancelLoading: false,
      confirm: {
        title: "",
        message: "",
      },
      patient: {
        episodeID: null,
        timeStart: null,
        timeEnd: null,
        userCreate: {
          fullName: null,
          avatar: null,
          genderType: false,
          phone: null,
          email: null,
          userID: null,
        },
        userAppoint: {
          fullName: null,
          avatar: null,
          genderType: false,
          phone: null,
          email: null,
          userID: null,
        },
        service: {
          serviceName: null,
          price: null,
        },
        clinic: {
          clinicName: null,
          address: null,
        },
      },
      loading: false,
    };
  },
  methods: {
    async render() {
      if (!this.bookingID) return this.$refs.BookCover.showCover();
      this.$refs.BookCover.showContent();
      this.loading = true;
      var result = await this.$httpClient.get(
        `/user/appointments/${this.bookingID}`
      );
      this.loading = false;
      if (result.error) {
        return this.showError(
          "Get appointment detail failed. Please try again later."
        );
      } else {
        this.$emit("success", result);
      }
      this.patient = result;
      this.isAllowCancel =
        result.statusID == 1 &&
        (this.userID.toLowerCase() == result.userCreate.userID.toLowerCase() ||
          this.userID.toLowerCase() ==
            result.userAppoint.userID.toLowerCase() ||
          this.roles.find(
            (r) =>
              r.clinicID.toLowerCase() == result.clinicID.toLowerCase() &&
              r.roleType != RoleType.Patient
          ));

      this.isAllowCheckIn =
        result.statusID == 1 &&
        (this.userID.toLowerCase() == result.userAppoint.userID.toLowerCase() ||
          this.roles.find(
            (r) =>
              r.clinicID.toLowerCase() == result.clinicID.toLowerCase() &&
              r.roleType != RoleType.Patient
          ));
    },
    async checkin() {
      var clinicID = this.patient.clinicID;
      var bookingID = this.patient.appointmentID;
      this.confirm = {
        title: this.$t("common.checkin"),
        message: `${this.$t("common.checkin")} ${
          this.patient.userCreate.fullName
        }, ${this.$t("nav.service")} ${this.patient.service.serviceName}?`,
      };
      if (!(await this.$refs.PopupConfirm.confirm())) return;
      this.checkInLoading = true;
      var result = await this.$httpClient.post(
        `owner/clinics/${clinicID}/appointments/${bookingID}/checkin`
      );
      this.checkInLoading = false;
      if (result.error) {
        return this.showError("Checkin failed. Please try again later.");
      } else {
        // this.showSuccess(
        //   `Successfull checkin ${this.patient.userCreate.fullName}`
        // );
        this.$emit("checkedin", result);
        this.$router.push(`/episode?id=${result.episodeID}`);
      }
    },
    async cancel() {
      var bookingID = this.patient.appointmentID;
      this.confirm = {
        title: this.$t("common.cancel") + " " + this.$t("nav.booking"),
        message: `${this.$t("common.cancel") + " " + this.$t("nav.booking")} ${
          this.patient.userCreate.fullName
        }, ${this.$t("nav.service")} ${this.patient.service.serviceName}?`,
      };
      if (!(await this.$refs.PopupConfirm.confirm())) return;
      this.cancelLoading = true;
      var result = await this.$httpClient.delete(
        `user/appointments/${bookingID}`
      );
      this.cancelLoading = false;
      if (result.error) {
        return this.showError("Cancel failed. Please try again later.");
      } else {
        // this.showSuccess(
        //   `Successfull cancel ${this.patient.userCreate.fullName}`
        // );
        this.$emit("cancel", this.patient.appointmentID);
      }
    },
    formatPrice(val) {
      return convertPriceString(val, true);
    },
    formatTime(timeStart, timeEnd) {
      return (
        moment(timeStart).format("HH:mm") +
        " - " +
        moment(timeEnd).format("HH:mm") +
        " , " +
        moment(timeEnd).format("DD/MM/YYYY")
      );
    },
  },
};
</script>
